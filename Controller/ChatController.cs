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
    class ChatController:BaseController
    {
        public ChatController()
        {
            controllerCode = ControllerCode.Chat;
        }

        public string SendChatMessage(string data, Client client, Server server)
        {

            //TODO
            return null;
        }

    }
}
