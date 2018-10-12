using System.Collections.Generic;
using Newtonsoft.Json;
using DeuluwaCore.Model;

namespace DeuluwaCore.Controller
{
    public class JsonConverter
    {
        public static Dictionary<string, string> GetDictionary(string str)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
        }

        public static List<Dictionary<string, string>> GetDictionaryList(string str)
        {
            return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(str);
        }        
    }
}
