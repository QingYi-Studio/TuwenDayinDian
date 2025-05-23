using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TuwenDayinDian.Models;

namespace TuwenDayinDian.windows
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {

        private HttpClient client;
        private DispatcherTimer timer;
        public Login()
        {
            InitializeComponent();
            // 初始化HttpClient
            client = new HttpClient();

            // 初始化DispatcherTimer，每3秒钟发送一次请求
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {

            timer.Stop();

            // 打开主窗口（这里需要你的具体实现）
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            GlobalData.Name = "陈露娟";
            // 关闭当前二维码窗口
            this.Close();
            //try
            //{

            //    // 发送GET请求到指定URL
            //    HttpResponseMessage response = await client.GetAsync("http://127.0.0.1:8000/getLoginName");
            //    response.EnsureSuccessStatusCode();  // 确保请求成功

            //    // 读取响应内容
            //    string responseContent = await response.Content.ReadAsStringAsync();
            //    GlobalData.Name = responseContent;
            //    // 如果响应为"loginOk"，则打开主窗口
            //    if (responseContent.Trim().Equals("loginOk", StringComparison.OrdinalIgnoreCase))
            //    {
            //        // 停止定时器
            //        timer.Stop();

            //        // 打开主窗口（这里需要你的具体实现）
            //        MainWindow mainWindow = new MainWindow();
            //        mainWindow.Show();

            //        // 关闭当前二维码窗口
            //        this.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // 处理异常，例如网络连接失败等
            //    MessageBox.Show("Error: " + ex.Message);
            //}
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // 清理资源
            timer.Stop();
            client.Dispose();
        }

        private void close_setPrintParam(object sender, RoutedEventArgs e)
        {
            base.OnClosed(e);

            // 清理资源
            timer.Stop();
            client.Dispose();
            this.Close();
            
        }
    }
}
