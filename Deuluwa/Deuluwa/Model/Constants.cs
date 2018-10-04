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

        public static string MakeClassDay(string charArray)
        {
            string result = "매주 ";
            string[] weekofDay = { "월,", "화,", "수,", "목,", "금,", "토,", "일," };

            for (int i = 0; i < 7; i++)
            {
                if (charArray[i] == 'T') result += weekofDay[i];
            }
            return result.Substring(0, result.Length - 1);
        }

        public static string MakeClassTime(string startTime, string endTime)
        {
            // pm/am처리
            startTime = Convert.ToInt32(startTime.Substring(0, 2)) >= 12 ?
                "PM " + string.Format("{0:00}:{1}", Convert.ToInt32(startTime.Substring(0, 2)) - 12, startTime.Substring(3, 2)) :
                "AM " + startTime;

            endTime = Convert.ToInt32(endTime.Substring(0, 2)) >= 12 ?
                "PM " + string.Format("{0:00}:{1}", Convert.ToInt32(endTime.Substring(0, 2)) - 12, endTime.Substring(3, 2)) :
                "AM " + endTime;

            return startTime + " ~ " + endTime;
        }

        public static string MakeAMPM(string time)
        {
            return Convert.ToInt32(time.Substring(0, 2)) >= 12 ?
                "PM " + string.Format("{0:00}:{1}", Convert.ToInt32(time.Substring(0, 2)) - 12, time.Substring(3, 2)) :
                "AM " + time;
        }
    }
}
