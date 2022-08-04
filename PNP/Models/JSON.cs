using System.Text.Json.Serialization;

namespace PNP.Models
{
    public class JSON
    {
        public IEnumerable<Log>? Logs { get; set; }

        public class Log
        {
            public DateTime At { get; set; }
            
            [JsonPropertyName("msg")]
            public string? Message { get; set; }
        }
    }
}