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
	public partial class ClassInformationPage : ContentPage
	{
        HttpClient client = new HttpClient { MaxResponseContentBufferSize = 256000 };
		public ClassInformationPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            listView.RowHeight = (int)App.Current.MainPage.Height / 8;
            JoinHttp();
        }

        private List<CustomClass> CoursesLoad(string httpString)
        {
            List<CustomClass> list = new List<CustomClass>();

            try
            {
                list = JsonConvert.DeserializeObject<List<CustomClass>>(httpString);
            }
            catch { }

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