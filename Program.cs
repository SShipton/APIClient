using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ConsoleTables;

namespace APIClient
{
    class Program
    {
        class Item
        {
            [JsonPropertyName("id")]
            public int id { get; set; }


            [JsonPropertyName("title")]
            public string title { get; set; }


            [JsonPropertyName("description")]
            public string description { get; set; }


            [JsonPropertyName("director")]
            public string director { get; set; }


            [JsonPropertyName("producer")]
            public string producer { get; set; }


            [JsonPropertyName("release_date")]
            public int release_date { get; set; }


            [JsonPropertyName("rt_score")]
            public int rt_score { get; set; }

            [JsonPropertyName("complete")]
            public bool Complete { get; set; }

            public string CompletedStatus
            {
                get
                {
                    return Complete ? "completed" : "not completed";
                }
            }
        }

        static async Task Main(string[] args)
        {
            var accessToken = "";

            if (args.Length == 0)
            {
                Console.WriteLine("Which list would you like?");
                accessToken = Console.ReadLine();
            }
            else
            {
                accessToken = args[0];
            }

            var client = new HttpClient();

            var url = $"https://ghibliapi.herokuapp.com/items?access_token={accessToken}";
            var responseStream = await client.GetStreamAsync("https://ghibliapi.herokuapp.com");

            var items = await JsonSerializer.DeserializeAsync<List<Item>>(responseStream);

            var table = new ConsoleTable("Title", "Release Date", "RT Score");

            foreach (var item in items)
            {
                table.AddRow(item.title, item.release_date, item.rt_score);
            }

            table.Write();
        }
    }
}
