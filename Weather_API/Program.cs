using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace Weather_API
{
    public class Program
    {
        private static object FormatedResponse;

        static void Main(string[] args)
        {
            var client = new HttpClient();
            var apiKey = "b738b5b25b624dca67669be7c3c605db";

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

                //Print It
                Console.WriteLine($"It's {temp}° outside right now. \nIt's up to you to decide what to do. \nChoose wisely.");
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