using System;
using System.Collections.Generic;
using System.Text;

namespace Deuluwa
{
    public class CourseInformation : CourseInformationJson
    {
        public CourseInformation(CourseInformationJson json)
        {
            index = json.index;
            coursename = json.coursename;
            teacher = json.teacher;
            starttime = Constants.MakeAMPM(json.starttime);
            endtime = Constants.MakeAMPM(json.endtime);
            roomname = json.roomname;
            classday = Constants.MakeClassDay(json.classday);
        }
    }

    public class CourseInformationJson
    {
        public string index { get; set; }
        public string coursename { get; set; }
        public string teacher { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string roomname { get; set; }
        public string classday { get; set; }
    }
}
