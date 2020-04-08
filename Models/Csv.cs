using System;

namespace gorila_cdb.Models
{
    public class Csv
    {
        public int Id { get; set; }

        public String sSecurityName { get; set; }

        public DateTime dtDate { get; set; }

        public float dLastTradePrice { get; set; }
    }
}
