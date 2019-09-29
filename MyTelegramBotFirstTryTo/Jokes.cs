using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using RestSharp;

namespace MyTelegramBotFirstTryTo
{
    public class Jokes
    {

        public class JokeData
        {
            public Root root { get; set; }
        }
        
        [XmlRoot(ElementName = "root")]

        public class Root
        {
            [XmlElement] 
            public string content { get; set; }            
        }
        
        // составляем url для запроса анекдота или истории
        private string PRE_FINAL_URL = CONSTANTS.API_URL_JOKE + CONSTANTS.TYPE;
        private RestClient RC = new RestClient();

        public string getJokeOrStory(string category)
        {
            // выбор: анекдот или история
            switch (category)
            {
                case "анекдот" : category = "1";
                    break;
                case "историю" : category = "2";
                    break;
                default : category = "1";
                    break;
            }
            
            // запрос анекдота\истории
            var Request = new RestRequest(PRE_FINAL_URL + category);
            var Response = RC.Get(Request);
            
            // парсим ответ, он в xml формате
            XmlSerializer serializer = new XmlSerializer(typeof(Jokes.Root));

            // у сервера проблемы с кодировкой - исправление
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding(1251);
            var enc1251 = encoding.GetString(Response.RawBytes);
            
            var Data = (Root)serializer.Deserialize(new StringReader(enc1251));
            
            return Data.content;
        } // method getJokeOrStory
        
        public Jokes()
        {}
    } // class Jokes
}