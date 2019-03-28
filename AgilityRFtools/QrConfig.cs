using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilityRFtools
{
    internal class QrConfig
    {
        public static string DBlocal = "Integrated Security=False;Data Source=10.130.24.4;Initial Catalog=KaizenDB;User ID=kadmin;Password=53c4dm1n;";
        public static string DBUser = "Integrated Security=False;Data Source=10.130.24.4;Initial Catalog=KaizenDB;User ID=kadmin;Password=53c4dm1n;";
        public static string Delimiter;
        public static Int32 AssySeq ;
        public static Int32 CartonIDSeq;
        public static string Carline;
        public string ASN_No;

        public void getConfig()
        {

            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select *  from TbPlBSami_FG_QRConfig", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                AssySeq = reader.GetInt32(3);
                CartonIDSeq = reader.GetInt32(4);
                Carline = reader.GetString(2);
                Delimiter = reader.GetString(6);

            }
        }
    }



}
