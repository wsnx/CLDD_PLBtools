using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class CekLabel
    {
        private static string strKey = "";
        private static string palletID01 = "";
        private static string palletID02 = "";
        private static string MappingList = "";
        private static string key = "";
        private static string Type = "";
        private static string JumlahBox = "";
        private static string lottable09 = "";
        private static string QrLabel01;
        private static string QRLabel02;
        private static string txt_label01;
        private static string txt_label02;
        private static string txt_SKU;
        private static string txt_Qty;


        public void Head()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black; ;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("          Label Check             ");
            Console.WriteLine("__________________________________");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;

        }
        public void Foot()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 16);
            Console.Write("Back [esc] ");
            Console.Write("| Next [Enter]");
            Console.SetCursorPosition(0, 17);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("__________________________________");
            Console.WriteLine("   PLB Agility Tools v 2.0.1   ");
        }
        private void backtomenu()
        {
            MainMenu L = new MainMenu();
            L.Menu_Home();
        }
        public void Start()
        {
            Console.Clear();
            key = "0";
            MainMenu.FormName = "Label Check";
            Handler();

        }
        private void FormHeader()
        {
            MainMenu.FormName = "Label Check";
            Console.BackgroundColor = ConsoleColor.Black;
            Head();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(" MappingList   :");
            Console.WriteLine(" Pallet No.    :");
            Console.WriteLine(" Pallet No.    :");
            Console.WriteLine(" Carton        :");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Type          :");
            Console.WriteLine(" Qty           :");

            Console.WriteLine("__________________________________");
            Console.WriteLine("Status:");
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("                                  ");
            Console.WriteLine("                                  ");
            Console.WriteLine("                                  ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Foot();
        }
        private void Handler()
        {
            if (key == "0")
            {
                FormHeader();
                ScanMappingList();
            }
            if (key == "1")
            {
                FormHeader();
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(16, 2);
                Console.WriteLine(MappingList);
                Console.SetCursorPosition(16, 6);
                Console.WriteLine(Type);
                Console.SetCursorPosition(16, 7);
                Console.WriteLine(JumlahBox);
                ScanPallet01();
            }
            else if (key == "2")
            {
                FormHeader();
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(16, 2);
                Console.WriteLine(MappingList);
                Console.SetCursorPosition(16, 3);
                Console.WriteLine(txt_label01);
                Console.SetCursorPosition(16, 6);
                Console.WriteLine(Type);
                Console.SetCursorPosition(16, 7);
                Console.WriteLine(JumlahBox);

                Console.SetCursorPosition(0, 10);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("             Sesuai               ");
                Console.WriteLine("                                  ");
                Console.WriteLine("                                  ");
                ScanPallet02();
            }
            else if (key == "3")
            {
                FormHeader();
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(16, 2);
                Console.WriteLine(MappingList);
                Console.SetCursorPosition(16, 3);
                Console.WriteLine(txt_label01);
                Console.SetCursorPosition(16, 4);
                Console.WriteLine(txt_label02);
                Console.SetCursorPosition(16, 6);
                Console.WriteLine(Type);
                Console.SetCursorPosition(16, 7);
                Console.WriteLine(JumlahBox);

                Console.SetCursorPosition(0, 10);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("             Sesuai               ");
                Console.WriteLine("             Sesuai               ");
                Console.WriteLine("                                  ");
                ScanIsiCarton();
            }
            else if (key == "4")
            {
                FormHeader();
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(16, 2);
                Console.WriteLine(MappingList);
                Console.SetCursorPosition(16, 3);
                Console.WriteLine(txt_label01);
                Console.SetCursorPosition(16, 4);
                Console.WriteLine(txt_label02);
                Console.SetCursorPosition(16, 5);
                Console.WriteLine(Parser.SKU + " - " + Parser.CartonNo);
                Console.SetCursorPosition(16, 6);
                Console.WriteLine(Type);
                Console.SetCursorPosition(16, 7);
                Console.WriteLine(JumlahBox);
                Console.SetCursorPosition(0, 10);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Mappinglist:        Sesuai          ");
                Console.WriteLine("Label      :        Sesuai          ");
                Console.WriteLine("Carton     :        Sesuai          ");
                CloseProses();
            }

        }

        private void ScanIsiCarton()
        {
        Ulang:
            Console.SetCursorPosition(16, 5);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(16, 5);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    Parser.QRinput = "";
                    key = "2";
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

        public void ResultParsing()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select b.lottable09,b.LOTTABLE10 as CartonID,a.ID as PaletID from lotxlocxid a inner join LOTATTRIBUTE b on a.lot = b.LOT " +
            " where a.ID=@PalletID and lottable10=@cartonID and a.sku= @sku and a.qty>0  " +
            " group by  a.SKU,b.lottable10,a.ID,b.lottable09 ", cn);
            cmd.Parameters.AddWithValue("@PalletID", MappingList);
            cmd.Parameters.AddWithValue("@CartonID", Parser.CartonNo);
            cmd.Parameters.AddWithValue("@SKU", Parser.SKU);
            cn.Open();
            try
            {
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    cn.Close();
                    cn.Open();
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lottable09 = reader.GetString(0);
                    }
                    cn.Close();
                    Console.SetCursorPosition(8, 6);
                    InsertSql();
                    cn.Close();
                }
                else
                {

                    Console.SetCursorPosition(0, 10);
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Mappinglist:        Sesuai          ");
                    Console.WriteLine("Label      :        Sesuai          ");
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Carton     :   Tidak Sesuai         ");
                    Console.ReadKey();
                    key = "3";
                    Handler();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();

            }

        }

        private void InsertSql()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("insert into tbplbsami_fg_labelCheck (palletid,sku,cartonid,editdate,editwho,QRcontent,lottable09,QRLabel,QRLabel02) values  " +
                " ( @palletid,@sku,@cartonid,getdate(),@editwho,@QRcontent,@lottable09,@QRLabel,@QRLabel02)", cn);
            cmd.Parameters.Add(new SqlParameter("PalletID", MappingList));
            cmd.Parameters.Add(new SqlParameter("SKU", Parser.SKU));
            cmd.Parameters.Add(new SqlParameter("CartonID", Parser.CartonNo));
            cmd.Parameters.Add(new SqlParameter("EditWho", LoginForm.NIK + '-' + LoginForm.UserName));
            cmd.Parameters.Add(new SqlParameter("QRcontent", Parser.QRinput));
            cmd.Parameters.Add(new SqlParameter("lottable09", lottable09));

            cmd.Parameters.Add(new SqlParameter("QRLabel", QrLabel01));
            cmd.Parameters.Add(new SqlParameter("QRLabel02", QRLabel02));
            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                cn.Close();
                key = "4";
                Handler();
            }
            catch (Exception ex)
            {
                cn.Close();
                Error();


            }

        }

        private void ScanMappingList()
        {
        Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(16, 2);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
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
                        MappingList = "";
                        goto Ulang;
                    }
                    else
                    {

                        key = "1";
                        MappingList = strKey;
                        GetDataSo();
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }
        private void ScanPallet01()
        {
        Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 3);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("                 ");
            Console.SetCursorPosition(16, 3);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    key = "0";
                    Handler();
                }
                else
                if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        palletID01 = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "2";
                        palletID01 = strKey;
                        CekPallet01();
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }
        private void CekPallet01()
        {
            QrLabel01 = palletID01;

            if (palletID01.Contains("MAZDA - 19-") || palletID01.Contains("MAZDA - 20-"))
            {
                QrLabel01 = palletID01;
                string[] x = palletID01.Split(new string[] { "," }, StringSplitOptions.None);
                string b;
                string a = x[0].ToString();
                string c = a.Substring(15, 5);
                palletID01 = "1MZD9-" + c;
                txt_label01 = a;
                QRLabel02 = QrLabel01;

                key = "3";
                Handler();

            }
            if (palletID01.Contains("- 19,") || palletID01.Contains("- 20,"))
            {
                QrLabel01 = palletID01;
                string[] x = palletID01.Split(new string[] { "," }, StringSplitOptions.None);
                string b;
                string a = x[0].ToString();
                txt_label01 = a;
                int xx = a.ToString().Length - 1;
                if (palletID01.Contains("DYNA"))
                {
                    int xxX = a.ToString().Length - 1;
                    palletID01 = a.Substring(4, 15);
                    palletID01 = "TS. " + palletID01;
                    key = "2";
                    Handler();

                }
                else
                {
                    palletID01 = a.Substring(0, xx - 4);
                    key = "2";
                    Handler();

                }

            }

            if (palletID01.Contains(","))
            {

                QrLabel01 = palletID01;

                string[] x = palletID01.Split(new string[] { "," }, StringSplitOptions.None);
                string aa = palletID01.Replace('.', ',');
                string[] y = aa.Split(new string[] { "," }, StringSplitOptions.None);
                string a = x[0].ToString();
                txt_label01 = a;
                string b = y[2].ToString();
                string c = y[3].ToString();
                int xx = a.ToString().Length - 1;
                string pallet = a.Substring(1, xx);
                SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
                cn.Close();
                SqlCommand cmd = new SqlCommand("select id,SKU,cast(sum(qty) as varchar)as QtyOrder from lotxlocxid " +
                    "where id like'%" + pallet.ToString() + "' and qty>0" +
                    " group by id,SKU ", cn);
                cn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        palletID01 = reader.GetString(0);
                        txt_SKU = reader.GetString(1);
                        txt_Qty = reader.GetString(2);


                    }
                }
                else
                {
                    string vCek = "SKU Not Found";
                    Console.SetCursorPosition(0, 12);
                    Console.WriteLine(vCek);
                    // ScanSO();

                }
                if (MappingList == palletID01)
                {


                }
                else
                {

                    Console.SetCursorPosition(0, 10);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("                                  ");
                    Console.WriteLine("        Pallet Not Found          ");
                    Console.WriteLine("                                  ");
                    Console.ReadKey();
                    key = "0";
                    Handler();

                }

                if (txt_SKU == b)
                {

                }
                else
                {

                    Console.WriteLine("SKU SALAH");

                }
                if (txt_Qty.ToString() == c + ".00000")
                {

                }
                else
                {
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Qty SALAH");
                    Error();
                }
                key = "2";
                Handler();

            }
            if (palletID01.Contains("1HND") || palletID01.Contains("4SYT"))
            {
                txt_label01 = QrLabel01;
                QRLabel02 = QrLabel01;
                key = "3";
                Handler();
            }
            else
            {
                //Error();
            }
        }
        private void ScanPallet02()
        {
        Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 4);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("                 ");
            Console.SetCursorPosition(16, 4);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    key = "1";
                    Handler();
                }
                else
                if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        palletID02 = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "2";
                        palletID02 = strKey;
                        CekPallet02();
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }


        private void CekPallet02()
        {


            if ((QrLabel01.Contains("DS.") && palletID02.Contains("DS.")) || (QrLabel01.Contains("BS.") && palletID02.Contains("BS.")))
            {
                Console.SetCursorPosition(0, 10);
                Console.Write("Pallet Tidak sesuai");
                key = "2";
                Error();
            }
            if (palletID02.Contains("- 19,") || palletID02.Contains("- 20,"))
            {
                QRLabel02 = palletID02;
                string[] x = palletID02.Split(new string[] { "," }, StringSplitOptions.None);
                string b;
                string a = x[0].ToString();
                txt_label02 = a;
                int xx = a.ToString().Length - 1;
                if (palletID02.Contains("DYNA"))
                {
                    int xxX = a.ToString().Length - 1;
                    palletID02 = a.Substring(4, 15);
                    palletID02 = "TS. " + palletID02;
                    key = "3";
                    Handler();

                }
                else
                {
                    palletID02 = a.Substring(0, xx - 4);
                    key = "2";
                    Handler();

                }

            }

            if (palletID02.Contains(","))
            {

                QRLabel02 = palletID02;

                string[] x = palletID02.Split(new string[] { "," }, StringSplitOptions.None);
                string aa = palletID02.Replace('.', ',');
                string[] y = aa.Split(new string[] { "," }, StringSplitOptions.None);
                string a = x[0].ToString();
                txt_label02 = a;
                string b = y[2].ToString();
                string c = y[3].ToString();
                int xx = a.ToString().Length - 1;
                string pallet = a.Substring(1, xx);
                SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
                cn.Close();
                SqlCommand cmd = new SqlCommand("select id,SKU,cast(sum(qty) as varchar)as QtyOrder from lotxlocxid " +
                    "where id like'%" + pallet.ToString() + "' and qty>0" +
                    " group by id,SKU ", cn);
                cn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        palletID02 = reader.GetString(0);
                        txt_SKU = reader.GetString(1);
                        txt_Qty = reader.GetString(2);


                    }
                }
                else
                {
                    string vCek = "SKU Not Found";
                    Console.SetCursorPosition(0, 12);
                    Console.WriteLine(vCek);
                    // ScanSO();

                }
                if (MappingList == palletID02)
                {


                }
                else
                {

                    Console.SetCursorPosition(0, 10);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("                                  ");
                    Console.WriteLine("        Pallet Not Found          ");
                    Console.WriteLine("                                  ");
                    Console.ReadKey();
                    key = "0";
                    Handler();

                }

                if (txt_SKU == b)
                {

                }
                else
                {

                    Console.WriteLine("SKU SALAH");

                }
                if (txt_Qty.ToString() == c + ".00000")
                {

                }
                else
                {
                    Console.WriteLine("Qty SALAH");
                }
                key = "3";
                Handler();

            }

        }
        private void Error()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Press Esc......");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        ulang:
            string strkey = "";
            ConsoleKeyInfo cki;
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Escape)
            {
                Handler();
            }

            else
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write(" ");
                Console.SetCursorPosition(0, currentLineCursor);
                goto ulang;
            }
        }
        private void CloseProses()
        {
            Console.SetCursorPosition(0, 15);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Press Esc......");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        ulang:
            string strkey = "";
            ConsoleKeyInfo cki;
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Escape)
            {
                key = "0";
                Handler();
            }

            else
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write(" ");
                Console.SetCursorPosition(0, currentLineCursor);
                goto ulang;
            }
        }

        private void GetDataSo()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select cast(count(sku)as varchar) as JumlahBox,case when (id like '%1HND%' or id like '%1MZD%' or id like '%4SYT%' )then 'MASTER' when count(distinct(SKU))>1 then 'Mixed' else 'Non Mixed' end Type from lotxlocxid " +
                "where id ='" + MappingList.ToString() + "' and qty>0" +
                " group by id ", cn);
            cn.Open();


            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Type = reader.GetString(1);
                        JumlahBox = reader.GetString(0);
                    }
                    key = "1";
                    Handler();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.SetCursorPosition(0, 10);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("                                  ");
                Console.WriteLine("        Pallet Not Found          ");
                Console.WriteLine("                                  ");
                Console.ReadKey();
                key = "0";
                Handler();

            }
        }
    }
}
