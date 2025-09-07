using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace SqlQueryRunnerFnApp
{
    public class QueryFunction
    {
        private readonly ILogger _logger;
        private readonly string _connectionString;

        public QueryFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<QueryFunction>();
            _connectionString = Environment.GetEnvironmentVariable("SqlConnectionString") ?? "";
        }

        [Function("RunSqlQuery")]
        public async Task<HttpResponseData> RunSqlQuery(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var query = JsonSerializer.Deserialize<QueryRequest>(requestBody)?.Query;
            var response = req.CreateResponse();

            if (string.IsNullOrWhiteSpace(query))
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                await response.WriteStringAsync(JsonSerializer.Serialize(new { error = "Query is required." }));
                return response;
            }

            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();
                using var cmd = new SqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                var results = new List<Dictionary<string, object>>();
                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }
                    results.Add(row);
                }
                await response.WriteStringAsync(JsonSerializer.Serialize(new { results }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running query");
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                await response.WriteStringAsync(JsonSerializer.Serialize(new { error = ex.Message }));
            }
            return response;
        }

        public class QueryRequest
        {
            public string? Query { get; set; }
        }
    }
}
