using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace OnlineCinema.Web.SqlDbUtils
{
    public class MySqlDbUtil
    {
        public static MySqlConnection GetConnection()
        {
            string connectionString = "Server=localhost;DataBase=online_cinema;UId=root;Pwd=0000";
            return new MySqlConnection(connectionString);
        }
    }
}
