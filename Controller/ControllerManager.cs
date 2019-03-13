using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using SLRS_Server.Servers;
using System.Reflection;

namespace SLRS_Server.Controller
{
    class ControllerManager
    {
        private Dictionary<ControllerCode, BaseController> controllerDict = new Dictionary<ControllerCode, BaseController>();
        private Server server;

        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }

        private void InitController()
        {

            controllerDict.Add(ControllerCode.User, new UserController());
            controllerDict.Add(ControllerCode.Chat, new ChatController());

        }
        public void HandleRequest(ControllerCode controllerCode, RequestCode requestCode, string data, Client client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(controllerCode, out controller);
            if (!isGet)
            {
                Console.WriteLine("Cant Get " + requestCode + " Handler");
                return;
            }
            string methodName = Enum.GetName(typeof(RequestCode), requestCode);
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("[warning]no method:<" + controller.GetType() + "." + methodName + ">");
            }
            Console.WriteLine(data);
            object[] parameters = new object[] { data, client, server };
            object o = mi.Invoke(controller, parameters);
            if (o == null || string.IsNullOrEmpty(o as string))
            {
                return;
            }
            server.SendResponse(client, requestCode, o as string);
        }
    }
}
