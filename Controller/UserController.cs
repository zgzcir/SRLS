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
    class UserController : BaseController
    {
        private UserDAO userDAO = new UserDAO();

        public UserController()
        {
            controllerCode = ControllerCode.User;
        }
        public string Login(string data, Client client, Server server)
        {
            Console.WriteLine("Login方法被调用了!");
            string[] strs = data.Split(',');
            bool isHave = userDAO.VerifyUser(client.MySQLconn, strs[0], strs[1]);
            if (isHave)
            {
                User user = userDAO.GetByUserName(client.MySQLconn, strs[0]);
                string newData = user.Id.ToString() + ',' + user.UserName + ',' + user.NickName + ',' + user.IsLoginFirst.ToString() + ',' + ((int)ReturnCode.Success).ToString();
                client.ClientUserId = user.Id;
                server.AddOnLineUserId(user.Id);
                return newData;
            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }
        }
        public string Register(string data, Client client, Server server)
        {

            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];
            bool isSuccess = userDAO.AddUSer(client.MySQLconn, username, password);
            if (isSuccess)
            {
                return ((int)ReturnCode.Success).ToString();
            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();

            }
        }
        public string VerifyRepeat(string data, Client client, Server server)
        {
            Console.WriteLine(data);
            bool isRepate = userDAO.VerifyRepate(client.MySQLconn, data);
            if (isRepate)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                return ((int)ReturnCode.Success).ToString();
            }

        }

        public string SetFirstLoginInformation(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            int id = int.Parse(strs[0]);
            string nickname = strs[1];
            bool isSuccess = userDAO.SetFirstLoginInformationById(client.MySQLconn, id, nickname);
            if (isSuccess)
            {
                return ((int)ReturnCode.Success).ToString();

            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }
        }
        public string GetFriendList(string data, Client client, Server server)
        {

            Console.WriteLine(data);
            StringBuilder sb = new StringBuilder();
            int id = int.Parse(data);
            string s = userDAO.GetFriendsIdById(client.MySQLconn, id);
            if(!string.IsNullOrEmpty(s))
            {
                string[] strs = s.Split(',');
                foreach (string ids in strs)
                {
                    sb.Append(ids);
                    sb.Append(',');
                    sb.Append(userDAO.GetNicknameById(client.MySQLconn, int.Parse(ids)));
                    sb.Append(',');
                    if(server.IsOnline(int.Parse(ids)))
                    {
                        sb.Append('1');
                    }
                    else
                    {
                        sb.Append('0');
                    }
                    sb.Append('|');
                }
            }
            if(sb.Length>0)
            {
                sb.Remove(sb.Length - 1, 1);
                return ((int)ReturnCode.Success).ToString()+'#'+sb.ToString();
            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }




        }

      
    }
}
