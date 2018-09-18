using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Deuluwa.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidToastMessage))]
namespace Deuluwa.Droid
{
    public class AndroidToastMessage : ToastMessage
    {
        public void LongToast(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortToast(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}