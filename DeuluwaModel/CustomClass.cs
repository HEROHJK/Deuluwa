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

    public class CustomClass : CustomClassJson
    {
        public string coursedate { get; set; }
        public string coursetime { get; set; }

        public CustomClass(CustomClassJson json)
        {
            index = json.index;
            coursename = json.coursename;
            
            classday = DeuluwaController.Constants.MakeClassDay(json.classday);
            coursedate = json.startdate + " - " + json.enddate;
            coursetime = Constants.MakeClassTime(json.starttime, json.endtime);
        }
    }
}
