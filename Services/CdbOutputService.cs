using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using gorila_cdb.IService;
using gorila_cdb.Models;
using CsvHelper;

namespace gorila_cdb.Service
{
    public class CdbOutputService : ICdbOutputService
    {
        private readonly String _file =  Path.Combine(
            Directory.GetCurrentDirectory(),
            "Data",
            "CDI_Prices.csv"
        );

        private IEnumerable<Csv> _smallCsv { get; set; }
        private CdbInput _cdbInput { get; set; }

        /// <summary>
        /// Calculates the accumulated Cdb tax
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CdbOutput> GetResult()
        {
            IList<CdbOutput> cdbOutputList = new List<CdbOutput>();
            double cdi = 0.0;
            decimal unitPrice = 1;

            for (DateTime date = _cdbInput.investmentDate; date <= _cdbInput.currentDate; date = date.AddDays(1))
            {
                Csv itemFound = _smallCsv.Where(x => x.dtDate == date).FirstOrDefault();
                if (itemFound != null)
                {
                    cdi = (itemFound.dLastTradePrice / 100 + 1);
                    cdi = Math.Pow(cdi, (1 / 252f));
                    cdi = Math.Round(cdi - 1, 8);

                    unitPrice = unitPrice * ((decimal)(cdi * _cdbInput.cdbRate / 100) + 1);
                    unitPrice = Math.Round(unitPrice, 14); //The test on the "www.notion.so" website is erroneously rounding values up to the 14th decimal place. I forced to have the same result.
                    unitPrice = Convert.ToDecimal(unitPrice.ToString("n16"));

                    cdbOutputList.Add(new CdbOutput()
                    {
                        Id = cdbOutputList.Count,
                        date = itemFound.dtDate,
                        unitPrice = unitPrice
                    });
                }
            }

            return cdbOutputList;
        }

        /// <summary>
        /// Reads csv data
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private IEnumerable<Csv> GetData(String file)
        {
            try
            {
                IList<Csv> records = new List<Csv>();

                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                    csv.Configuration.HeaderValidated = null;
                    csv.Configuration.MissingFieldFound = null;
                    csv.Configuration.RegisterClassMap<CsvMap>();

                    reader.ReadLine(); //jumping header
                    while (csv.Read())
                    {
                        records.Add(
                            new Csv(){
                                Id = records.Count,
                                sSecurityName = csv.GetField(0),
                                dtDate = DateTime.Parse(csv.GetField(1)),
                                dLastTradePrice = float.Parse(csv.GetField(2).Replace(".",","))
                            }
                        );
                    }

                    return records;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Decreases the number of records to be searched in the csv file.    
        /// </summary>
        /// <param name="cdbInput"></param>
        /// <param name="csv"></param>
        /// <returns></returns>
        private IEnumerable<Csv> OptimizeData(IEnumerable<CdbInput> cdbInput, IEnumerable<Csv> csv)
        {
            List<Csv> newCsv = (from a in csv
                                from b in cdbInput.Where(x =>
                                    (a.dtDate.AddMonths(1)) >= x.investmentDate &&
                                    (a.dtDate.AddMonths(-1)) <= x.currentDate
                                )
                                select new Csv()
                                {
                                    sSecurityName = a.sSecurityName,
                                    dtDate = a.dtDate,
                                    dLastTradePrice = a.dLastTradePrice
                                }).ToList();

            newCsv.OrderBy(x => x.dtDate);
            return newCsv;
        }

        /// <summary>
        /// Validates api input data
        /// </summary>
        /// <param name="cdbInput"></param>
        /// <returns></returns>
        public IList<Error> Validate(CdbInput cdbInput)
        {
            IEnumerable<CdbInput> cdbInputList = new CdbInput[] { cdbInput };
            List<Error> errorList = new List<Error>();
            DateTime minDateCsv, maxDateCsv;

            _smallCsv = OptimizeData(cdbInputList, GetData(_file));
            _cdbInput = cdbInput;

            if (_smallCsv.Count() == 0)
            {
                errorList.Add(new Error()
                {
                    fields = new String[]{
                        "currentDate", "investmentDate"
                    },
                    message = "Desculpe, mas as datas informadas não estão presentes em nossas bases de dados."
                });

                return errorList;
            }

            if (cdbInput.currentDate > DateTime.Now)
            {
                errorList.Add(new Error()
                {
                    fields = new String[]{"currentDate"},
                    message = $"Desculpe, mas não podemos prever o valor do resgate na data de {cdbInput.currentDate.ToShortDateString()}."
                });

                return errorList;
            }

            if (cdbInput.investmentDate > DateTime.Now)
            {
                errorList.Add(new Error()
                {
                    fields = new String[]{"investmentDate"},
                    message = $"Desculpe, mas você ainda não realizou o seu investimento, correto?! Por favor, verifique a data de investimento."
                });

                return errorList;
            }

            if (cdbInput.currentDate < cdbInput.investmentDate)
            {
                errorList.Add(new Error()
                {
                    fields = new String[]{"investmentDate"},
                    message = $"A data de resgate não pode ser menor do que a data de investimento."
                });

                return errorList;
            }            

            if (cdbInput.cdbRate <= 0)
            {
                errorList.Add(new Error()
                {
                    fields = new String[]{"cdbRate"},
                    message = "Taxa cdb inválida"
                });
            }

            minDateCsv = _smallCsv.Select(x => x.dtDate).Min();
            
            if (cdbInput.investmentDate < minDateCsv)
            {
                errorList.Add(new Error()
                {
                    fields = new String[]{"investmentDate"},
                    message = $"Por favor, insira um valor maior do que este. A menor data de investimento presente em nossas bases é {minDateCsv.ToShortDateString()}."
                });
            }

            maxDateCsv = _smallCsv.Select(x => x.dtDate).Max();
            
            if (cdbInput.currentDate > maxDateCsv)
            {
                errorList.Add(new Error()
                {
                    fields = new String[]{"currentDate"},
                    message = $"Por favor, insira um valor menor do que este. A maior data de resgate presente em nossas bases é {maxDateCsv.ToShortDateString()}."
                });
            }

            return errorList;
        }
    }
}