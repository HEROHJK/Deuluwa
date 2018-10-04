using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeuluwaPIM.View
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow
    {
        public static bool loggined = false;
        public MainWindow()
        {
            InitializeComponent();
            LoginWindow lw = new LoginWindow();
            lw.ShowDialog();
            if (!loggined) { Close(); }

        }

        private void AccountManagement(object sender, RoutedEventArgs e)
        {
            //사용자 관리
        }

        private void CheckManagement(object sender, RoutedEventArgs e)
        {
            //출석 관리
        }

        private void CourseManagement(object sender, RoutedEventArgs e)
        {
            //수업 관리
        }

        private void ClassManagement(object sender, RoutedEventArgs e)
        {
            //교실 관리
        }

        private void Setting(object sender, RoutedEventArgs e)
        {
            //환경 설정
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            //로그 아웃
            loggined = false;
            Properties.Settings.Default["autologin"] = false;
            Properties.Settings.Default["id"] = null;
            Properties.Settings.Default["password"] = null;
            Properties.Settings.Default.Save();
            LoginWindow lw = new LoginWindow();
            lw.ShowDialog();
            if (!loggined) { Close(); }
        }
    }
}
