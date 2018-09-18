using System;
using System.Collections.Generic;
using System.Text;

namespace Deuluwa
{
    public class CustomClass
    {
        public string className { get; set; }
        public string dayOfWeek { get; set; }
        public string classDate { get; set; }
        public string classTime { get; set; }

        public List<CustomClass> GetCustomClasses()
        {
            List<CustomClass> customClasses = new List<CustomClass>()
            {
                new CustomClass()
                {
                    className="영어 기본",
                    dayOfWeek="매주 월, 수, 금",
                    classDate="2018.09.01 ~ 2018.09.30",
                    classTime="PM 3:00 ~ PM 4:30"
                },
                new CustomClass()
                {
                    className="외국인 회화",
                    dayOfWeek="매주 월, 수, 금",
                    classDate="2018.09.01~2018.09.30",
                    classTime="PM 4:30 ~ PM 6:00"
                }
            };

            return customClasses;
        }
    }
}
