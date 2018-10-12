using System;
using System.Net.Http;
using System.Threading.Tasks;
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
                MaxResponseContentBufferSize = 256000,
                Timeout = TimeSpan.FromSeconds(3)
            };

            idEntry.Completed += IdEntry_Completed;
            passwordEntry.Completed += PasswordEntry_Completed;
            loginButton.Clicked += LoginButton_Clicked;
            

            if(IsAutoLogin())
            {
                //자동로그인일시
                string id = Application.Current.Properties["autoLoginID"].ToString();
                string password = Application.Current.Properties["autoLoginPW"].ToString();
                autoLoginSwitch.IsToggled = true;
                Login(id, password);
            }

        }

        private void AutoLoginSwitch_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private bool IsAutoLogin()
        {
            try
            {
                Console.WriteLine(Application.Current.Properties["autoLogin"].ToString());

                if (Application.Current.Properties["autoLogin"].ToString() == "TRUE")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
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
            string url = DeuluwaCore.Constants.shared.GetData("url") + string.Format("user/?id={0}&password={1}", id, password);

            var uri = new Uri(url);

            bool result = false;
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    if (content.IndexOf("success") >= 0) result = true;
                    else result = false;
                }
                else result = false;

                if (result)
                {
                    Console.WriteLine("스위치 토글 여부 : " + autoLoginSwitch.IsToggled);
                    //로그인 정보 저장
                    if (autoLoginSwitch.IsToggled)
                    {
                        LoginManager.AutoLoginEnable(id, password);
                    }
                    else
                    {
                        LoginManager.AutoLoginDisable();
                    }

                    DeuluwaCore.Constants.shared.DeleteLoginInformation();
                    DeuluwaCore.Constants.shared.InsertData("id", id);
                    DeuluwaCore.Constants.shared.InsertData("password", password);
                    Application.Current.MainPage = new NavigationPage(new MainMenuPage())
                    {
                        BarTextColor = Color.White,
                    };
                }
                else
                {
                    await DisplayAlert("로그인 실패", "ID와 패스워드를 다시한번 확인 해 줄래요?", "네 ㅎ");
                }
            }
            catch
            {
                await DisplayAlert("로그인 실패", "서버와 통신이 원할하지 않네요, 잠시후 다시 접속 해 줄래요?", "네 ㅎ");
            }
        }
    }
}