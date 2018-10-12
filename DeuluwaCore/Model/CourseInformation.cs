namespace DeuluwaCore.Model
{
    public class CourseInformation
    {
        public string index { get; set; }       //인덱스
        public string coursename { get; set; }  //수업명
        public string classday { get; set; }    //수업요일

        public string coursedate { get; set; }  //진행기간
        public string coursetime { get; set; }  //진행시간

        public string teacher { get; set; }     //강사명
        public string roomname { get; set; }    //강의실명

        public CourseInformation(System.Collections.Generic.Dictionary<string, string> dict)
        {
            if(dict.ContainsKey("index")) index = dict["index"];
            if (dict.ContainsKey("coursename")) coursename = dict["coursename"];
            if (dict.ContainsKey("roomname")) roomname = dict["roomname"];
            if (dict.ContainsKey("teacher")) teacher = dict["teacher"];

            classday = Constants.MakeClassDay(dict["classday"]);
            coursedate = dict["startdate"] + " - " + dict["enddate"];
            coursetime = Constants.MakeClassTime(dict["starttime"], dict["endtime"]);
            
        }
    }
}