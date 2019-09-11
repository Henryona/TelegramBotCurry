using System.Globalization;
using RestSharp;
using Newtonsoft.Json;

namespace MyTelegramBotFirstTryTo
{
    public class WeatherForecasts
    {
        public class WeatherData
        {
            public Current current { get; set; }
        }

        public class Current
        {
            public double temp_c { get; set; }
            public Condition condition { get; set; }
        }

        public class Condition
        {
            public string text { get; set; }
        }
        
        const string API_URL = "http://api.apixu.com/v1/current.json";
        const string API_KEY = "eb073dd347194fe0815161551193008";
        const string FINAL_URL = API_URL + "?key=" + API_KEY + "&lang=ru&q=";
        private RestClient RC = new RestClient();

        public WeatherForecasts()
        { }

        public string getWeatherInCity(string city)
        {
            var URL = FINAL_URL + city;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<WeatherData>(Response.Content);
            var Temp = (int) Data.current.temp_c;

            return $"В городе {city.Substring(0, 1).ToUpper() + city.Substring(1, city.Length - 1)} сейчас {Data.current.condition.text}, температура воздуха {Temp}";
        } // method getWeatherInCity

    } // class WeatherForecasts
}