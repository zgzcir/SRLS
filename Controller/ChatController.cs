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
        public ChatController()
        {
            controllerCode = ControllerCode.Chat;
        }

        public string SendChatMessage(string data, Client client, Server server)
        {

            string[] strs = data.Split(',');
            int id = int.Parse(strs[1]);
            Client targetClient = server.GetClientById(id);
            if (targetClient == null)
            {
                //TODO 如果对方不在线 将消息存入数据库等待发送
            }
            string message = strs[0] + ',' + client.ClientUserId.ToString();
            client.BroadcastMessage(targetClient, RequestCode.ReciveChatMessage, message);
            return null;
        }

    }
}
