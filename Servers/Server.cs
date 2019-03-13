using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Common;
using SLRS_Server.Controller;

namespace SLRS_Server.Servers
{
    class Server
    {
        private IPEndPoint iPEndPoint;
        private Socket serverSocket;
        private List<Client> clientList = new List<Client>();
        private List<int> onLineUserIdList = new List<int>();
        private Dictionary<int, Client> UserIdClientDict = new Dictionary<int, Client>();
        private ControllerManager controllerManager;
        public Server() { }
        public Server(string ipStr, int port)
        {
            SetIpAndPort(ipStr, port);
            controllerManager = new ControllerManager(this);

        }

        public void UserOffline(int id)
        {
            onLineUserIdList.Remove(id);
            UserIdClientDict.Remove(id);
        }
        
        public Client GetOnlineClientById(int id)
        {
            Client client;
            UserIdClientDict.TryGetValue(id, out client);
            if (client == null)
            {
                foreach (Client c in clientList)
                {
                    if (c.ClientUserId == id)
                    {
                        client = c;
                        UserIdClientDict.Add(id, client);
                        return client;
                    }
                }
            }
            if (client == null)
            {
                return null;
            }
            else
            {
                return client;
            }
        }
        public void AddOnLineUserId(int id)
        {
            onLineUserIdList.Add(id);
        }
        public bool IsOnline(int id)
        {
            if (onLineUserIdList.Contains(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetIpAndPort(string ipStr, int port)
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(iPEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack, null);
            Console.WriteLine("服务器已启动...");
        }
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket, this);
            Console.WriteLine("新的客户端接入了！");

            client.Start();
            clientList.Add(client);

            serverSocket.BeginAccept(AcceptCallBack, null);

        }
        public void RemoveClient(Client client)
        {
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }

        public void SendResponse(Client client, RequestCode requestCode, string data)
        {
            try
            {

                client.Send(requestCode, data);
            }
            catch (Exception e)
            {
                Console.WriteLine("在Send Response时发生了错误:" + e);
            }
        }

        public void HandleRequest(ControllerCode controllerCode, RequestCode requestCode, string data, Client client)
        {
            controllerManager.HandleRequest(controllerCode, requestCode, data, client);
        }

    }
}
