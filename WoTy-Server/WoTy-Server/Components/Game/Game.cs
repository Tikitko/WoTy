using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace WoTy_Server
{
    class Game
    {
        private List<ClientSandbox> ClientsInGame;
        private World w = new World();
        private Tank tank1;
        private Control Control1 = new Control();
        private Tank tank2;
        private Control Control2 = new Control();

        private List<float> Results;
        public Game(ref List<ClientSandbox> Clients, List<int> Lobby)
        {
            ClientsInGame = new List<ClientSandbox>();
            w = new World();
            tank1 = new Tank();
            tank1.Create(Control1);
            /* kostil */ tank1.position_x = -300;
            tank1.Move();
            w.AddTank(tank1);
            tank2 = new Tank();
            tank2.Create(Control2);
            /* kostil */ tank2.position_x = 300;
            /* kostil */ tank2.rotation_degree = 180;
            tank2.Move();
            w.AddTank(tank2);
            foreach (int v in Lobby)
            {
                ClientsInGame.Add(Clients[v]);
                ClientsInGame[ClientsInGame.Count - 1].ExternalStatus = 4;
                ClientsInGame[ClientsInGame.Count - 1].RSEH += new ClientSandbox.ReceiveStringEventHandler(MsgFromClient);


            }
            new Thread(new ThreadStart(Start)).Start();
            Results = new List<float>();
        }

        private void MsgFromClient(object sender, string msg)
        {
            string m = Regex.Match(msg, @"\[\[([^\[\]]+)\]\]").Groups[0].Value;
            string m1 = Regex.Match(m, @"([^\[\]]+)").Groups[0].Value;
            List<string> k = m1.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> z = k[1].Split(new string[] { "*" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (m1 != string.Empty && sender == ClientsInGame[0] && z.Any())
            {
                Control1._action = Convert.ToBoolean(Convert.ToInt32(z[0]));
                Control1._turnLeft = Convert.ToBoolean(Convert.ToInt32(z[1]));
                Control1._turnRight = Convert.ToBoolean(Convert.ToInt32(z[2]));
                Control1._moveUp = Convert.ToBoolean(Convert.ToInt32(z[3]));
                Control1._moveDown = Convert.ToBoolean(Convert.ToInt32(z[4]));
                Control1._moveLeft = Convert.ToBoolean(Convert.ToInt32(z[5]));
                Control1._moveRight = Convert.ToBoolean(Convert.ToInt32(z[6]));
            }
            if (m1 != string.Empty && sender == ClientsInGame[1] && z.Any())
            {
                Control2._action = Convert.ToBoolean(Convert.ToInt32(z[0]));
                Control2._turnLeft = Convert.ToBoolean(Convert.ToInt32(z[1]));
                Control2._turnRight = Convert.ToBoolean(Convert.ToInt32(z[2]));
                Control2._moveUp = Convert.ToBoolean(Convert.ToInt32(z[3]));
                Control2._moveDown = Convert.ToBoolean(Convert.ToInt32(z[4]));
                Control2._moveLeft = Convert.ToBoolean(Convert.ToInt32(z[5]));
                Control2._moveRight = Convert.ToBoolean(Convert.ToInt32(z[6]));
            }
        }

        private void SenderInformationInstaller(int type)
        {
            List<List<float>> k;
            List<float> t, v;
            for (int i = 0; i < ClientsInGame.Count; i++)
            {
                k = new List<List<float>>();
                t = new List<float>();
                v = new List<float>();
                switch (type)
                {
                    case 1:
                    case 4:
                        k.Add(new List<float>() { type });
                        ClientsInGame[i].SetSendString(FormatList(k));
                        break;
                    case 2:
                        k.Add(new List<float>() { type });

                            t.Add(tank1.position_x);
                            t.Add(tank1.position_y);
                            t.Add(tank1.rotation_degree);
                            t.Add(tank1.tower_rotation_degree);
                        
                            v.Add(tank2.position_x);
                            v.Add(tank2.position_y);
                            v.Add(tank2.rotation_degree);
                            v.Add(tank2.tower_rotation_degree);
                        /*if(i == 0)
                        {
                            k.Add(t);
                            k.Add(v);
                        }
                        if(i == 1)
                        {
                            k.Add(v);
                            k.Add(t);
                        }*/
                        k.Add(t);
                        k.Add(v);

                        ClientsInGame[i].SetSendString(FormatList(k));
                        break;
                    case 3:
                        k.Add(new List<float>() { type });
                        k.Add(Results);
                        ClientsInGame[i].SetSendString(FormatList(k));
                        break;
                }
            }
        }

        private string FormatList(List<List<float>> l)
        {
            string r = string.Empty;
            for (int i = 0; i < l.Count; i++)
            {
                for (int j = 0; j < l[i].Count; j++)
                {
                    r += l[i][j].ToString();
                    if (j != l[i].Count - 1)
                        r += "*";
                }
                if(i != l.Count - 1)
                    r += "||";
            }
            return "[[" + r + "]]";
        } 

        public void Start()
        {
            Console.Write("Game with clients ");
            foreach (ClientSandbox v in ClientsInGame)
                Console.Write(v.ClientName + " ");
            Console.WriteLine("started!");
            SenderInformationInstaller(1);
            Thread.Sleep(5000);
            

            while(true)
            {
                foreach (ClientSandbox v in ClientsInGame)
                    if (v.GetStatus() == 0)
                    {
                        Console.Write("Game with clients ");
                        foreach (ClientSandbox vi in ClientsInGame)
                            Console.Write(vi.ClientName + " ");
                        Console.WriteLine("ended because of loss of connection.");
                        SenderInformationInstaller(4);
                        Thread.Sleep(5000);
                        goto gend;
                    }
                SenderInformationInstaller(2);
                w.Move(tank1);
                w.Move(tank2);
                
                Thread.Sleep(15);

                /*if (Results.Count == ClientsInGame.Count)
                {
                    Console.Write("Game with clients ");
                    foreach (ClientSandbox v in ClientsInGame)
                        Console.Write(v.ClientName + " ");
                    Console.WriteLine("ended!");
                    SenderInformationInstaller(3);
                    Thread.Sleep(5000);
                    goto gend;
                }*/
            }
            gend:
            foreach (ClientSandbox v in ClientsInGame)
                v.ExternalStatus = 4;
        }
    }
}
