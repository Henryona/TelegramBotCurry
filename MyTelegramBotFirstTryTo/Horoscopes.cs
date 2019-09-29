using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Chroniton.Schedules;
using RestSharp;
using Newtonsoft.Json;

namespace MyTelegramBotFirstTryTo
{
    public class Horoscope
    {
        public class HoroscopeData
        {
            public Horo horo { get; set; }
        }

        [XmlRoot(ElementName = "horo")]
        public class Horo
        {
            [XmlElement]
            public Sign aries { get; set; }
            [XmlElement]
            public Sign taurus { get; set; }
            [XmlElement]
            public Sign gemini { get; set; }
            [XmlElement]
            public Sign cancer { get; set; }
            [XmlElement]
            public Sign leo { get; set; }
            [XmlElement]
            public Sign virgo { get; set; }
            [XmlElement]
            public Sign libra { get; set; }
            [XmlElement]
            public Sign scorpio { get; set; }
            [XmlElement]
            public Sign sagittarius { get; set; }
            [XmlElement]
            public Sign capricorn { get; set; }
            [XmlElement]
            public Sign aquarius { get; set; }
            [XmlElement]
            public Sign pisces { get; set; }
        }

        public class Sign
        {
            [XmlElement]
            public string today { get; set; }
        }
        
        
        public Horoscope()
        { }
        
        // составляем url для запроса гороскопа
        string FINAL_URL = CONSTANTS.API_URL_HORO + CONSTANTS.API_HORO_TYPE + CONSTANTS.API_XML ;
        private RestClient RC = new RestClient();
        
        public string getHororscopeBySign(string signOfZodiac)
        {
            // запрос гороскопа
            var Request = new RestRequest(FINAL_URL);
            var Response = RC.Get(Request);

            // парсим ответ, он в xml формате
            XmlSerializer serializer = new XmlSerializer(typeof(Horo));
            var ms = new MemoryStream(Response.RawBytes);
            var Data = (Horo)serializer.Deserialize(ms);

            // возвращаем гороскоп на нужный знак зодиака
            switch (signOfZodiac)
            {
                case "овен" : return Data.aries.today;
                case "телец" : return Data.taurus.today;
                case "близнецы" : return Data.gemini.today;
                case "рак" : return Data.cancer.today;
                case "лев" : return Data.leo.today;
                case "дева" : return Data.virgo.today;
                case "весы" : return Data.libra.today;
                case "скорпион" : return Data.scorpio.today;
                case "стрелец" : return Data.sagittarius.today;
                case "козерог" : return Data.capricorn.today;
                case "водолей" : return Data.aquarius.today;
                case "рыбы" : return Data.pisces.today;
                default : return CONSTANTS.UNKNOWN_ZODIAC;
            } // switch (signOfZodiac)
            
        } // method getHororscopeBySign
    } //class Horoscope
}