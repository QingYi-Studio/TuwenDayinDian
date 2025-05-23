using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
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

namespace TuwenDayinDian
{
    /// <summary>
    /// WebSocket.xaml 的交互逻辑
    /// </summary>
    public partial class WebSocket : Page
    {
        private ClientWebSocket _webSocket;
        public WebSocket()
        {
            InitializeComponent();
            _webSocket = new ClientWebSocket();
        }

   
        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Uri serverUri = new Uri("ws://127.0.0.1:8000/websocket?userId=25107671"); // WebSocket服务器地址
                await _webSocket.ConnectAsync(serverUri, CancellationToken.None);
                StatusTextBlock.Text = "Connected";
                ConnectButton.IsEnabled = false;
                SendButton.IsEnabled = true;
                ReceiveButton.IsEnabled = true;
                await ReceiveMessages(); // 启动接收消息的异步任务
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = MessageTextBox.Text;
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
                await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                StatusTextBlock.Text = "Message sent";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ReceiveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] buffer = new byte[1024];
                WebSocketReceiveResult result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                MessageBox.Show("Received message: " + message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private int i = 0;

        private async Task ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            while (_webSocket.State == WebSocketState.Open)
            {
                try
                {
                    WebSocketReceiveResult result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    string message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                    
                    Console.WriteLine(message+i);
                    i++;
                    //MessageBox.Show("Received message: " + message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }









    }
}
