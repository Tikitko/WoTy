using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using System.Globalization;

namespace WoTy_Client
{
    class Game
    {
        private BackgroundWorker worker;
        private MainWindow _this;
        private Socket sender;
        private int Status;
        public Control co;

        private Tank tank1;
        private Tank tank2;
        public Game(MainWindow t, Socket s, Control c)
        {
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(workerF);
            co =c;
            _this = t;
            sender = s;
            Status = 0;
            
        }

        private List<List<string>> StrDecoder(string msg)
        {
            List<List<string>> ot = new List<List<string>>();
            string m = Regex.Match(msg, @"\[\[([^\[\]]+)\]\]").Groups[0].Value;
            string m1 = Regex.Match(m, @"([^\[\]]+)").Groups[0].Value.Replace(".",",");
            if (m1 == string.Empty) return null;
            List<string> z = m1.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string v in z)
                ot.Add(v.Split('*').ToList());
            return ot;
        }


        private void SocketCheckError(SocketException ex)
        {
            if (ex.ErrorCode != 0)
            {
                Status = 0;
                _this.Dispatcher.BeginInvoke((Action)delegate () {
                    _this.info.Content = "Ошибка: " + ex.Message;
                    _this.info.Visibility = Visibility.Visible;
                });
            }
        }

        private void UpdateForm(string str)
        {
            List<List<string>> ot = StrDecoder(str);

            if (ot != null)
            {
                _this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    switch (ot[0][0])
                    {
                        case "1":
                            _this.info.Content = "Приготовьтесь...";
                            break;
                        case "2":
                            _this.info.Content = "Бой!!! Ой, а пушки не стреляют... :(";
                            if (tank1.GetObject().GetObject().Visibility != Visibility.Visible)
                            {
                                _this.Dispatcher.BeginInvoke((Action)delegate ()
                                {
                                    tank1.GetObject().GetObject().Visibility = Visibility.Visible;
                                    tank2.GetObject().GetObject().Visibility = Visibility.Visible;
                                });
                            }

                            tank1.position_x = float.Parse(ot[1][0]);
                            tank1.position_y = float.Parse(ot[1][1]);
                            tank1.rotation_degree = float.Parse(ot[1][2]);
                            tank1.tower_rotation_degree = float.Parse(ot[1][3]);

                            tank2.position_x = float.Parse(ot[2][0]);
                            tank2.position_y = float.Parse(ot[2][1]);
                            tank2.rotation_degree = float.Parse(ot[2][2]);
                            tank2.tower_rotation_degree = float.Parse(ot[2][3]);


                            tank1.Move();
                            tank2.Move();
                            break;
                        case "3":
                            _this.info.Content = "Игра окончена!";
                            tank1.GetObject().GetObject().Visibility = Visibility.Hidden;
                            tank2.GetObject().GetObject().Visibility = Visibility.Hidden;
                            break;
                        case "4":
                            _this.info.Content = "Один из клиентов потерял соединение! Игра окончена...";
                            tank1.GetObject().GetObject().Visibility = Visibility.Hidden;
                            tank2.GetObject().GetObject().Visibility = Visibility.Hidden;
                            break;
                    }
                });
            }
        }

        private void workerF(object s, DoWorkEventArgs e)
        {
            SocketException ex = new SocketException();
            string temp, r;
            temp = r = string.Empty;

            while (SocketFunctions.Receive(sender, ref r, ref ex) && Convert.ToBoolean(Status))
            {
                UpdateForm(r);

                SocketException exe = new SocketException();
                string _out;  
                _out = "[[2||" + Convert.ToInt32(co._action) + "*" + Convert.ToInt32(co._turnLeft) + "*" + Convert.ToInt32(co._turnRight) + "*" + Convert.ToInt32(co._moveUp) + "*" + Convert.ToInt32(co._moveDown) + "*" + Convert.ToInt32(co._moveLeft) + "*" + Convert.ToInt32(co._moveRight) + "]]";
                SocketFunctions.Send(sender, _out, ref exe);
                Thread.Sleep(1);
            }
            SocketCheckError(ex);
        }

        public void Start()
        {
            Status = 1;

            tank1 = new Tank();
            tank1.Create();
            tank1.GetObject().GetObject().Visibility = Visibility.Hidden;
            _this.Dispatcher.BeginInvoke((Action)delegate () {
                _this.Map.Children.Add(tank1.GetObject().GetObject());
            });
            tank1.Move();


            tank2 = new Tank();
            tank2.Create();
            tank2.GetObject().GetObject().Visibility = Visibility.Hidden;
            _this.Dispatcher.BeginInvoke((Action)delegate () {
                _this.Map.Children.Add(tank2.GetObject().GetObject());
            });
            tank2.Move();

            worker.RunWorkerAsync();
        }
    }
}
