using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeuluwaCore
{
    public class Constants
    {
        public static string MakeTime(string time)
        {
            // pm/am처리
            time = Convert.ToInt32(time.Substring(0, 2)) >= 12 ?
                "PM " + string.Format("{0:00}:{1}", Convert.ToInt32(time.Substring(0, 2)) - 12, time.Substring(3, 2)) :
                "AM " + time;

            return time;
        }

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

        public async static Task<string> HttpRequest(string url)
        {
            string result = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 3000;
                WebResponse response = await request.GetResponseAsync();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
                stream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return result;
        }

        public async static Task<string> HttpRequestPost(string url, string postdata)
        {
            string result = null;

            try
            {
                var data = UTF8Encoding.UTF8.GetBytes(postdata);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 3000;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentLength = data.Length;

                using (var sendStream = await request.GetRequestStreamAsync())
                {
                    sendStream.Write(data, 0, data.Length);
                }

                var response = await request.GetResponseAsync();
                result = new StreamReader(response.GetResponseStream()).ReadToEnd();

                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return result;
        }
    }
}
