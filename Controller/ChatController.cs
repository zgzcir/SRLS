using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using SLRS_Server.Servers;
using SLRS_Server.Model;
using SLRS_Server.DAO;
namespace SLRS_Server.Controller
{
    class ChatController : BaseController
    {
        private ChatDAO chatDAO = new ChatDAO();

        public ChatController()
        {
            controllerCode = ControllerCode.Chat;
        }

        public string SendAndSaveChatMessage(string data, Client client, Server server)
        {

            string[] strs = data.Split(',');
            int id = int.Parse(strs[1]);
            chatDAO.SaveMessage(client.MySQLconn, strs[0], client.ClientUserId, id, DateTime.Now);
            Client targetClient = server.GetOnlineClientById(id);
            {
                if(targetClient!=null)
                {
                    string broadcastData = strs[0] +','+ client.ClientUserId.ToString();
                    client.BroadcastMessage(targetClient, RequestCode.SendMessage, broadcastData);
                }
            }
            //if (targetClient == null)
            //{
           //    //TODO 
            //}
            //string message = strs[0] + ',' + client.ClientUserId.ToString();
            //client.BroadcastMessage(targetClient, RequestCode.ReciveChatMessage, message);
            return null;
        }

      

        //public void SendMessage(string data, Client client, Server server)
        //{
        //    int id = int.Parse(data);
        //    Client targetClient = server.GetOnlineClientById(id);
        //    string messages = chatDAO.GetReciveMessage(client.MySQLconn, id);
        //    //if (targetClient != null)
        //    //{
        //    //    client.BroadcastMessage(targetClient, RequestCode.ShowMessage, messages);
        //    //}

        //}
    }
}
