namespace Bot
{
    class Response
    {
        public bool ok { get; set; }
        public Result[] result { get; set; }
    }

    class Result
    {
        public int update_id { get; set; }
        public Message message { get; set; }
    }

    class Message
    {
        public int message_id { get; set; }
        public User from { get; set; }
        public User chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
        public Entities[] entities { get; set; }
    }

    class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string type { get; set; }
    }

    class Entities
    {
        public string type { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
    }
}
