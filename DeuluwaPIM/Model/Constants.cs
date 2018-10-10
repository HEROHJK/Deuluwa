using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace DeuluwaPIM.Model
{
    class Constants
    {
        public static string HttpRequest(string url)
        {
            string result = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 3000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
                stream.Close();
                response.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return result;
        }

        public static string HttpRequestPost(string url, string postdata)
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

                using(var sendStream = request.GetRequestStream())
                {
                    sendStream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
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

        public static Style MakeRowStyle(RoutedEvent routedEvent, System.Windows.Input.MouseButtonEventHandler handler)
        {
            Style rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(routedEvent, handler));
            return rowStyle;
        }
    }
}
