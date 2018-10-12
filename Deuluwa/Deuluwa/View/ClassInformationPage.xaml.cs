using System;
using System.Collections.Generic;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DeuluwaCore.Model;
using DeuluwaCore;

namespace Deuluwa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassInformationPage : ContentPage
    {
        HttpClient client = new HttpClient { MaxResponseContentBufferSize = 256000 };
        public ClassInformationPage()
        {
            InitializeComponent();
            listView.RowHeight = (int)App.Current.MainPage.Height / 8;
            JoinHttp();
        }

        private List<CourseInformation> CoursesLoad(string httpString)
        {
            List<CourseInformation> jsonList = new List<CourseInformation>();

            List<Dictionary<string, string>> dictList = DeuluwaCore.Controller.JsonConverter.GetDictionaryList(httpString);

            foreach (var dict in dictList)
            {
                jsonList.Add(new CourseInformation(dict));
            }
            return jsonList;
        }

        private async void JoinHttp()
        {
            string url =
                Constants.shared.GetData("url") +
                "usercourselist/?id=" +
                Constants.shared.GetData("id");

            Uri uri = new Uri(url);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                listView.ItemsSource = CoursesLoad(content);
            }
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CourseInformation customClass = e.Item as CourseInformation;
            Navigation.PushAsync(new CourseInformationPage(customClass.index));
        }
    }
}