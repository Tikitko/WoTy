using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Windows.Threading;

namespace WoTy_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Socket sender;
        private Control c;

        public MainWindow()
        {
            InitializeComponent();

            c = new Control();
        }

        private async void button_Click(object s, RoutedEventArgs e)
        {
            try
            {
                start.IsEnabled = false;
                start.Content = "Подключение к серверу...";
                sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(new IPEndPoint(IPAddress.Parse("82.146.32.134"), 27018)); // 82.146.32.134
                start.Content = "Соединение установлено!";
                await Task.Delay(2000);
                start.Visibility = Visibility.Hidden;
                logo.Visibility = Visibility.Hidden;
                key.Visibility = Visibility.Hidden;
                info.Content = "Поиск игры! Пожалуйста подождите...";
                (new Game(this, sender, c)).Start();
            }
            catch (Exception ex)
            {
                start.Content = "Ошибка: " + ex.Message;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            c.KeyDown(e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            c.KeyUp(e);
        }
    }
}
