using System;
using System.Collections.Generic;
using System.Text;

namespace Deuluwa
{
    public class CustomClassJson
    {
        public string index { get; set; }
        public string coursename { get; set; }
        public string classday { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
    }

    public class CustomClass
    {
        public string index { get; set; }
        public string coursename { get; set; }
        public string classday { get; set; }
        public string coursedate { get; set; }
        public string coursetime { get; set; }

        public CustomClass(CustomClassJson json)
        {
            index = json.index;
            coursename = json.coursename;
            classday = MakeClassDay(json.classday);
            coursedate = json.startdate + " - " + json.enddate;
            coursetime = MakeClassTime(json.starttime, json.endtime);
        }

        private string MakeClassDay(string charArray)
        {
            string result = "매주 ";
            string[] weekofDay = { "월,", "화,", "수,", "목,", "금,", "토,", "일," };
            
            for(int i = 0; i<7; i++)
            {
                if (charArray[i] == 'T') result += weekofDay[i];
            }
            return result.Substring(0, result.Length - 1);
        }

        private string MakeClassTime(string startTime, string endTime)
        {
            // pm/am처리
            startTime = Convert.ToInt32(startTime.Substring(0, 2)) >= 12 ? 
                "PM " + string.Format("{0:00}:{1}",Convert.ToInt32(startTime.Substring(0,2))-12,startTime.Substring(3,2)) :
                "AM " + startTime;

            endTime = Convert.ToInt32(endTime.Substring(0, 2)) >= 12 ? 
                "PM " + string.Format("{0:00}:{1}", Convert.ToInt32(endTime.Substring(0, 2)) - 12, endTime.Substring(3, 2)) : 
                "AM " + endTime;

            return startTime + " ~ " + endTime;
        }

    }
}
