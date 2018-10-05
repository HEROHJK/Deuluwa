using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeuluwaPIM
{
    class NoticeList
    {
        public static List<NoticeMessage> list;
    }

    public class NoticeMessage
    {
        [JsonProperty("index")]
        public string index { get; set; }
        [JsonProperty("time")]
        public string date { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("user")]
        public string writer { get; set; }
    }
}
