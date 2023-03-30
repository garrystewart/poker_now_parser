using System.Text.Json.Serialization;

namespace PNP.Models
{
    public class JSON
    {
        private IEnumerable<Log> _logs = new List<Log>();

        public IEnumerable<Log> Logs { get => _logs.OrderByDescending(l => l.At); set => _logs = value; }

        public class Log
        {
            public DateTime At { get; set; }

            [JsonPropertyName("msg")]
            public string Message { get; set; } = string.Empty;
        }
    }
}