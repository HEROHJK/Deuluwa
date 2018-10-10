namespace DeuluwaPIM
{

    class NoticeList
    {
        public static System.Collections.Generic.List<NoticeMessage> list;
    }

    public class NoticeMessage
    {
        [Newtonsoft.Json.JsonProperty("index")]
        public string index { get; set; }
        [Newtonsoft.Json.JsonProperty("time")]
        public string date { get; set; }
        [Newtonsoft.Json.JsonProperty("message")]
        public string message { get; set; }
        [Newtonsoft.Json.JsonProperty("user")]
        public string writer { get; set; }
    }

}