using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class UnloadForm : ConfigDB
    {
        public static string QRSAMI;
        private static string ASN="";
        private static string NoMobil = "";
        private static string CartonID="";
        private static string Carline="";
        private static string NIK = "";
        private static string Loc = "";
        private static string PalletID="";
        private static string SKU="";
        private static int Qty;
        private static DateTime Editdate = DateTime.Now;
        private static string key;
        private static string strKey = "";
        private static Int32 Expected;
        private static Int32 Received;
        public static string Delimiter;
        public static Int32 AssySeq;
        public static Int32 CartonIDSeq;
        private static string cek = "";

        public void Head()
        {
            MainMenu.FormName = "UnloadForm";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            Console.SetCursorPosition(0,0);
            Console.WriteLine("         Unload Form         ");
            Console.WriteLine("_____________________________");
            Console.ForegroundColor = ConsoleColor.Green;
       }
        public void Foot()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 16);
            Console.Write("Back [esc] ");
            Console.Write("| Next [Enter]");
            Console.SetCursorPosition(0, 17);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("_______________________________");
            Console.WriteLine("   PLB Agility Tools v 2.0.1   ");
        }
        public void Start()  
        {
            Head();
            Foot();
            Console.SetCursorPosition(0,2);
            Console.WriteLine("Entry ASN :");
            ASNform();
        }
        public void FormHeader ()
        {

            Head();
            Console.SetCursorPosition(0,2);
            Console.WriteLine("ASN No   :");
            Console.WriteLine("No Mobil :");
            Console.WriteLine("Stage Loc:");
            Console.WriteLine("PalletID :");
            Console.WriteLine("Scan QR  :");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("Total ");
            Console.ForegroundColor= ConsoleColor.Red;
            Console.Write(Received );
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write( " of ");
            Console.WriteLine(Expected+ " Carton");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public static void ClearLine()
        {
        int currentLeft = Console.CursorLeft;
        int currentTop = Console.CursorTop;
        Console.Write(new String(' ', Console.WindowWidth - currentLeft));
        Console.SetCursorPosition(currentLeft, currentTop);
        }
        public void ASNform()
        {
            Ulang:
            Console.SetCursorPosition(12, 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(12, 2);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape)
                {
                 backtomenu();
                 }

                else if (cki.Key == ConsoleKey.Backspace)
                {
                 goto Ulang;

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
                     key = "4";
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
        public void LocForm()
        {
            Ulang:
            Console.SetCursorPosition(12, 4);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(12, 4);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    ASN = "";
                    key = "0";
                    Handler();
                    break;
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                    if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        Loc = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "1";
                        Loc = strKey;
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
            Console.SetCursorPosition(12, 5);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(12, 5);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape|| cki.Key == ConsoleKey.UpArrow)
                {
                    ASN = "";
                    key = "0";
                    Handler();
                    break;
                }

                else if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
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
            Console.SetCursorPosition(12, 6);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(12, 6);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape)
                {
                 Parser.QRinput = "";
                 key = "1";
                 Handler();
                }
                if (cki.Key ==  ConsoleKey.UpArrow)
                {
                 Parser.QRinput = "";
                 key = "1";
                 Handler();
 
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                 goto Ulang;

                }
                else
                 if (cki.Key == ConsoleKey.Enter)
                {
                 if (strKey == "")
                 {
                     Parser.QRinput = "";
                     goto Ulang;
                 }
                 else
                 {
                     key = "3";
                     Parser.QRinput = strKey;
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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            if (key=="0")
            {
                ClearLine();
                FormHeader();
                Console.SetCursorPosition(12, 2);
                Console.Write(ASN);
                ASNform();
            }
            else if (key == "4")
            {
                FormHeader();
                Console.SetCursorPosition(12, 2);
                Console.Write(ASN);
                Console.SetCursorPosition(12, 3);
                Console.Write(NoMobil);
                Console.SetCursorPosition(12, 4);
                Console.Write(NoMobil);
                LocForm();
            }
            else if (key =="1")
            {
                FormHeader();
                Console.SetCursorPosition(12, 2);
                Console.Write(ASN);
                Console.SetCursorPosition(12, 3);
                Console.Write(NoMobil);
                Console.SetCursorPosition(12, 4);
                Console.Write(Loc);
                PalletForm();
            }
            else if (key =="2")
            {

                if (cek=="")
                {
                    FormHeader();
                    Console.SetCursorPosition(12, 2);
                    Console.Write(ASN);
                    Console.SetCursorPosition(12, 3);
                    Console.Write(NoMobil);
                    Console.SetCursorPosition(12, 4);
                    Console.Write(Loc);
                    Console.SetCursorPosition(12, 5);
                    Console.Write(PalletID);
                    CartonForm();
                }else if (cek=="1")
                {
                    FormHeader();
                    Console.SetCursorPosition(12, 2);
                    Console.Write(ASN);
                    Console.SetCursorPosition(12, 3);
                    Console.Write(NoMobil);
                    Console.SetCursorPosition(12, 4);
                    Console.Write(Loc);
                    Console.SetCursorPosition(12, 5);
                    Console.Write(PalletID);
                    resultSql();
                    CartonForm();

                }
            }
            else if(key =="3")
            {
                Parser f = new Parser();
                f.Load();
            }
            else
            {
                ASNform();
            }
        }
        public void UpdateSql()
        {
            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("INSERT INTO kaizenDB.dbo.tbPLBSAMI_FG_UnloadDetails(" +
                "Receiptkey,NoMobil,Carline,FromPalletID,SKU,Qty,CartonID,DestinationCode,EditDate,UserName)" +
                " VALUES (@Receiptkey,@NoMobil,@Carline,@FromPalletID,@SKU,@Qty,@CartonID,@DestinationCode,@EditDate,@UserName)", cn);
            cmd.Parameters.Add(new SqlParameter("Receiptkey", ASN));
            cmd.Parameters.Add(new SqlParameter("NoMobil",NoMobil ));
            cmd.Parameters.Add(new SqlParameter("Carline", Carline));
            cmd.Parameters.Add(new SqlParameter("Loc",Loc));
            cmd.Parameters.Add(new SqlParameter("FromPalletID", PalletID));
            cmd.Parameters.Add(new SqlParameter("SKU", Parser.SKU));
            cmd.Parameters.Add(new SqlParameter("Qty", Parser.Qty));
            cmd.Parameters.Add(new SqlParameter("CartonID", Parser.CartonNo));
            cmd.Parameters.Add(new SqlParameter("DestinationCode",Parser.DestinationCode));
            cmd.Parameters.Add(new SqlParameter("Editdate", Editdate));
            cmd.Parameters.Add(new SqlParameter("UserName", LoginForm.NIK+'-'+LoginForm.UserName));
            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                cn.Close();
                Console.SetCursorPosition(0, 7);
                Console.WriteLine("Succes");
                key = "2";
                SumDetails();
                cek = "1";
                Handler();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex);
                Console.ReadKey();
                cn.Close();
            }
         }

        private static void resultSql()
        {
            Console.SetCursorPosition(0,9);
            SqlConnection cn = new SqlConnection(DBlocal);
            SqlCommand cmd = new SqlCommand("select top 5 receiptkey,CartonID,sku from tbPLBSAMI_FG_UnloadDetails" +
                " WHERE Receiptkey = @RECEIPTKEY order by TransID Desc", cn);
            cmd.Parameters.AddWithValue("@RECEIPTKEY", ASN);
            cn.Open();
            Console.WriteLine(String.Format("CartonID | SKU "));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0} | {1}",
                reader[1], reader[2]));
            }
            reader.Close();

        }
        public static void SumDetails()
        {
               SqlConnection cn = new SqlConnection(DBlocal);
                cn.Close();
                SqlCommand cmd = new SqlCommand("Select expected,Receipt from v_tbPLBSAMI_fg_UnloadSum where RECEIPTKEY=@RECEIPTKEY", cn);
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
        private void backtomenu()
        {
            MainMenu L = new MainMenu();
            L.Menu_Home();
        }
    }
}