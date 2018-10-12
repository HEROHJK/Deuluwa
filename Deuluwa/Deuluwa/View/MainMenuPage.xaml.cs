using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeuluwaCore.Model;

namespace Deuluwa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : ContentPage
    {
        HttpClient client = new HttpClient
        {
            MaxResponseContentBufferSize = 256000
        };

        bool backButtonPressed = false;

        public ObservableCollection<ButtonCell> cells = new ObservableCollection<ButtonCell>();
        public MainMenuPage()
        {
            InitializeComponent();
            logoutButton.Clicked += LogoutButton_Clicked;
            mainTableView.RowHeight = (int)App.Current.MainPage.Height / 7;
            DataLoad();
            DependencyService.Get<ToastMessage>().LongToast("접속 되었습니다!");
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("로그아웃", "로그아웃 하시겠습니까?", "예", "아뇨");
            if (answer)
            {
                LoginManager.AutoLoginDisable();
                Application.Current.MainPage = new LoginPage();
            }
        }

        private void ClassInformationTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ClassInformationPage());
        }

        private void AttendanceTapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AttendanceCheckPage());
        }

        public async void DataLoad()
        {
            string url = DeuluwaCore.Constants.shared.GetData("url") + "userinfo/?id=" + DeuluwaCore.Constants.shared.GetData("id");

            var uri = new Uri(url);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    Dictionary<string, string> info = DeuluwaCore.Controller.JsonConverter.GetDictionary(content);
                    nameLabel.Text = info["name"];
                    addressLabel.Text = info["address"];
                    phoneNumberLabel.Text = info["phonenumber"];
                }
                catch
                {
                    await DisplayAlert("인증 오류", "사용자 정보를 불러올 수 없네요?\r\n다시 로그인 해 주시겠어요?", "네 ㅎ");
                    Application.Current.MainPage = new LoginPage();
                }
            }
            else
            {
                await DisplayAlert("접속 오류", "서버에 접속이 잘 안되네요?\r\n나중에 다시 사용 해 주시겠어요?", "네 ㅎ");
                Application.Current.MainPage = new LoginPage();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            if (!backButtonPressed)
            {
                DependencyService.Get<ToastMessage>().ShortToast("한번 더 누르면 프로그램을 종료합니다.");
                backButtonPressed = true;
                //스레드 실행
                Thread t1 = new Thread(new ThreadStart(Run))
                {
                    IsBackground = true
                };
                t1.Start();
                return true;
            }
            else return false;
        }

        private void Run()
        {
            Thread.Sleep(1000);
            backButtonPressed = false;
        }
    }
}