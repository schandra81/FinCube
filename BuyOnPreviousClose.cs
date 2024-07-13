using System.Net;
using KiteConnect;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FinCube
{
    public class BuyOnPreviousClose
    {
        private readonly Kite Kite = new("j0uutxy7z3gxinkm" /*api key*/, Debug: true);
        private readonly string InstrumentToken = "595969";
        private readonly string Tradingsymbol = "CPSEETF";
        private static LocalDb db = LocalDb.Load();
        private readonly ILogger _logger;

        public BuyOnPreviousClose(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BuyOnPreviousClose>();
        }

        [Function("BuyOnPreviousClose")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
