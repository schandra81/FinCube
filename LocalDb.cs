using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinCube
{
    public class LocalDb(string requestToken)
    {
        public string RequestToken { set; get; } = requestToken;
        public List<Accumulation> Investments { set; get; } = [];

        public static LocalDb Load()
        {
            using var http = new HttpClient();
            var response = http.GetAsync("https://techlancer.com/fincube.json").Result;
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Failed to get access token");
            var jsonString = response.Content.ReadAsStringAsync().Result;
            var db = JsonSerializer.Deserialize<LocalDb>(jsonString);
            if (db == null)
                throw new Exception("Failed to load local db");
            // Return the deserialized Store object
            return db;
        }

        public void Save()
        {
            // Serialize the Store object to JSON
            var jsonString = JsonSerializer.Serialize(this);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            using var http = new HttpClient();
            var response = http.PostAsync("https://techlancer.com/savefincube.php", content).Result;
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Failed to save localdb into server");
        }
    }

    public class Accumulation
    {
        public DateTime Date { set; get; }
        public decimal Amount { set; get; }
    }
}
