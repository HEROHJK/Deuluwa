using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deuluwa
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        HttpClient client;
		public LoginPage ()
		{
            InitializeComponent ();

            client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };

            idEntry.Completed += IdEntry_Completed;
            passwordEntry.Completed += PasswordEntry_Completed;
            loginButton.Clicked += LoginButton_Clicked;
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            LogInAction(sender, e);
        }

        private void PasswordEntry_Completed(object sender, EventArgs e)
        {
            LogInAction(sender, e);
        }

        private void IdEntry_Completed(object sender, EventArgs e)
        {
            passwordEntry.Focus();
        }

        public void LogInAction(object sender, EventArgs e)
        {
            Login(idEntry.Text, passwordEntry.Text);
        }

        public async void Login(string id, string password)
        {
            string url = Constants.shared.GetData("url") + string.Format("user/?id={0}&password={1}", id, password);

            var uri = new Uri(url);

            bool result = false;

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                if (content == "success") result = true;
                else result = false;
            }
            else result = false;

            if(result)
            {
                Constants.shared.DeleteLoginInformation();
                Constants.shared.InsertData("id", idEntry.Text);
                Constants.shared.InsertData("password", passwordEntry.Text);
                Application.Current.MainPage = new NavigationPage(new MainMenuPage());
            }
            else
            {
                await DisplayAlert("로그인 실패", "ID와 패스워드를 다시한번 확인 해 줄래요?", "네 ㅎ");
            }
        }
	}
}