using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Text;
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
        
        const string API_URL = "http://rzhunemogu.ru/Rand.aspx";
        const string FINAL_URL = API_URL + "?CType=";
        private RestClient RC = new RestClient();

        public string getJokeOrStory(string category)
        {
            switch (category)
            {
                case "анекдот" : category = "1";
                    break;
                case "историю" : category = "2";
                    break;
                default : category = "1";
                    break;
            }
            var URL = FINAL_URL + category;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            
            XmlSerializer serializer = 
                new XmlSerializer(typeof(Jokes.Root));

            // у сервера проблемы с кодировкой- исправление
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