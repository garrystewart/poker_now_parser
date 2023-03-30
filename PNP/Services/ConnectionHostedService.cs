using PNP.Models;

namespace PNP.Services
{
    public class ConnectionHostedService : IHostedService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ConnectionHostedService> _logger;
        private ICollection<string> _ids = new HashSet<string>();
        private ICollection<Response> _responses = new HashSet<Response>();
        private Timer? _timer = null;
        private int executionCount = 0;

        public ConnectionHostedService(ILogger<ConnectionHostedService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public void AddId(string id)
        {
            if (!_ids.Any(i => i == id))
            {
                _ids.Add(id);
            }            
        }

        public void RemoveId(string id)
        {
            _ids.Remove(id);
        }

        public IEnumerable<string> GetIds()
        {
            return _ids;
        }

        public Response? GetResponse(string id)
        {
            return _responses.SingleOrDefault(r => r.Id == id);
        }

        public class Response
        {
            public Response()
            {
                LastUpdated = DateTime.Now;
            }

            public string Id { get; set; } = string.Empty;
            public JSON? Json { get; set; }
            public DateTime LastUpdated { get; set; }
        }

        public JSON? GetLogs(string id)
        {
            return _httpClient.GetFromJsonAsync<JSON>($"https://www.pokernow.club/games/{id}/log").Result;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void DoWork(object? state)
        {
            foreach (string id in _ids)
            {
                JSON? json = GetLogs(id);

                if (json is not null)
                {
                    // check to see if there's already a record, if not create a new one
                    Response? response = _responses.SingleOrDefault(r => r.Id == id);

                    if (response is not null)
                    {
                        // update the record
                        response.Json = json;
                        response.LastUpdated = DateTime.Now;
                    }
                    else
                    {
                        // create a new record
                        _responses.Add(new Response
                        {
                            Id = id,
                            Json = json
                        });
                    }
                }
            }

            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }
    }
}
