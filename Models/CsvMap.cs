using CsvHelper.Configuration;

namespace gorila_cdb.Models
{
    public class CsvMap : ClassMap<Csv>
    {
        public CsvMap()
        {
            Map(m => m.sSecurityName).NameIndex(0);
            Map(m => m.dtDate).NameIndex(1);
            Map(m => m.dLastTradePrice).NameIndex(2);
        }
    }
}
