using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Deuluwa
{
    class LoginManager
    {
        public static async void AutoLoginDisable()
        {
            Application.Current.Properties["autoLoginID"] = "";
            Application.Current.Properties["autoLoginPW"] = "";
            Application.Current.Properties["autoLogin"] = "FALSE";
            await Application.Current.SavePropertiesAsync();
        }

        public static async void AutoLoginEnable(string id, string password)
        {
            Application.Current.Properties["autoLoginID"] = id;
            Application.Current.Properties["autoLoginPW"] = password;
            Application.Current.Properties["autoLogin"] = "TRUE";
            await Application.Current.SavePropertiesAsync();
        }

    }
}
