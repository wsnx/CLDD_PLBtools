using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{

    class UnloadForm
    {
        private static string ASN="";
        private static string CartonID="";
        private static string Carline="";
        private static string Conn = "Integrated Security=False;Data Source=10.130.24.4;Initial Catalog=KaizenDB;User ID=kadmin;Password=53c4dm1n;";
        private static string NIK = "18.332";
        private static string UserName="" ;
        private static string PalletID="";
        private static string SKU="";
        private static int Qty=10;
        private static DateTime Editdate = DateTime.Now;
        private static string key;
        private static string strKey = "";

        public void Frame()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("     Form Unloading     ");
            Console.BackgroundColor = ConsoleColor.White;

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
            Console.Clear();
            Frame();
            Console.SetCursorPosition(0,2);
            Console.WriteLine("ASN No.      :");
            Console.WriteLine("No Mobil     :");
            Console.WriteLine("Carline Type :");
            Console.WriteLine("PalletID     :");
            Console.WriteLine("Scan QR      :");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("0"+" of ");
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("36");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("------------------------");
            
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
//0
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
                    ASN = "";
                    break;
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
//1
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

            }
            else
            {
                ASNform();
            
            }
        }
        private void ParsingQR()
        {

            UpdateSql();
        }

        void UpdateSql()
        {
            SqlConnection cn = new SqlConnection(Conn);
            cn.Close();
            SqlCommand cmd = new SqlCommand("INSERT INTO kaizenDB.dbo.tbPLBSAMI_FG_UnloadingDetail(" +
                "ASN,CarlineType,SKU,CartonID,Qty,PalletID,EditDate,UserName,NIK)" +
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
                Console.ForegroundColor = ConsoleColor.Green;
                cn.Close();
                key = "1";
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
            
            SqlConnection cn = new SqlConnection(Conn);
            SqlCommand cmd = new SqlCommand("select ASN,CartonID,PalletID from tbPLBSAMI_FG_UnloadingDetail" +
                "  order by transid desc", cn);
            //cmd.Parameters.AddWithValue("@ASN",ASN );
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0} \t|{1} ",
                    reader[1], reader[2]));
                }

                reader.Close();
            Console.Read();
            
        }

    }
}