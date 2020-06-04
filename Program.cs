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
        class Brewery
        {
            [JsonPropertyName("id")]
            public int id { get; set; }


            [JsonPropertyName("name")]
            public string name { get; set; }


            [JsonPropertyName("city")]
            public string city { get; set; }


            [JsonPropertyName("state")]
            public string state { get; set; }


            [JsonPropertyName("country")]
            public string country { get; set; }


            [JsonPropertyName("brewery type")]
            public string brewery_type { get; set; }


            [JsonPropertyName("street")]
            public string street { get; set; }

            [JsonPropertyName("postal code")]
            public int postal_code { get; set; }

            [JsonPropertyName("longitude")]
            public int longitude { get; set; }

            [JsonPropertyName("latitude")]
            public int latitude { get; set; }

            [JsonPropertyName("phone")]
            public int phone { get; set; }

            [JsonPropertyName("website url")]
            public string website_url { get; set; }

            [JsonPropertyName("updated at")]
            public DateTime updated_at { get; set; }

            [JsonPropertyName("tag list")]
            public string tag_list { get; set; }

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

        static async Task<string> Main(string[] args)
        {
            var client = new HttpClient();

            Console.WriteLine("Please enter the ID of the brewery you wish to search for:   ");

            try
            {
                var id = int.Parse(Console.ReadLine());

                var url = $"https://api.openbrewerydb.org/breweries/{id}";

                var responseStream = await client.GetStreamAsync(url);

                var brewery = await JsonSerializer.DeserializeAsync<Brewery>(responseStream);

                var table = new ConsoleTable("ID", "Name", "City", "State", "Country");

                table.AddRow(brewery.id, brewery.name, brewery.city, brewery.state, brewery.country);
                table.Write();
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("ID not found.");
            }
            catch (FormatException)
            {
                Console.WriteLine("ID not found.");
            }
            //break;
        }
    }
}

