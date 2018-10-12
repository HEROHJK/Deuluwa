using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using DeuluwaCore.Model;

namespace DeuluwaPIM.View
{
    /// <summary>
    /// UserControlWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControlWindow
    {
        List<User> userList;
        List<CourseInformation> courseList;
        List<Attendance> attendanceList;

        public UserControlWindow()
        {
            InitializeComponent();

            ModifyMode(false);


            LoadUsers();
        }

        private async void LoadUsers()
        {
            userList = new List<User>();

            var list = DeuluwaCore.Controller.JsonConverter.GetDictionaryList
                (await DeuluwaCore.Constants.HttpRequest("http://silco.co.kr:18000/userlist"));

            foreach(var dict in list)
            {
                userList.Add(new User(dict));
            }

            userDatagrid.ItemsSource = userList;
        }

        private async void LoadCourses(string id)
        {
            try
            {
                courseList = new List<CourseInformation>();
                var dictList = DeuluwaCore.Controller.JsonConverter.GetDictionaryList
                    (await DeuluwaCore.Constants.HttpRequest("http://silco.co.kr:18000/usercourselist/?id=" + id));

                foreach(var dict in dictList)
                {
                    courseList.Add(new CourseInformation(dict));
                }

                courseDatagrid.ItemsSource = courseList;
            }
            catch { }
        }

        private async void LoadAttendances(string id, string courseid)
        {
            try
            {
                attendanceList = new List<Attendance>();
                List<Dictionary<string, string>> list = DeuluwaCore.Controller.JsonConverter.GetDictionaryList
                    (await DeuluwaCore.Constants.HttpRequest("http://silco.co.kr:18000/userattendancelist/?courseid=" + courseid + "&id=" + id));

                foreach(var attendance in list)
                {
                    attendanceList.Add(new Attendance(attendance));
                }

                attendanceDatagrid.ItemsSource = attendanceList;

            }
            catch
            {

            }
        }

        private void ViewUser(User user)
        {
            //개인정보 로드
            idBox.Text = user.id;
            nameBox.Text = user.name;
            phonenumberBox.Text = user.phonenumber;
            addressBox.Text = user.address;

            //수강과목 로드
            LoadCourses(user.id);
        }

        private async void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if(button.Content as string == "정보 수정")
            {
                button.Content = "수정 완료";
                ModifyMode(true);
            }
            else
            {
                var dialogResult = await this.ShowMessageAsync("수정", "이대로 수정을 하시겠습니까?",MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, Constants.metroDialogSettings);
                if(dialogResult == MessageDialogResult.Affirmative)
                {
                    //수정 확인
                }
                else if(dialogResult == MessageDialogResult.Negative)
                {
                    //수정 확인 안함
                }
                else if(dialogResult == MessageDialogResult.FirstAuxiliary)
                {
                    //취소
                    button.Content = "정보 수정";
                    ModifyMode(false);
                }
                
            }
        }

        private void ModifyMode(bool on)
        {
            if (on)
            {
                nameBox.IsReadOnly =        false;
                phonenumberBox.IsReadOnly = false;
                addressBox.IsReadOnly =     false;
            }
            else
            {
                nameBox.IsReadOnly =        true;
                phonenumberBox.IsReadOnly = true;
                addressBox.IsReadOnly =     true;
            }
        }

        private void userDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User item = userDatagrid.SelectedItem as User;

            ViewUser(item);
        }

        private void courseDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CourseInformation item = courseDatagrid.SelectedItem as CourseInformation;

            LoadAttendances(idBox.Text, item.index);
        }
    }
}