using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using MySql.Data.MySqlClient;
using Common;
using SLRS_Server.Tool;
using SLRS_Server.Model;
namespace SLRS_Server.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private MySqlConnection mySqlConn;

        private int  clientUserId;

        private Message msg = new Message();
        public Client() { }
        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            mySqlConn = DataBaseConnectTool.Connect();
        }
        public MySqlConnection MySQLconn
        {
            get
            {
                return mySqlConn;
            }
        }

        public void Start()
        {
            if (clientSocket == null || clientSocket.Connected == false)
            {
                Console.WriteLine("未找到客户端或客户端远端连接已关闭");
                return;
            }
            clientSocket.BeginReceive(msg.data, msg.DynamicLength, msg.RemainSize, SocketFlags.None, ReciveCallBack, null);

        }

        public void ReciveCallBack(IAsyncResult ar)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false)
                {
                    Console.WriteLine("未找到客户端或客户端远端连接已关闭");
                    return;
                }
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                }
                msg.ReadMessage(count, OnProcessMessage);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }
        public void Send(RequestCode requestCode, string data)
        {
            try
            {
                Console.WriteLine("向客户端发送了一条消息:" + data + "    >>>" + requestCode + "<<<");
                byte[] bytes = Message.PackData(requestCode, data);
                clientSocket.Send(bytes);
            }
            catch (Exception e)
            {
                Console.WriteLine("无法发送消息:" + e);
            }

        }
        private void OnProcessMessage(ControllerCode controllerCode, RequestCode requestCode, string data)
        {
            server.HandleRequest(controllerCode, requestCode, data,this);        }
        private void Close()
        {
            DataBaseConnectTool.CloseConnection(mySqlConn);
            if(clientSocket!=null)
            {
                clientSocket.Close();
                Console.WriteLine("一个远程连接已关闭");
            }

        }
    }
}
