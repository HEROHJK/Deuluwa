using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;

namespace DeuluwaPIM.View
{
    /// <summary>
    /// UserControlWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControlWindow
    {
        List<Model.UserAddInformation> userList;
        List<Model.UserCourse> courseList;
        List<Model.Attendance> attendanceList;

        public UserControlWindow()
        {
            InitializeComponent();

            ModifyMode(false);


            LoadUsers();
        }

        private async void LoadUsers()
        {
            
            userList =  JsonConvert.DeserializeObject<List<Model.UserAddInformation>>
                (await Constants.HttpRequest("http://silco.co.kr:18000/userlist"));
            userDatagrid.ItemsSource = userList;
        }

        private async void LoadCourses(string id)
        {
            try
            {
                courseList = JsonConvert.DeserializeObject<List<Model.UserCourse>>
                    (await Constants.HttpRequest("http://silco.co.kr:18000/usercourselist/?id=" + id));
                courseDatagrid.ItemsSource = courseList;
            }
            catch { }
        }

        private async void LoadAttendances(string id, string courseid)
        {
            try
            {
                attendanceList = JsonConvert.DeserializeObject<List<Model.Attendance>>
                    (await Constants.HttpRequest("http://silco.co.kr:18000/userattendancelist/?courseid=" + courseid + "&id=" + id));

                foreach(var attendance in attendanceList)
                {
                    attendance.Change();
                }

                attendanceDatagrid.ItemsSource = attendanceList;

            }
            catch
            {

            }
        }

        private void ViewUser(Model.UserAddInformation user)
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
            Model.UserAddInformation item = userDatagrid.SelectedItem as Model.UserAddInformation;

            ViewUser(item);
        }

        private void courseDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.UserCourse item = courseDatagrid.SelectedItem as Model.UserCourse;

            LoadAttendances(idBox.Text, item.index);
        }
    }
}
