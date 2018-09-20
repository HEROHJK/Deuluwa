using Poz1.NFCForms.Abstract;
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
    public partial class AttendanceCheckPage : ContentPage
    {
        private readonly INfcForms device;

        public AttendanceCheckPage()
        {
            InitializeComponent();
            latitude.IsVisible = false;
            longitude.IsVisible = false;
            device = DependencyService.Get<INfcForms>();
            device.NewTag += HandleNewTag;

        }

        

        void HandleNewTag(object sender, NfcFormsTag e)
        {
            longitude.IsVisible = true;
            latitude.IsVisible = true;

            longitude.Text = Encoding.Default.GetString(e.NdefMessage[0].Type);
            latitude.Text = Encoding.Default.GetString(e.NdefMessage[0].Payload).Substring(3);
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Back();
        }

        private void Back()
        {
            Navigation.PopAsync();
        }
    }
}