using DeuluwaPIM.Model;
using Newtonsoft.Json;
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
    /// NoticeWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NoticeWindow
    {
        bool writeMode;
        public NoticeWindow(bool writeMode, string index="")
        {
            InitializeComponent();
            this.writeMode = writeMode;
            WriteMode(this.writeMode, index);
            Task.Factory.StartNew(LoadNoticeDataThread);
            indexColumn.Visibility = Visibility.Hidden;
            datagrid.RowHeight = 25;
        }

        private void WriteMode(bool writeMode, string index="")
        {
            this.writeMode = writeMode;
            textBox.Document.Blocks.Clear();

            if (this.writeMode)
            {
                textBox.IsReadOnly = false;
                writerLabel.Content = MainWindow.userName;
                writeButton.Content = "작성 완료";
            } else
            {
                textBox.IsReadOnly = true;
                writeButton.Content = "신규 작성";
                foreach(NoticeMessage message in NoticeList.list)
                {
                    if(index == message.index)
                    {
                        textBox.Document.Blocks.Add(new Paragraph(new Run(message.message)));
                        writerLabel.Content = message.writer;
                        dateLabel.Content = message.date;
                        break;
                    }
                }
            }
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

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NoticeMessage message = datagrid.SelectedItem as NoticeMessage;
            if (writeMode)
            {
                if(MessageBox.Show("작성중이던 글을 취소하겠습니까?", "작성중이던 글", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
            }
            WriteMode(false, message.index);
        }

        private void writeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if((string)button.Content == "신규 작성")
            {
                WriteMode(true);
            }
            else
            {
                if (MessageBox.Show("글을 작성하시겠습니까?", "공지 작성", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string postData = string.Format("id={0}", MainWindow.userId);
                    postData += string.Format("&message={0}", new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd).Text);
                    string result = Constants.HttpRequestPost("http://silco.co.kr:18000/writenoticemessage/", postData);
                    try
                    {
                        NoticeList.list = JsonConvert.DeserializeObject<List<NoticeMessage>>(result);

                        WriteMode(false, NoticeList.list[0].index);

                        datagrid.ItemsSource = NoticeList.list;
                    }
                    catch
                    {
                        MessageBox.Show("글 작성 실패");
                    }

                }
            }
        }
    }
}
