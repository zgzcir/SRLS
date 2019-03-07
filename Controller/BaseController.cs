using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using SLRS_Server.Servers;
namespace SLRS_Server.Controller
{
    abstract class BaseController
    {
        protected ControllerCode controllerCode = ControllerCode.None;
        public ControllerCode ControllerCode
        {
            get
            {
                return controllerCode;
            }
        }
        public virtual string DefaultHandale(string data, Client client, Server server)
        {
            return null;
        }

    }
}
