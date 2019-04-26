using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilityRFtools
{
    class PalletChceking
    {
        private static string PalletID;
        private static string strKey = "";
        private static string key;
        private static string txt_CartonID;
        private static string result = "";

        public void Head()
        {

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("         Mapping Check       ");
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
            MainMenu.FormName = "Mapping Cek";
            Console.BackgroundColor = ConsoleColor.Black;
            Head();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("PalletNo     :");
            Palletform(); 
        }
        private void resultSql()
        {
            Console.SetCursorPosition(0,8);
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            SqlCommand cmd = new SqlCommand("select top 5 SKU,FromPalletID, CartonID from tbplbsami_fg_Unloaddetails " +
                "where FromPalletID = @PalletID", cn);
            cmd.Parameters.AddWithValue("@PalletID",PalletID);
            cn.Open();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(String.Format("List Pallet:"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(String.Format("CartonID"));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txt_CartonID = reader.GetString(1);
                Console.WriteLine(String.Format("{0}",
                reader[2]));
            }
            reader.Close();
        }
        public void ResultParsing()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select top 10 SKU,FromPalletID, CartonID from tbplbsami_fg_Unloaddetails " +
                "where FromPalletID = @PalletID", cn);
            cmd.Parameters.AddWithValue("@PalletID", PalletID);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txt_CartonID = reader.GetString(2);
                if (Parser.CartonNo ==txt_CartonID)
                {
                    result = "Sesuai";
                }
                else
                {
                    result = "Tidak Sesuai";
                }
            }
            reader.Close();
            key = "2";
            Handler();
        }

        public void Palletform()
        {
            Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(14, 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
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
                        key = "1";
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
        public void Cartonform()
        {
            Ulang:
            Console.SetCursorPosition(14, 3);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(14, 3);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    Parser.QRinput = "";
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
                        Parser.QRinput = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "2";
                        Parser.QRinput = strKey;
                        Parser l = new Parser();
                        l.Load();
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }

        }
        private void FormHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Head();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Pallet       :");
            Console.WriteLine("Scan Carton  :");
            Console.WriteLine("Assy Number  :");
            Console.SetCursorPosition(0, 7);
            Console.Write("Total ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("12");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" of ");
            Console.WriteLine("20"+ " Carton");
            Console.ForegroundColor = ConsoleColor.White;
            resultSql();
        }
        private void Handler()
        {
            if (key=="0")
            {
                FormHeader();
                Palletform();
            }
            else if (key=="1")
            {
                FormHeader();
                Console.SetCursorPosition(14, 2);
                Console.Write(PalletID);
                Cartonform();
            }
            else if (key == "2")
            {
                FormHeader();
                Console.SetCursorPosition(14, 2);
                Console.Write(PalletID);
                Console.SetCursorPosition(14, 4);
                Console.Write(Parser.SKU);
                Console.SetCursorPosition(0,6);
                Console.WriteLine(">> "+Parser.CartonNo + " "+result );
                Cartonform();
            }
        }
        private void backtomenu()
        {
            MainMenu L = new MainMenu();
            L.Menu_Home();
        }
     }
}
