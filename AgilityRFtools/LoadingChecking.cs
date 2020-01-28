using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class LoadingChecking
    {
        private static string strKey = "";
        private static string TraillerNo = "";
        private static string Orderkey = "";
        private static string ExternalOrderkey = "";
        private static string PalletID = "";
        private static string StorerKey = "";
        private static string StatusPallet = "";
        private static string txt_QRcontent = "";
        private static string txt_SKU = "";
        private static string key = "";
        private static string AJUNO = "";
        private static int Scan = 0;
        private static int Expected = 0;
        private static DateTime Editdate = DateTime.Now;
        private static string Notes;

        public void Head()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black; ;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("        Loading  Checking        ");
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
            Console.WriteLine("_______________________________");
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
            Head();
            Foot();
            MainMenu.FormName = "Loading Checking";
            Console.BackgroundColor = ConsoleColor.Black;
            Head();

            Foot();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Pallet Number:");
            ScanPallet();
        }
        private void FormHeader()
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Head();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Pallet        :");
            Console.WriteLine("Owner         :");
            Console.WriteLine("Container No  :");
            Console.WriteLine("Orderkey      :");
            Console.WriteLine("ExternOrder   :");
            Console.WriteLine("AJU NO        :");
            Console.WriteLine("Status        :");
            Console.WriteLine("--------------");

            Console.Write("Scan ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Scan);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Of ");
            Console.Write(Expected);
            Console.Write(" PALLET ");
            Console.SetCursorPosition(0, 9);
            Console.Write("Container No >>");

        }
        private void Handler()
        {
            if (key == "0")
            {
                Scan = 0;
                Expected = 0;
                FormHeader();
                ScanPallet();
            }
            else if (key == "1")
            {
                PalletID = "";
                FormHeader();
                Console.SetCursorPosition(16, 2);
                Console.Write(TraillerNo);

            }
            else if (key == "2")
            {
                sumSCAN();
                SumExpected();
                FormHeader();

                Console.SetCursorPosition(16, 2);
                Console.Write(PalletID);
                Console.SetCursorPosition(16, 3);
                Console.Write(StorerKey);
                Console.SetCursorPosition(16, 4);
                Console.Write(TraillerNo);
                Console.SetCursorPosition(16, 5);
                Console.Write(Orderkey);
                Console.SetCursorPosition(16, 6);
                Console.Write(ExternalOrderkey);
                Console.SetCursorPosition(16, 7);
                Console.Write(AJUNO);
                ScanContainerForm();

                if (Orderkey.Contains("0"))
                {
                    ScanContainerForm();
                    Console.SetCursorPosition(0, 8);
                    Console.Write(StatusPallet);
                }
                if (StatusPallet == "belum bisa di muat")
                {


                    Console.SetCursorPosition(0, 8);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(StatusPallet);
                    Console.ForegroundColor = ConsoleColor.White;
                    Error();

                }
                else
                {
                    Console.SetCursorPosition(0, 8);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(StatusPallet);
                    Console.ForegroundColor = ConsoleColor.White;
                    Error();


                }
            }
        }

        private void ScanPallet()
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

                        if (strKey.Contains(","))
                        {
                            key = "1";
                            txt_QRcontent = strKey;

                            if (strKey.Contains("MAZDA - 19-") || strKey.Contains("MAZDA - 20-"))
                            {

                                string[] x = strKey.Split(new string[] { "," }, StringSplitOptions.None);
                                string a = x[0].ToString();

                                string c = a.Substring(a.Length - 5);
                                PalletID = "1MZD9-" + c;
                                GetDataOrder2();

                            }

                            if (strKey.Contains("DYNA") && strKey.Contains(","))
                            {

                                string[] x = strKey.Split(new string[] { "," }, StringSplitOptions.None);
                                string b;
                                string a = x[0].ToString();
                                int xx = a.ToString().Length - 1;
                                int xxX = a.ToString().Length - 1;
                                PalletID = a.Substring(4, 15);
                                PalletID = "TS. " + PalletID;


                            }
                            else
                            {
                                string[] b = strKey.Split(new string[] { "," }, StringSplitOptions.None);
                                string a = b[0].ToString();
                                string c = b[1].ToString();
                                int xx = a.ToString().Length - 1;
                                PalletID = a.Substring(1, xx);
                                txt_SKU = c.ToString();

                            }

                            GetDataOrder();
                            Handler();

                        }
                        else if (strKey.Contains("1HND"))

                        {
                            PalletID = strKey;
                            GetDataOrder2();
                            Handler();


                        }
                        else if (strKey.Contains("2DNA"))

                        {
                            PalletID = strKey;
                            GetDataOrder2();
                            Handler();
                        }
                        else if (strKey.Contains("1DNA"))

                        {
                            PalletID = strKey;
                            GetDataOrder2();
                            Handler();
                        }
                        else if (strKey.Contains("4SYT"))
                        {

                            PalletID = strKey;
                            GetDataOrder2();
                            Handler();
                        }
                        else
                        {
                            PalletID = strKey;
                            GetDataOrder2();
                            Handler();

                        }

                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }

        private void ScanContainerForm()
        {
        Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 9);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(16, 9);
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
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                 if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        TraillerNo = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "1";
                        TraillerNo = strKey;
                        CekContainer();
                        Handler();
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }
        private void Error()
        {
        Ulang:
            Console.SetCursorPosition(0, 10);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Press Esc ... ");
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
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

                    goto Ulang;
                }
            }
        }
        private void GetDataOrder()
        {

            Console.SetCursorPosition(0, 12);
            Console.WriteLine(PalletID);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, 13);
            Console.WriteLine("Pastikan Status SO Sudah Picked");
            Console.SetCursorPosition(0, 15);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Transmiting Data....           ");

            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select a.STORERKEY,a.ORDERKEY,a.EXTERNORDERKEY,case when isnull(a.TrailerNumber,'')='' then null else a.TrailerNumber end TrailerNumber3,case when isnull(c.BC33,'')='' then 'blm ada doc' else c.bc33 end BC33,  " +
                " case when isnull(c.BC33,'')='' then 'belum bisa di muat' else c.bc33 end Status,b.ID as PalletID,b.SKU,b.JumlahCarton from Orders a  inner join (select orderkey,id,SKU,count(distinct(LOT))as JumlahCarton from PICKDETAIL group by orderkey,id,SKU ) b on a.orderkey = b.ORDERKEY  " +
                " inner join (select orderkey,sku,susr3 as BC33 from orderdetail group by orderkey,sku,susr3) c on a.orderkey = c.orderkey and b.sku = c.sku " +
                " where  b.ID like '%" + PalletID + "%' and a.status>'17' and a.status <'95' ", cn);

            cn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StorerKey = reader.GetString(0);
                        Orderkey = reader.GetString(1);
                        ExternalOrderkey = reader.GetString(2);
                        TraillerNo = reader.GetString(3);
                        AJUNO = reader.GetString(4);
                        StatusPallet = reader.GetString(5);
                        PalletID = reader.GetString(6);
                    }
                    key = "2";
                    Handler();
                    cn.Close();
                }
                catch (Exception ex)
                {

                    Error();
                }
            }
            else
            {
                Console.SetCursorPosition(0, 12);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                      ");
                Console.WriteLine("          Pallet Not Found            ");
                Console.WriteLine("                                      ");
                Console.WriteLine("                                      ");
                key = "0";
                Error();
                Handler();

            }
        }
        private void GetDataOrder2()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select a.STORERKEY,a.ORDERKEY,a.EXTERNORDERKEY,case when isnull(a.TrailerNumber,'')='' then null else a.TrailerNumber end TrailerNumber3,case when isnull(c.BC33,'')='' then 'blm ada doc' else c.bc33 end BC33,  " +
                " case when isnull(c.BC33,'')='' then 'belum bisa di muat' else c.bc33 end Status,b.ID as PalletID,b.SKU,b.JumlahCarton from Orders a  inner join (select orderkey,id,SKU,count(distinct(LOT))as JumlahCarton from PICKDETAIL group by orderkey,id,SKU ) b on a.orderkey = b.ORDERKEY  " +
                " inner join (select orderkey,sku,susr3 as BC33 from orderdetail group by orderkey,sku,susr3) c on a.orderkey = c.orderkey and b.sku = c.sku " +
                " where  b.ID = @palletID1 and a.status>'17' and a.status <'95' ", cn);
            cmd.Parameters.AddWithValue("@palletID1", PalletID);
            cn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        StorerKey = reader.GetString(0);
                        Orderkey = reader.GetString(1);
                        ExternalOrderkey = reader.GetString(2);
                        TraillerNo = reader.GetString(3);
                        AJUNO = reader.GetString(4);
                        StatusPallet = reader.GetString(5);
                        PalletID = reader.GetString(6);
                    }
                    key = "2";
                    Handler();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(0, 11);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("              Pallet Not Found               ");
                    key = "0";
                    Error();
                    Handler();

                }
            }
            else
            {
                Console.SetCursorPosition(0, 12);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                      ");
                Console.WriteLine("          Pallet Not Found            ");
                Console.WriteLine("                                      ");
                Console.WriteLine("                                      ");
                key = "0";
                Error();
                Handler();

            }
        }

        private void CekContainer()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select a.ORDERKEY,trailernumber,id as PalletID from orders a inner join pickdetail b on a.orderkey = b.orderkey " +
            " where (b.id = @palletID1 or b.id = @palletID2 ) and trailernumber =@trailernumber group by a.ORDERKEY, trailernumber, id", cn);
            cmd.Parameters.AddWithValue("@PalletID1", PalletID);
            cmd.Parameters.AddWithValue("@PalletID2", PalletID);
            cmd.Parameters.AddWithValue("@trailernumber", TraillerNo);
            cn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {

                Console.SetCursorPosition(0, 11);
                Notes = "Sesuai";
                InsertSQL();
                cn.Close();
            }
            else
            {

                Console.SetCursorPosition(0, 11);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                  ");
                Console.WriteLine("                                  ");
                Console.WriteLine("              SALAH               ");
                Console.WriteLine("                                  ");
                Console.WriteLine("                                  ");

                key = "0";
                Error();
                Handler();


            }
        }
        private void InsertSQL()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("insert into tbplbsami_fg_ShipmentLoadingCheck " +
                "(Orderkey,ExternalOrderkey,PalletID,ContainerNo,Notes,EditDate,EditWho,Storerkey,AJUNO,QRcontent) " +
                "values (@Orderkey,@ExternalOrderkey,@PalletID,@ContainerNo,@Notes,getdate(),@EditWho,@storerkey,@AJUNO,@QRcontent)", cn);
            cmd.Parameters.Add(new SqlParameter("Orderkey", Orderkey));
            cmd.Parameters.Add(new SqlParameter("EXternalOrderkey", ExternalOrderkey));
            cmd.Parameters.Add(new SqlParameter("PalletID", PalletID));
            cmd.Parameters.Add(new SqlParameter("ContainerNo", TraillerNo));
            cmd.Parameters.Add(new SqlParameter("Notes", Notes));
            cmd.Parameters.Add(new SqlParameter("EditWho", LoginForm.NIK + '-' + LoginForm.UserName));
            cmd.Parameters.Add(new SqlParameter("Storerkey", StorerKey));
            cmd.Parameters.Add(new SqlParameter("AJUNO", AJUNO));
            cmd.Parameters.Add(new SqlParameter("QRcontent", txt_QRcontent));


            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                cn.Close();
                Console.SetCursorPosition(0, 11);
                Console.WriteLine("Data berhasil di simpan");
                Console.ReadKey();
                key = "0";
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
        private void sumSCAN()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select cast(count(distinct(PalletID))as int)as Pallet from tbplbsami_fg_ShipmentLoadingCheck  where ContainerNO=@trailerNumber  and orderkey='" + Orderkey + "'", cn);
            cmd.Parameters.AddWithValue("@trailerNumber", TraillerNo); cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Scan = reader.GetInt32(0);
            }
            cn.Close();
        }
        private void SumExpected()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select cast(count(distinct(id))as int)Pallet from orders a inner join pickdetail b on a.orderkey = b.orderkey  where TrailerNumber=@trailerNumber and a.orderkey='" + Orderkey + "' ", cn);
            cmd.Parameters.AddWithValue("@trailerNumber", TraillerNo);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Expected = reader.GetInt32(0);
            }
            cn.Close();

        }
    }
}
