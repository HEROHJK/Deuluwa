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
	public partial class ClassInformationPage : ContentPage
	{
        CustomClassViewModel vm;
		public ClassInformationPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            vm = new CustomClassViewModel();
            listView.ItemsSource = vm.customClasses;
		}
	}
}