using System;
using System.Collections.Generic;
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
            public double temperature { get; set; }
            public List<String> weather_descriptions { get; set; }
        }
        public class Location
        {
            public Country country { get; set; }
        }
        
        public class Country
        {
            public string ctext { get; set; }
        }

        private RestClient RC = new RestClient();
        private readonly string PRE_FINAL_API;

        public WeatherForecasts(string weatherToken)
        {
            // формирование запроса к погодному апи
            PRE_FINAL_API = CONSTANTS.API_URL_WEATHER + weatherToken + CONSTANTS.API_LANGUAGE;
        }

        public string getWeatherInCity(string city)
        {
            // запрос
            var final_city = city;
            if (city.ToLower() == "москва")
                final_city = "Москва, Россия";
            
            var FINAL_URL = PRE_FINAL_API + final_city;
            var Request = new RestRequest(FINAL_URL);
            var Response = RC.Get(Request);
            
            // парсим ответ, он в json-формате
            var Data = JsonConvert.DeserializeObject<WeatherData>(Response.Content);
            var Temp = (int) Data.current.temperature;
            var weather_state = Data.current.weather_descriptions[0];
            var weather_state_final = "";
            
            // тут переводы для погодных условий (ясно, пасмурно и т.д)
            if (CONSTANTS.WeatherStateTranslates.ContainsKey(weather_state))
                weather_state_final = CONSTANTS.WeatherStateTranslates[weather_state];
            else
                weather_state_final = weather_state;
                
            return $"В городе {city.Substring(0, 1).ToUpper() + city.Substring(1, city.Length - 1)} сейчас {weather_state_final}, температура воздуха {Temp}";
            
        } // method getWeatherInCity

    } // class WeatherForecasts
}