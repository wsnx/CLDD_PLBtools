using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class Dyna
    {

        private static string strKey;
        private static string PalletID;
        private static string DocNumber;
        private static string key;
        private static string TotalSKU;
        private static string TotalCarton;
        public void backtomenu()
        {
            MainMenu L = new MainMenu();
            L.Menu_Home();
        }
        public void Start()
        {
            MainMenu.FormName = "Dyna";
            Console.BackgroundColor = ConsoleColor.Black;
            Head();
            key = "0";
            Handler();

        }
        public void Head()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("         Docking Dyna        ");
            Console.WriteLine("_____________________________");
            Console.ForegroundColor = ConsoleColor.Green;

        }
        public void Docform()
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
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.UpArrow)
                {
                    backtomenu();
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                 if (cki.Key == ConsoleKey.Enter || cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.Tab)
                {
                    if (strKey == "")
                    {
                        DocNumber = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "1";
                        DocNumber = strKey;
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
            Console.BackgroundColor = ConsoleColor.Black;
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
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.UpArrow)
                {
                    key = "0";
                    Handler();
                    break;
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                 if (cki.Key == ConsoleKey.Enter ||
                    cki.Key == ConsoleKey.DownArrow ||
                    cki.Key == ConsoleKey.RightArrow ||
                    cki.Key == ConsoleKey.Tab)
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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(14, 4);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(14, 4);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.UpArrow)
                {
                    key = "1";
                    Handler();
                    break;
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                 if (cki.Key == ConsoleKey.Enter ||
                    cki.Key == ConsoleKey.DownArrow ||
                    cki.Key == ConsoleKey.RightArrow ||
                    cki.Key == ConsoleKey.Tab)
                {
                    if (strKey == "")
                    {
                        Parser.QRinput = "";
                        goto Ulang;
                    }
                    else
                    {
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
        public void FormHeader()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Head();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Doc. No.");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Pallet No.");
            Console.SetCursorPosition(0, 4);
            Console.WriteLine("Carton No.");
            Console.SetCursorPosition(0, 6);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Total Carton :");
            Console.SetCursorPosition(0, 7);
            Console.WriteLine("Total SKU    :");
            Console.ForegroundColor = ConsoleColor.Green;

        }
        public void Handler()
        {
            if (key == "0")
            {
                FormHeader();
                Docform();

            }
            else if (key == "1")
            {

                FormHeader();
                Console.SetCursorPosition(14, 2);
                Console.WriteLine(DocNumber);
                PalletForm();
            }
            else if (key == "2")
            {

                FormHeader();
                ResultSQL();
                JumlahSKU();
                Console.SetCursorPosition(14, 2);
                Console.WriteLine(DocNumber);
                Console.SetCursorPosition(14, 3);
                Console.WriteLine(PalletID);
                Console.SetCursorPosition(14, 6);
                Console.WriteLine(TotalCarton);
                Console.SetCursorPosition(14, 7);
                Console.WriteLine(TotalSKU);

                CartonForm();
            }
        }

        private void ResultSQL()
        {
            Console.SetCursorPosition(0, 9);
            Console.ForegroundColor = ConsoleColor.White;
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            SqlCommand cmd = new SqlCommand("SELECT	 CartonID,SKU  FROM tbplbsami_fg_DockingDyna WHERE Invoice=@DocNumber AND PalletID=@PalletID order by SKU, cast(cartonID as int)", cn);
            cmd.Parameters.AddWithValue("@DocNumber", DocNumber);
            cmd.Parameters.AddWithValue("@PalletID", PalletID);

            cn.Open();
            Console.WriteLine(String.Format("__________________________________"));
            Console.WriteLine(String.Format("{0,7} | {1,9}  ", "CartonID", "SKU"));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0,7}  | {1,9} ",
                reader[0], reader[1]));
            }
            reader.Close();
            Console.ForegroundColor = ConsoleColor.Green;
        }
        private void JumlahSKU()
        {

            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("SELECT	  CASE WHEN Count(distinct(SKU)) >4 THEN Concat('Jumlah SKU Tidak Sesuai (',cast(Count(distinct(SKU))AS varchar),')')ELSE cast(Count(distinct(SKU))AS varchar) END JumlahSKU ," +
                "cast(Count(distinct(Qrcontent))as varchar) AS JumlahCarton, palletID,Invoice " +
                " FROM tbplbsami_fg_DockingDyna WHERE Invoice=@DocNo AND PalletID=@PalletID GROUP BY palletID,Invoice", cn);
            cmd.Parameters.AddWithValue("@DocNo", DocNumber);
            cmd.Parameters.AddWithValue("@PalletID", PalletID);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TotalSKU = reader.GetString(0);
                TotalCarton = reader.GetString(1);
            }
            cn.Close();
        }
        public void InsertSQL()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();

            SqlCommand cmd = new SqlCommand("insert into tbplbsami_fg_DockingDyna (Invoice,PalletID,SKU,CartonID,QRContent,editdate,EditBy)" +
                " values (@Invoice,@PalletID,@SKU,@CartonID,@QRContent,getdate(),@EditBy)", cn);
            cmd.Parameters.Add(new SqlParameter("Invoice", DocNumber));
            cmd.Parameters.Add(new SqlParameter("PalletID", PalletID));
            cmd.Parameters.Add(new SqlParameter("SKU", Parser.SKU));
            cmd.Parameters.Add(new SqlParameter("CartonID", Parser.CartonNo));
            cmd.Parameters.Add(new SqlParameter("QRContent", Parser.QRinput));
            cmd.Parameters.Add(new SqlParameter("EditBy", LoginForm.UserName));
            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                cn.Close();
                key = "2";
                Handler();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex);
                Console.ReadKey();
                cn.Close();

                key = "2";
                Handler();
            }

        }

    }
}
