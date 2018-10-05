using DeuluwaPIM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DeuluwaPIM.View
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow
    {
        public static bool loggined = false;
        public static string userId = "";
        public static string userName = "";

        public MainWindow()
        {
            InitializeComponent();
            LoginWindow lw = new LoginWindow();
            lw.ShowDialog();
            if (!loggined) { Close(); }
            Task.Factory.StartNew(LoadNoticeDataThread);

            datagrid.RowStyle = Constants.MakeRowStyle(DataGridRow.MouseDoubleClickEvent, new MouseButtonEventHandler(DataGridCell_MouseDoubleClick));
            datagrid.RowHeight = 25;
        }

        private void LoadNoticeDataThread()
        {
            NoticeList.list = LoadData();

            if (datagrid.Dispatcher.CheckAccess())
            {
                datagrid.ItemsSource = NoticeList.list;
            }
            else datagrid.Dispatcher.BeginInvoke(new Action(LoadNoticeDataThread));
        }

        private List<NoticeMessage> LoadData()
        {
            string result = Model.Constants.HttpRequest("http://silco.co.kr:18000/notice");
            var array = JsonConvert.DeserializeObject<List<NoticeMessage>>(result);

            return array;
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
            Hide();

            loggined = false;
            Properties.Settings.Default["autologin"] = false;
            Properties.Settings.Default["id"] = null;
            Properties.Settings.Default["password"] = null;
            Properties.Settings.Default.Save();

            LoginWindow lw = new LoginWindow();
            lw.ShowDialog();

            if (!loggined) { Close(); }

            Show();
        }

        private void WriteButton_Click(object sender, RoutedEventArgs e)
        {
            NoticeWindow window = new NoticeWindow(true);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            NoticeMessage item = row.Item as NoticeMessage;

            NoticeWindow window = new NoticeWindow(false, item.index);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();

        }

        
    }
}
