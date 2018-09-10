using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deuluwa
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
            InitializeComponent ();
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
            DisplayAlert("미 구현", "구현 중입니다", "확인");
        }
	}
}