using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deuluwa
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainMenuPage : ContentPage
	{
        public ObservableCollection<ButtonCell> cells = new ObservableCollection<ButtonCell>();
		public MainMenuPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            nameLabel.Text = "김호종";
            logoutButton.Clicked += LogoutButton_Clicked;
            mainTableView.RowHeight = (int)App.Current.MainPage.Height / 7;
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("로그아웃", "로그아웃 하시겠습니까?", "예", "아뇨");
            if (answer)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }

        private void AttendanceTapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AttendanceCheckPage());
        }
    }
}