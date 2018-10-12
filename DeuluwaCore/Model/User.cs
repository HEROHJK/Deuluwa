using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeuluwaCore.Model
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
        public string admin { get; set; }

        public User(Dictionary<string, string> dict)
        {
            if (dict.ContainsKey("id")) id = dict["id"];
            if (dict.ContainsKey("name")) name = dict["name"];
            if (dict.ContainsKey("address")) address = dict["address"];
            if (dict.ContainsKey("phonenumber")) phonenumber = dict["phonenumber"];
            if (dict.ContainsKey("admin")) admin = dict["admin"];
        }
    }
}
