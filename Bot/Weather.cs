using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Bot
{
    class Weather
    {
        public string Town { get; set; }
        public string Date { get; set; }
        public string WeatherPhenomena { get; set; }
        public string Temperature { get; set; }
        public string Wind { get; set; }
        public string AtmospherePressure { get; set; }
        public string Humidity { get; set; }
        public string Rainfall { get; set; }

        public List<Weather> GetWeather()
        {
            WebClient client = new WebClient();
            string html = client.DownloadString(@"http://pogoda.by/34300");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var table = doc.DocumentNode.SelectNodes("//div[@class='middle']/table").Descendants("tr")
                .Select((tr, i) => tr.Elements("td").Skip(2).Select(td => i == 1 ? td.Descendants("img").FirstOrDefault()
                .GetAttributeValue("title", "").Replace(".", ". ") : td.InnerText.Trim()).ToList()).ToList();

            List<Weather> weatherList = new List<Weather>();
            for (int i = 0; i < table[0].Count; i++)
                weatherList.Add(new Weather
                {
                    Town = "Харьков",
                    Date = table[0][i].Replace(".", ". "),
                    WeatherPhenomena = table[1][i],
                    Temperature = table[2][i],
                    Wind = table[3][i].Replace("&nbsp;", " "),
                    AtmospherePressure = table[4][i].Insert(4, " "),
                    Humidity = table[5][i],
                    Rainfall = table[6][i].Replace("—", "Без осадков")
                });

            return weatherList;
        }
    }
}
