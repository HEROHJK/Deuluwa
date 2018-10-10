using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeuluwaPIM.View
{
    /// <summary>
    /// UserControlWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControlWindow
    {
        public UserControlWindow()
        {
            InitializeComponent();
        }

        private async void GetUserList()
        {
            await Model.Constants.HttpRequest("http://silco.co.kr:18000/userlist");
        }

        private void UserDataGridViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
