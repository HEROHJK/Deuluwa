using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeuluwaCore.Model
{
    public class NoticeMessage
    {
        public string index { get; set; }
        public string message { get; set; }
        public string user { get; set; }
        public string time { get; set; }

        public NoticeMessage(Dictionary<string, string> dict)
        {
            if (dict.ContainsKey("index")) index = dict["index"];
            if (dict.ContainsKey("message")) message = dict["message"];
            if (dict.ContainsKey("user")) user = dict["user"];
            if (dict.ContainsKey("time")) time = dict["time"];
        }
    }
}
