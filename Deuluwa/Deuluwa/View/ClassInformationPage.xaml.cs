using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deuluwa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClassInformationPage : ContentPage
	{
        HttpClient client = new HttpClient { MaxResponseContentBufferSize = 256000 };
		public ClassInformationPage ()
		{
			InitializeComponent ();
            listView.RowHeight = (int)App.Current.MainPage.Height / 8;
            JoinHttp();
        }

        private List<CustomClass> CoursesLoad(string httpString)
        {
            List<CustomClassJson> jsonList = new List<CustomClassJson>();

            try
            {
                jsonList = JsonConvert.DeserializeObject<List<CustomClassJson>>(httpString);
            }
            catch { }

            List<CustomClass> list = new List<CustomClass>();

            foreach(CustomClassJson json in jsonList)
            {
                list.Add(new CustomClass(json));
            }

            return list;
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
	}
}