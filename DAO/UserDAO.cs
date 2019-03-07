using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SLRS_Server.Model;
namespace SLRS_Server.DAO
{
    class UserDAO
    {
        public User GetByUserName(MySqlConnection conn, string username)
        {
            MySqlDataReader reader = null;
            try
            {


                MySqlCommand cmd = new MySqlCommand("Select * from user where username =@username", conn);
                cmd.Parameters.AddWithValue("username", Encoding.UTF8.GetBytes(username));

                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    int id = reader.GetInt32("id");
                    string userName = reader.GetString("username");
                    string nickName = reader.GetString("nickname");
                    bool ilf = reader.GetBoolean("isfirstlogin");
                    return new User(id, userName, null, nickName, ilf);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetByUserName时发生了错误:" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return null;

        }
        public string GetNicknameById(MySqlConnection conn, int id)
        {
            MySqlDataReader reader = null;
            try
            {


                MySqlCommand cmd = new MySqlCommand("Select * from user where id =@id", conn);
                cmd.Parameters.AddWithValue("id",id);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    
                    string nickname = reader.GetString("nickname");
                    return nickname;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetNicknameById时发生了错误:" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return null;

        }

        public bool AddUSer(MySqlConnection conn, string username, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into user set username=@username,password=@password", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("当AddUser时出现异常：" + e);
                return false;
            }

        }

        //public bool VerifyRepate(MySqlConnection conn,string username)
        //{
        //    MySqlDataReader reader = null;
        //    try
        //    {
        //        Console.WriteLine(username);

        //        MySqlCommand cmd = new MySqlCommand("Select * from user where username =@username", conn);
        //        cmd.Parameters.AddWithValue("username", Encoding.UTF8.GetBytes(username));
        //        reader = cmd.ExecuteReader();
        //        if(reader.Read())
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("当verifrepate时出现异常：" + e);
        //        return false;
        //    }
        //    finally
        //    {
        //        if (reader != null)
        //        {
        //            reader.Close();
        //        }
        //    }
        //}
        public bool VerifyUser(MySqlConnection conn, string username, string password)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * from user where username =@username and password=@password", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                reader = cmd.ExecuteReader();
                bool isRead = reader.Read();
                if (isRead)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("当verifiuser时出现异常：" + e);

            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return false;

        }
        public bool VerifyRepate(MySqlConnection conn, string username)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * from user where username =@username", conn);
                cmd.Parameters.AddWithValue("username", username);
                reader = cmd.ExecuteReader();
                bool isRead = reader.Read();
                if (isRead)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("当verifiuser时出现异常：" + e);

            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return false;

        }

        public bool SetFirstLoginInformationById(MySqlConnection conn, int id, string nickName)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE user SET nickname=@nickname, isfirstlogin=0 WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("nickname", nickName);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("当SetFirstLoginInformationById时出现异常：" + e);
                return false;
            }
        }


        public string GetFriendsIdById(MySqlConnection conn, int id)
        {
            StringBuilder sb = new StringBuilder();
            MySqlDataReader reader1 = null;
            MySqlDataReader reader2 = null;
            try
            {
                MySqlCommand cmd1 = new MySqlCommand("Select * from friends where leftid =@id", conn);
                MySqlCommand cmd2 = new MySqlCommand("Select * from friends where rightid =@id", conn);
                cmd1.Parameters.AddWithValue("id", id);
                cmd2.Parameters.AddWithValue("id", id);
                reader1 = cmd1.ExecuteReader();

                while (reader1.Read())
                {
                    sb.Append(reader1.GetInt32("rightid"));
                    sb.Append(',');
                }
                reader1.Close();
                reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    sb.Append(reader2.GetInt32("leftid"));
                    sb.Append(',');
                }
                reader2.Close();
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                else
                {
                    sb.Append('r');
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("当GetFriendsIdById时出现异常：" + e);
                reader1.Close();
                reader2.Close();
                return null;
            }

        }
    }
}
