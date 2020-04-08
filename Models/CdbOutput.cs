using System;
using System.Text.Json.Serialization;

namespace gorila_cdb.Models
{
    public class CdbOutput
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime date { get; set; }

        public decimal unitPrice { get; set; }
    }
}
