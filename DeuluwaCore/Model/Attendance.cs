using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeuluwaCore.Model
{
    public class Attendance
    {
        public string checkdate { get; set; }
        public string checktime { get; set; }
        public string attendance { get; set; }

        public Attendance(Dictionary<string, string> dict)
        {
            checkdate = dict["checkdate"];
            checktime = dict["checktime"];
            attendance = dict["attendance"];

            TimeChange();
            AttendanceChange();
        }

        private void TimeChange()
        {
            checktime = checktime.Trim();
            checktime = Convert.ToInt32(checktime.Substring(0, 2)) >= 12 ?
                "PM " + string.Format("{0:00}:{1}", Convert.ToInt32(checktime.Substring(0, 2)) - 12, checktime.Substring(2, 2)) :
                "AM " + checktime;
        }

        private void AttendanceChange()
        {
            switch (attendance)
            {
                case "0": attendance = "출석"; break;
                case "1": attendance = "지각"; break;
                case "2": attendance = "결석"; break;
                default: attendance = "error"; break;
            }
        }
    }
}
