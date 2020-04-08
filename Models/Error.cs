using System;
using System.Text.Json.Serialization;

namespace gorila_cdb.Models
{
    public class Error
    {
        [JsonIgnore]
        public int Id { get; set; }
        public String[] fields { get; set; }
        public String message { get; set; }
    }
}
