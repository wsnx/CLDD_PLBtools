using System;
using System.Data;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class Generate
    {
        SqlConnection Conn = new SqlConnection(ConfigDB.conWMS);
        SqlConnection ConnLocal = new SqlConnection(ConfigDB.DBlocal);
        DataSet DsWMS = new DataSet();

        private static string txt_Carmaker;
        private static string txt_SKU;
        private static string txt_AssyCode;
        private static string txt_startSerial;
        private static string txt_EndSerial;
        private static int txt_SNP;
        private static string txt_Storerkey;

        public void CreateTask()
        {

            DsWMS.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = ConnLocal;
            cmd.CommandText = "select * from tbMappingInstruction";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);

            DA.Fill(DsWMS);

            int a = DsWMS.Tables[0].Rows.Count;
            for (int i = 0; i < a; i++)
            {
                string aaa = DsWMS.Tables[0].Rows[i]["SNP"].ToString();
                txt_SNP = Convert.ToInt32(aaa);
                txt_startSerial = DsWMS.Tables[0].Rows[i]["Startserial"].ToString();
                txt_SKU = DsWMS.Tables[0].Rows[i]["AssyNumber"].ToString();
                txt_EndSerial = DsWMS.Tables[0].Rows[i]["EndSerial"].ToString();
                txt_Storerkey = DsWMS.Tables[0].Rows[i]["Factory"].ToString();
                GenerateMapping();
            }
        }

        private void GenerateMapping()
        {
            int x = Convert.ToInt32(txt_startSerial);
            for (int i = x; i < ((x) + txt_SNP); i++)
            {
                int b = i.ToString().Length;
                int a = txt_startSerial.Length;
                string c = txt_startSerial.Substring(0, a - b);
                txt_startSerial = c.ToString() + i.ToString();
                ConnLocal.Close();
                SqlCommand cmd = new SqlCommand("insert into   tbMappingID (MappingID,CartonID,SKU) values(@MappingID,@CartonID,@SKU)", ConnLocal);
                cmd.Parameters.Add(new SqlParameter("MappingID", "1900001"));
                cmd.Parameters.Add(new SqlParameter("CartonID", txt_startSerial));
                cmd.Parameters.Add(new SqlParameter("SKU", txt_SKU));
                ConnLocal.Open();
                cmd.ExecuteNonQuery();
                ConnLocal.Close();
                Console.WriteLine(txt_startSerial);
            }
        }

    }
}


