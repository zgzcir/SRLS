using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLRS_Server.Servers;
namespace SLRS_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server= new Server("127.0.0.1", 6689);//172.18.177.108//127.0.0.1
            server.Start();
            Console.ReadKey();
        }
    }
}
