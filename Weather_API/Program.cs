using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Weather_API
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Begin reading appsettings.json file for ApiKey
            var client = new HttpClient();
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            string apiKey = config.GetSection("AppSettings")["ApiKey"];
            // End reading appsettings.json file for ApiKey


            while (true)
            {
                /* I had a problem with unhandled exceptions. I decided to watch
                the video. All of my code matched the video except for one thing. 
                I was sending my request to http instead of https.
                Most of the lines of code are to make the output look creative :).
                
                I would have liked to have sent more specific locations that 
                used city and state. That's for another time. */
                Console.WriteLine("~~ Ye Old Rad and Nifty Temperature App ~~");
                Console.WriteLine("==========================================");

                Console.Write("Enter a city: ");
                string city_name = Console.ReadLine();

                var weatherURL = $"https://api.openweathermap.org/data/2.5/forecast?q={city_name}&appid={apiKey}&units=imperial"; // endpoint
                var response = client.GetStringAsync(weatherURL).Result;

                JObject formattedResponse = JObject.Parse(response);
                var temp = formattedResponse["list"][0]["main"]["temp"]; // This was a little tricky
                var description = formattedResponse["list"][0]["weather"][0]["description"]; // This was a little tricky

                //Print It
                Console.WriteLine($"It's {temp}° outside right now and {description}. \nIt's up to you to decide what to do. \nChoose wisely.");
                Console.WriteLine("==========================================\n");

                // How many timess will they check the temperature? How many cites do they know the names of?
                Console.Write("Do you want to see the temperature \ninformation for another location? (y/n): ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice.ToLower() != "y")
                {
                    break;
                }
            }
        }
    }
}