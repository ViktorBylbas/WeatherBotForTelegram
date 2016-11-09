using System.Collections.Generic;
using System.Net;

namespace Bot
{
    class Telegram
    {
        private static string Token = @"enter_token_your_bot";
        private static string Link = "https://api.telegram.org/bot" + Token;
        private static int LastUpdateID = 0;
        private static Telegram instance;

        private Telegram() { }

        public static Telegram GetInstance()
        {
            if (instance == null)
                instance = new Telegram();
            return instance;
        }

        public string GetUpdates()
        {
            Response resp;
            string message = "";
            string errorMessage = "This command is missing. Enter /getweather - output weather in the near future";
            string weatherMessage = "Харьков:\n";

            while (true)
            {
                using (var webClient = new WebClient())
                {
                    string response = webClient.DownloadString(Link + "/getUpdates" + "?offset=" + (LastUpdateID + 1));
                    if (response.Length <= 23)
                        continue;

                    resp = new ParserJSON(response).LoadJson();
                    foreach (var item in resp.result)
                    {
                        LastUpdateID = item.update_id;
                        message = "(" + item.message.chat.id + ") @" + item.message.from.username + ": " + item.message.text;
                        if (item.message.text.Contains("/getweather"))
                        {
                            Weather weather = new Weather();
                            List<Weather> weatherList = weather.GetWeather();
                            foreach (var w in weatherList)
                                weatherMessage += w.Date + "\nТемпература: " + w.Temperature + "\nОсадки, мм: " + w.Rainfall
                                    + "\n\n";
                            SendMessage(weatherMessage, item.message.chat.id);
                            message += " " + true;
                        }
                        else
                            SendMessage(errorMessage, item.message.chat.id);
                    }
                }

                return message;
            }
        }

        private void SendMessage(string message, int chatId)
        {
            using (var webClient = new WebClient())
            {
                var pars = new System.Collections.Specialized.NameValueCollection();
                pars.Add("text", message);
                pars.Add("chat_id", chatId.ToString());
                webClient.UploadValues(Link + "/sendMessage", pars);
            }
        }
    }
}
