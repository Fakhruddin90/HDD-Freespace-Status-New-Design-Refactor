using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HDD_Freespace_Status_Design_1
{
    public class Connection
    {
        public static void SQLConnection()
        {
            string server = "localhost";
            string user = "root";
            string database = "database";
            int port = 3306;
            string password = "";

            string connStr = $"server={server};user={user};database={database};port={port};password={password}";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                MessageBox.Show("Connecting to MySQL...");
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            conn.Close();
            MessageBox.Show("Disconnected from MySQL");
        }
    }
}
