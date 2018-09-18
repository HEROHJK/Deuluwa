using System;
using System.Collections.Generic;
using System.Text;

namespace Deuluwa
{
    class Constants
    {
        public static Constants shared = new Constants();
        private Dictionary<string, string> dataList;

        public Constants()
        {
            dataList = new Dictionary<string, string>();
            InsertData("url", "http://silco.co.kr:18000/");
        }

        public void InsertData(string key, string value)
        {
            dataList.Add(key, value);
        }

        public void DeleteLoginInformation()
        {
            dataList.Remove("id");
            dataList.Remove("password");
        }

        public string GetData(string key)
        {
            return dataList[key];
        }
    }
}
