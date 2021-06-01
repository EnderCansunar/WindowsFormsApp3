using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace WindowsFormsApp3
{
    class db
    {
        public static string ConStr1 = ConfigurationManager.ConnectionStrings["Mysql1"].ToString();

        public MySqlConnection baglantiinvise = new MySqlConnection(ConStr1);
        public string baglanti_kontrolinvise()
        {
            try
            {
                baglantiinvise.Open();
                return "true";


            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
    }

     }
}
