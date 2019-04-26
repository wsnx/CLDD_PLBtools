using System; 
using System.Data.SqlClient;
namespace AgilityRFtools
{
    class Parser
    {
        public  static string QRinput;
        public static string SKU;
        public static string CartonNo;
        public static string DestinationCode;
        public static string Delimiter;
        public static Int32 AssySeq;
        public static Int32 CartonIDSeq;
        public static Int32 Qty;
        public static string Carline;
        public void Load()
        {
            ParserRegulerProduct();
        }
        private void ParserRegulerProduct()
        {
            string a = QRinput.Substring(0, 2);
            if (a == "TF" || a == "SM")
            {

                string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                SKU = b[3].ToString();
                CartonNo = b[4].ToString();
                DestinationCode = b[5].ToString();
                string x = b[6].ToString();
                Qty = Convert.ToInt32(x);
                Handler();

            }
            else
            {

            }
         
        }
        private void Handler()
        {
            if (MainMenu.FormName == "Unload Form")
            {

                Console.SetCursorPosition(0, 7);
                Console.WriteLine("Transmiting Data");
                UnloadForm f = new UnloadForm();
                f.UpdateSql();
            }
            else if (MainMenu.FormName == "Mapping Cek")
            {
                PalletChceking l = new PalletChceking();
                l.ResultParsing();
            }
        }
            /*
        private void QRParser()
        {
             string Qr = "TF,1706,,,BJP8,397A3-5TL1A 0150,000630,170721,09";
            //Get Data QR Config
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select *  from TbPlBSami_FG_QRConfig", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string sku=(SKU = reader.GetString(1));
                if (Qr.Contains(sku))
                {
                    AssySeq = reader.GetInt32(3);
                    CartonIDSeq = reader.GetInt32(4);
                    Delimiter = reader.GetString(6);
                    SKU = reader.GetString(1);
                    goto ParserQR;
                }
                else
                {
                    Load();
                }
            }
            ParserQR:
            string[] HasilScan = Qr.Split(new string[] { Delimiter }, StringSplitOptions.None);
            string txt_sku = HasilScan[AssySeq].ToString();
            string txt_CartonID = HasilScan[CartonIDSeq].ToString();
            Console.WriteLine(txt_sku);
            Console.WriteLine(txt_CartonID);
            Console.ReadLine();
        }
        */
    }    
}
