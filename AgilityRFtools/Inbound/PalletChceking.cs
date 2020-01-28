using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class PalletChceking
    {
        private static string FromPalletID = "";
        private static string Notes = "";
        private static string Remarks = "";
        private static string PalletID = "";
        private static string strKey = "";
        private static string key;
        private static string txt_CartonID;
        private static string result = "";
        private static DateTime Editdate = DateTime.Now;
        private static int Expected = 0;
        private static int Scan = 0;

        public void Head()
        {
            MainMenu.FormName = "MappingCek";
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
            Console.SetCursorPosition(0, 8);
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            SqlCommand cmd = new SqlCommand("select  mappingID,a.CartonID,b.sku,c.Notes from tbPLBSAMI_FG_tempGenerateLIST a inner join tbPLBSAMI_FG_stgMappingStock b on a.sku= b.sku and a.CartonID = b.CartonID and concat(a.AssyCode,'~',DestinationCode)= b.Lottable09 left join  " +
            "   (select PalletID, CartonID, SKU, notes from tbplbsami_fg_mappingChecking group by PalletID, CartonID, SKU, Notes) c on c.CartonID = a.CartonID and c.PalletID = a.MappingID and a.sku = c.SKU " +
            "   where MappingID = @PalletID and  isnull(c.Notes, '') = ''  " +
            " group by mappingID,a.CartonID,b.sku,c.Notes ", cn);
            cmd.Parameters.AddWithValue("@PalletID", PalletID);
            cn.Open();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(String.Format("List Carton Belum Scan (top 10):"));
            Console.WriteLine(String.Format("_______________________________"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(String.Format("CartonID"));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txt_CartonID = reader.GetString(1);
                Console.WriteLine(String.Format("{0}",
                reader[1]));
            }
            reader.Close();
        }
        public void SumExpected()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select cast(count(CartonID)as int) as JumlahCarton from tbPLBSAMI_FG_tempGenerateLIST where MappingID=@PalletID group by MappingID", cn);
            cmd.Parameters.AddWithValue("@PalletID", PalletID);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Expected = reader.GetInt32(0);
            }
            cn.Close();
        }
        public void sumScan()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select cast(count(distinct(cartonID))as int)as JumlahScan from  tbplbsami_fg_mappingChecking where PalletID=@palletID group by PalletID", cn);
            cmd.Parameters.AddWithValue("@PalletID", PalletID);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Scan = reader.GetInt32(0);
            }
            cn.Close();

        }
        public void ResultParsing()
        {


            if (Parser.QRinput.Contains("EMPTY"))
            {

                result = "Sesuai";
                InsertSql();


            }
            else
            {
                SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
                cn.Close();
                SqlCommand cmd = new SqlCommand("select a.SKU,a.mappingID,a.CartonID from tbPLBSAMI_FG_tempGenerateLIST  a" +
                    " where a.MappingID = @PalletID and CartonID= @cartonID and sku = @SKU" +
                    "", cn);
                cmd.Parameters.AddWithValue("@PalletID", PalletID);
                cmd.Parameters.AddWithValue("@CartonID", Parser.CartonNo);
                cmd.Parameters.AddWithValue("@SKU", Parser.SKU);
                cn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    result = "Sesuai";
                    InsertSql();
                    cn.Close();
                }
                else
                {
                    Console.WriteLine("Data tidak di temukan");
                }
            }
        }
        private void InsertSql()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("insert into tbplbsami_fg_mappingChecking " +
                "(PalletID,CartonID,SKU,Remarks,Notes,EditDate,EditWho,QRcontent) " +
                "values (@PalletID,@CartonID,@SKU,@Remarks,@Notes,getdate(),@EditWho,@QRcontent)", cn);

            cmd.Parameters.Add(new SqlParameter("PalletID", PalletID));
            cmd.Parameters.Add(new SqlParameter("CartonID", Parser.CartonNo));
            cmd.Parameters.Add(new SqlParameter("SKU", Parser.SKU));
            cmd.Parameters.Add(new SqlParameter("Remarks", Remarks));
            cmd.Parameters.Add(new SqlParameter("Notes", "Sesuai"));
            //cmd.Parameters.Add(new SqlParameter("Editdate", Editdate));
            cmd.Parameters.Add(new SqlParameter("EditWho", LoginForm.NIK + '-' + LoginForm.UserName));
            cmd.Parameters.Add(new SqlParameter("QRcontent", Parser.QRinput));
            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                cn.Close();
                Console.SetCursorPosition(0, 7);
                Console.WriteLine("Succes");
                key = "2";
                Handler();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex);
                cn.Close();
            }

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
            Console.Write(Scan);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" of ");
            Console.WriteLine(Expected + " Carton");
            Console.ForegroundColor = ConsoleColor.White;
            resultSql();
        }
        private void Handler()
        {
            if (key == "0")
            {

                FormHeader();
                Palletform();
            }
            else if (key == "1")
            {
                Scan = 0;
                Expected = 0;
                sumScan();
                SumExpected();
                FormHeader();
                Console.SetCursorPosition(14, 2);
                Console.Write(PalletID);
                Cartonform();
            }
            else if (key == "2")
            {
                sumScan();
                FormHeader();
                Console.SetCursorPosition(14, 2);
                Console.Write(PalletID);
                Console.SetCursorPosition(14, 4);
                Console.Write(Parser.SKU);
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(">> " + Parser.CartonNo + " " + result);
                Cartonform();
                resultSql();
            }
        }
        private void backtomenu()
        {
            MainMenu L = new MainMenu();
            L.Menu_Home();
        }

    }
}
