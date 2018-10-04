using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deuluwa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseInformationPage : ContentPage
    {
        HttpClient client = new HttpClient { MaxResponseContentBufferSize = 256000 };

        public CourseInformationPage(string index)
        {
            InitializeComponent();
            GetCourseInformation(index);
        }

        private async void GetCourseInformation(string index)
        {
            string url = Constants.shared.GetData("url") +
                "coursetotalinformation/?courseid=" +
                index;

            Uri uri = new Uri(url);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    CourseInformation info = new CourseInformation(JsonConvert.DeserializeObject<CourseInformationJson>(content));
                    courseNameLabel.Text = info.coursename;
                    teacherNameLabel.Text = info.teacher;
                    courseTimeLabel.Text = info.starttime + " - " + info.endtime;
                    classLabel.Text = info.roomname;
                    Title = info.coursename;
                }
                catch
                {
                    await DisplayAlert("조회 실패", "조회에 실패하였어요 ㅠ\r\n조금 이따 다시 시도 해 볼래요?", "네 ㅎㅎ");
                    Navigation.PopAsync();
                }
                
            }
        }
    }
}