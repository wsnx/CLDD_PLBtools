using System;
using System.Data.SqlClient;
using System.Drawing;
using Console = Colorful.Console;

namespace AgilityRFtools
{

    class UnloadForm : QrConfig
    {
        private static string ASN="";
        private static string CartonID="";
        private static string Carline="";
        private static string NIK = "18.332";
        private static string UserName="Wisnu" ;
        private static string PalletID="";
        private static string SKU="";
        private static int Qty=10;
        private static DateTime Editdate = DateTime.Now;
        private static string key;
        private static string strKey = "";
        private static Int32 Expected;
        private static Int32 Received;
        public void Frame()
        {
            Console.BackgroundColor = Color.WhiteSmoke;
            Console.ForegroundColor = Color.Black;
            Console.Clear();
            Console.BackgroundColor = Color.DarkOrange;
            
            Console.ForegroundColor = Color.Black;
            Console.WriteLine("           Unload Form           ");
            Console.WriteLine("_________________________________");

            Console.BackgroundColor = Color.WhiteSmoke;

        }
        public void Start()
        {
            Frame();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Entry ASN    :");
            ASNform();
        }
        public void FormHeader ()
        {
            SumDetails();
            Console.Clear();
            Frame();
            Console.SetCursorPosition(0,2);
            Console.WriteLine("ASN No.      :");
            Console.WriteLine("No Mobil     :");
            Console.WriteLine("Carline Type :");
            Console.WriteLine("PalletID     :");
            Console.WriteLine("Scan QR      :");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("Total "+Received+" of ");
            Console.ForegroundColor= Color.Red;
            Console.WriteLine(Expected+ " Carton");
            Console.ForegroundColor = Color.Black;
            Console.WriteLine("------------------------");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            resultSql();  
        }
        public void FormDetail()
        {

        }
        public static void ClearLine()
        {
        int currentLeft = Console.CursorLeft;
        int currentTop = Console.CursorTop;
        Console.Write(new String(' ', Console.WindowWidth - currentLeft));
        Console.SetCursorPosition(currentLeft, currentTop);
        }

        private static void ScanResult()
        {
            

        }
        public void ASNform()
        {
            Ulang:
            Console.SetCursorPosition(14, 2);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape)
                {
                    backtomenu();
                 }
                
                else
                    if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        ASN= "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "1";
                        ASN= strKey;
                        Handler();
                        
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }

        public void PalletForm()
        {
            Ulang:
            Console.SetCursorPosition(14, 5);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape)
                {
                    ASN = "";
                    key = "0";
                    Handler();
                    break;
                    
                }
                else
                    if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        PalletID = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "2";
                        PalletID = strKey;
                        Handler();

                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }
        public void CartonForm()
        {
            Ulang:
            Console.SetCursorPosition(14, 6);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape)
                {
                    CartonID = "";
                    key = "1";
                    Handler();
                    break;
                }
                else
                    if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        CartonID = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "3";
                        CartonID = strKey;
                        Handler();

                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }

        public void Handler()
        {

            if (key=="0")
            {
                ClearLine();
                FormHeader();
                Console.SetCursorPosition(14, 2);
                Console.Write(ASN);
                ASNform();
            }
            else if (key =="1")
            {

                FormHeader();
                Console.SetCursorPosition(14, 2);
                Console.Write(ASN);
                Console.SetCursorPosition(14, 3);
                Console.Write(Carline);
                PalletForm();
            }
            else if (key =="2")
            {
                FormHeader();
                Console.SetCursorPosition(14, 2);
                Console.Write(ASN);
                Console.SetCursorPosition(14, 3);
                Console.Write(Carline);
                Console.SetCursorPosition(14, 4);
                Console.Write(Carline);
                Console.SetCursorPosition(14, 5);
                Console.Write(PalletID);
                CartonForm();

            }
            else if(key =="3")
            {
                ParsingQR();
            }
            else
            {
                ASNform();
            
            }
        }
        private void ParsingQR()
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

            cn.Close();
            string[] HasilScan = CartonID.Split(new string[] {Delimiter }, StringSplitOptions.None);
            string txt_sku = HasilScan[4].ToString();
            string txt_CartonID = HasilScan[5].ToString();
            CartonID = txt_CartonID.ToString();
            SKU = txt_sku.ToString();
            Console.SetCursorPosition(0, 7);
            Console.WriteLine("Transmiting data......");
            UpdateSql();

        }

        void UpdateSql()
        {
            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("INSERT INTO kaizenDB.dbo.tbPLBSAMI_FG_UnloadDetail(" +
                "ASN,Carline,SKU,CartonID,Qty,PalletID,EditDate,UserName,NIK)" +
                " VALUES (@ASN,@Carline,@SKU,@CartonID,@Qty,@PalletID,@EditDate,@UserName,@NIK)", cn);
            cmd.Parameters.Add(new SqlParameter("ASN", ASN));
            cmd.Parameters.Add(new SqlParameter("Carline",Carline ));
            cmd.Parameters.Add(new SqlParameter("SKU", SKU));
            cmd.Parameters.Add(new SqlParameter("CartonID", CartonID));
            cmd.Parameters.Add(new SqlParameter("Qty", Qty));
            cmd.Parameters.Add(new SqlParameter("PalletID", PalletID));
            cmd.Parameters.Add(new SqlParameter("EditDate", Editdate));
            cmd.Parameters.Add(new SqlParameter("UserName", UserName));
            cmd.Parameters.Add(new SqlParameter("NIK", NIK));
            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = Color.Green;
                cn.Close();
                Console.SetCursorPosition(0, 7);
                Console.WriteLine("Succes");
                key = "2";
                Handler();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
                cn.Close();
            }

        }

        private static void resultSql()
        {
         SqlConnection cn = new SqlConnection(DBlocal);
            SqlCommand cmd = new SqlCommand("select ASN,CartonID,PalletID from tbPLBSAMI_FG_UnloadDetail" +
                " WHERE ASN = '0000000006' order by TransID Desc", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0} \t|{1} ",
                    reader[1], reader[2]));
                }

                reader.Close();
        }
        private void backtomenu()
        {
            LoginForm L = new LoginForm();
            L.FormLogin();
        }
        public static void SumDetails()
        {
                SqlConnection cn = new SqlConnection(DBlocal);
                cn.Close();
                SqlCommand cmd = new SqlCommand("Select Expected,Received from v_tbPLBSAMI_fg_UnloadSum where RECEIPTKEY='0000000006'", cn);
                cmd.Parameters.AddWithValue("@RECEIPTKEY", ASN);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Expected = reader.GetInt32(0);
                    Received = reader.GetInt32(1);

                }
            cn.Close();
        }
    }
}