using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SLRS_Server.Tool
{
    class DataBaseConnectTool
    {
        public const string CONNECTIONSTRING = "datasource=127.0.0.1;port=3306;database=slrs;user=root;pwd=a123456789...;Allow User Variables=True;";
        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTIONSTRING);
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine("Connect to DataBase wrong:" + e);
                return null;
            }

        }

        public static void CloseConnection(MySqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
            }
            else
            {
                Console.WriteLine("conn cant be null");
            }

        }

    }
}
