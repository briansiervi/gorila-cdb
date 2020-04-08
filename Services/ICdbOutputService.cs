using System.Collections.Generic;
using gorila_cdb.Models;

namespace gorila_cdb.IService
{
    /// <summary>
    /// Class that validates the input data of the api and returns the accumulated rate of the cdb
    /// </summary>
    public interface ICdbOutputService
    {
        public IList<Error> Validate(CdbInput cdbInput);
        public IEnumerable<CdbOutput> GetResult();        
    }
}