using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLRS_Server.Model
{
    class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string NickName { get; set; }
        public bool IsLoginFirst { get; set; }


        public User(int id, string username, string password, string nickName = "null", bool ilf = true)
        {
            this.Id = id;
            this.UserName = username;
            this.Password = password;
            this.NickName = nickName;
            this.IsLoginFirst = ilf;
        }
    }
}