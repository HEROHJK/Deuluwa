using System;
using System.IO;
using System.Net;

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
    }
}
