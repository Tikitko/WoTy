using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WoTy_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title ="WoTy Server";
            Console.WriteLine("WoTy-Server V0.0.1 Pre Alpha");
            Console.WriteLine("Server Started!\n");
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n");
            
            (new Thread(new ThreadStart(new ClientsConnector().Connector))).Start();

            while (true)
               Console.ReadLine();
        }
    }
}
