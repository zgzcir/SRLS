using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SLRS_Server.DAO
{
    class ChatDAO
    {

        public void SaveMessage(MySqlConnection conn, string message, int sendId, int reciveId, DateTime dateTime)
        {

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into messages set message=@message,sendid=@sendid,reciveid=@reciveid,datetime=@datetime", conn);
                cmd.Parameters.AddWithValue("message", message);
                cmd.Parameters.AddWithValue("sendid", sendId);
                cmd.Parameters.AddWithValue("reciveid", reciveId);
                cmd.Parameters.AddWithValue("datetime", dateTime);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("当SaveMessage时出现异常：" + e);
            }


        }
        public string GetNotificationId(MySqlConnection conn, int reciveId)
        {
            StringBuilder sb = new StringBuilder();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select sendid from messages where reciveid=@reciveId and hasread=0", conn);
                cmd.Parameters.AddWithValue("reciveid", reciveId);
                reader = cmd.ExecuteReader();
                //bool isRead = reader.Read();
                while (reader.Read())
                {
                    sb.Append(reader.GetInt32("sendid"));
                    sb.Append(',');
                }
                reader.Close();
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("当GetNotification时出现异常：" + e);
                reader.Close();
                return null;
            }
        }
        public string GetReciveMessage(MySqlConnection conn, int sendId)
        {
            StringBuilder sb = new StringBuilder();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmdSelect = new MySqlCommand("SELECT message FROM messages where sendid=@sendId and hasread=0",conn);
                cmdSelect.Parameters.AddWithValue("sendid", sendId);
                reader = cmdSelect.ExecuteReader();
                while (reader.Read())
                {
                    sb.Append(reader.GetString("message"));
                    sb.Append(',');
                }
                reader.Close();
                if(sb.Length>0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                MySqlCommand cmdUpdate = new MySqlCommand("UPDATE messages SET hasread=1 WHERE  sendid=@sendId and hasread=0", conn);//and hasread=0
                cmdUpdate.Parameters.AddWithValue("sendid", sendId);
                cmdUpdate.ExecuteNonQuery();
                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("当GetReciveMessage时出现异常：" + e);
                reader.Close();
                return null;
            }
        }

    }
}
