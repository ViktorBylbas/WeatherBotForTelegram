namespace Bot
{    
    class ParserJSON
    {
        public string jsonText { get; set; }

        public ParserJSON(string jsonText)
        {
            this.jsonText = jsonText;
        }

        public Response LoadJson()
        {
            string text = jsonText;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(text);
        }
    }
}
