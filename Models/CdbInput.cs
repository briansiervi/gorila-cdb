using System;

namespace gorila_cdb.Models
{
    public class CdbInput
    {
        public int Id { get; set; }
        public DateTime investmentDate { get; set; }

        public float cdbRate { get; set; }

        public DateTime currentDate { get; set; }
    }
}
