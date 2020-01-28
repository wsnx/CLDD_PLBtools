
using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class UnloadForm : ConfigDB
    {
        private static int ScanOK;
        private static int ScanSelisih;
        public static string QRSAMI;
        public static string ASN;
        private static string NoMobil = "";
        private static string CartonID = "";
        private static string Carline = "";
        private static string NIK = "";
        private static string Loc = "";
        private static string PalletID = "";
        private static string SKU = "";
        private static int Qty;
        private static DateTime Editdate = DateTime.Now;
        public static string key;
        private static string strKey = "";
        private static Int32 Expected = 0;
        private static Int32 Received = 0;
        public static string Delimiter;
        public static Int32 AssySeq;
        public static Int32 CartonIDSeq;
        private static string cek = "";
        public static string ActualPallet = "";
        private static string Notes = "";
        private static int Cek;
        private static string vCek;
        public void Head()
        {
            MainMenu.FormName = "UnloadForm";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
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

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Entry ASN :");
            ASNform();
        }
        public void FormHeader()
        {

            Head();

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("ASN No   :");
            Console.WriteLine("No Mobil :");
            Console.WriteLine("Stage Loc:");
            Console.WriteLine("PalletID :");
            Console.WriteLine("Scan QR  :");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("( OK :" + ScanOK);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Selisih:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ScanSelisih + ")");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Scan ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Received);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" of ");
            Console.WriteLine(Expected + " Carton");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, Console.CursorTop + 1);
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
                        ASN = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "4";
                        ASN = strKey;
                        CekASN();
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
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {

                    key = "4";
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


                        string[] b = strKey.Split(new string[] { "," }, StringSplitOptions.None);
                        PalletID = b[0].ToString();
                        CekPallet();
                        SumExpected();
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
            SumExpected();
            SumScan();
            if (Expected == Received)
            {
                Console.SetCursorPosition(0, 7);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Closed");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                resultSql();
                PalletForm();
            }
            else
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
                    if (cki.Key == ConsoleKey.UpArrow)
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
        }
        public void Handler()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            if (key == "0")
            {
                ClearLine();
                FormHeader();
                Console.SetCursorPosition(12, 2);
                Console.Write(ASN);
                ASNform();

            }
            else if (key == "4")
            {
                sumNoMobil();
                FormHeader();
                Console.SetCursorPosition(12, 2);
                Console.Write(ASN);
                Console.SetCursorPosition(12, 3);
                Console.Write(NoMobil);
                Console.SetCursorPosition(12, 4);
                Console.Write(NoMobil);
                Console.SetCursorPosition(0, 7);
                Console.Write(vCek);
                LocForm();
            }
            else if (key == "1")
            {
                Received = 0;
                FormHeader();
                Console.SetCursorPosition(12, 2);
                Console.Write(ASN);
                Console.SetCursorPosition(12, 3);
                Console.Write(NoMobil);
                Console.SetCursorPosition(12, 4);
                Console.Write(Loc);
                PalletForm();
            }
            //Cartonform
            else if (key == "2")
            {
                Received = 0;
                ScanOK = 0;
                ScanSelisih = 0;
                if (cek == "")
                {
                    SumExcess();
                    SumScan();
                    FormHeader();
                    Console.SetCursorPosition(12, 2);
                    Console.Write(ASN);
                    Console.SetCursorPosition(12, 3);
                    Console.Write(NoMobil);
                    Console.SetCursorPosition(12, 4);
                    Console.Write(Loc);
                    Console.SetCursorPosition(12, 5);
                    Console.Write(PalletID);

                    if (Received > Expected)
                    {
                        Console.SetCursorPosition(0, 7);
                        Console.WriteLine("Excess");
                        Console.Read();
                        resultSql();
                        LocForm();
                    }
                    if (Received == Expected)
                    {
                        resultSql();
                        Console.SetCursorPosition(0, 7);
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Closed");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.ReadKey();
                        key = "4";
                        Handler();

                    }
                    else
                    {
                        SumExpected();
                        SumScan();
                        resultSql();
                        CartonForm();
                    }
                }
                else if (cek == "1")
                {
                    SumExcess();
                    SumExpected();
                    SumScan();
                    FormHeader();
                    Console.SetCursorPosition(12, 2);
                    Console.Write(ASN);
                    Console.SetCursorPosition(12, 3);
                    Console.Write(NoMobil);
                    Console.SetCursorPosition(12, 4);
                    Console.Write(Loc);
                    Console.SetCursorPosition(12, 5);
                    Console.Write(PalletID);
                    Console.SetCursorPosition(0, 8);
                    resultSql();
                    CartonForm();

                }
            }
            else if (key == "3")
            {
                Parser f = new Parser();
                f.Load();
            }
            else
            {
                ASNform();
            }
        }

        private void CekASN()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            SqlCommand cmd = new SqlCommand("Select cast(count(receiptkey)as int) as Asn from receiptdetail where receiptkey=@receiptkey", cn);
            cmd.Parameters.AddWithValue("@receiptkey", ASN);
            cn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cek = reader.GetInt32(0);
                }
            }
            if (Cek >= 1)
            {
                vCek = "";
            }
            else
            {
                vCek = "ASN Not Found";
                Console.SetCursorPosition(0, 7);
                Console.WriteLine(vCek);
                ASNform();
            }

        }
        private void CekPallet()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            SqlCommand cmd = new SqlCommand("Select cast(count(receiptkey)as int) as Asn from receiptdetail where receiptkey=@receiptkey and ALTSKU=@Pallet ", cn);
            cmd.Parameters.AddWithValue("@receiptkey", ASN);
            cmd.Parameters.AddWithValue("@Pallet", PalletID);
            cn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cek = reader.GetInt32(0);
                }
            }
            if (Cek >= 1)
            {
                vCek = "";
            }
            else
            {
                vCek = "Pallet Not Found";
                Console.SetCursorPosition(0, 7);
                Console.WriteLine(vCek);
                PalletForm();
            }

        }
        public void CekSKU()
        {
            try
            {

                SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
                SqlCommand cmd = new SqlCommand("Select cast(count(receiptkey)as int) as Asn from receiptdetail " +
                    " where receiptkey=@receiptkey and " +
                   " ALTSKU=@Pallet and  " +
                    " (Lottable10 =@cartonID )  " +
                    " and sku = @sku ", cn);
                cmd.Parameters.AddWithValue("@receiptkey", ASN);
                cmd.Parameters.AddWithValue("@Pallet", PalletID);
                cmd.Parameters.AddWithValue("@cartonID", Parser.CartonNo);
                cmd.Parameters.AddWithValue("@SKU", Parser.SKU);
                cn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Cek = reader.GetInt32(0);

                        Notes = "OK";
                        UpdateSql();
                    }

                }
                if (Cek >= 1)
                {
                    Notes = "OK";
                    UpdateSql();
                }

                else
                {


                    ErrorMessage();

                }
            }
            catch (Exception ex)
            {
                ErrorMessage();

            }
        }
        private void ErrorMessage()
        {
            CekError();
        ulang:
            FormHeader();
            Console.SetCursorPosition(12, 2);
            Console.Write(ASN);
            Console.SetCursorPosition(12, 3);
            Console.Write(NoMobil);
            Console.SetCursorPosition(12, 4);
            Console.Write(Loc);
            Console.SetCursorPosition(12, 5);
            Console.Write(PalletID);
            Console.SetCursorPosition(12, 6);
            Console.Write(Parser.SKU + " ID : " + Parser.CartonNo);
            Console.SetCursorPosition(0, 7);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("SKU tidak sesuai");
            Console.BackgroundColor = ConsoleColor.Black;
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Enter)
                {
                    key = "2";
                    Handler();
                }
                else
                {
                    goto ulang;
                }
            }


        }
        private void CekError()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            SqlCommand cmd = new SqlCommand("Select receiptkey,sku,Lottable10,ALTSKU from receiptdetail " +
                " where receiptkey=@receiptkey and  " +
                " Lottable10=@CartonID and sku = @sku ", cn);
            cmd.Parameters.AddWithValue("@receiptkey", ASN);
            cmd.Parameters.AddWithValue("@CartonID", Parser.CartonNo);
            cmd.Parameters.AddWithValue("@SKU", Parser.SKU);
            cn.Open();
            SqlDataReader reader = null;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PalletID = reader.GetString(3);
            }
        }
        public void UpdateSql()
        {

            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();

            SqlCommand cmd = new SqlCommand("INSERT INTO tbPLBSAMI_FG_UnloadDetails(" +

                "Receiptkey,NoMobil,Carline,Loc,FromPalletID,SKU,Qty,CartonID,EditDate,UserName,ActualPallet,Notes,PK,QRcontent,AssyCode)" +
                " VALUES (@Receiptkey,@NoMobil,@Carline,@Loc,@FromPalletID,@SKU,@Qty,@CartonID,getDate(),@UserName,@ActualPallet,@Notes,@PK,@QRcontent,@AssyCode)", cn);

            cmd.Parameters.Add(new SqlParameter("Receiptkey", ASN));
            cmd.Parameters.Add(new SqlParameter("NoMobil", NoMobil));
            cmd.Parameters.Add(new SqlParameter("Carline", Carline));
            cmd.Parameters.Add(new SqlParameter("Loc", Loc));
            cmd.Parameters.Add(new SqlParameter("FromPalletID", PalletID));
            cmd.Parameters.Add(new SqlParameter("SKU", Parser.SKU));
            cmd.Parameters.Add(new SqlParameter("Qty", Parser.Qty));
            cmd.Parameters.Add(new SqlParameter("CartonID", Parser.CartonNo));
            // cmd.Parameters.Add(new SqlParameter("Editdate", Editdate));
            cmd.Parameters.Add(new SqlParameter("UserName", LoginForm.NIK + '-' + LoginForm.UserName));
            cmd.Parameters.Add(new SqlParameter("ActualPallet", ActualPallet));
            cmd.Parameters.Add(new SqlParameter("Notes", Notes));
            cmd.Parameters.Add(new SqlParameter("PK", ASN + Parser.SKU + Parser.CartonNo + Parser.AssyCode));
            cmd.Parameters.Add(new SqlParameter("QRcontent", Parser.QRinput));
            cmd.Parameters.Add(new SqlParameter("AssyCode", Parser.AssyCode));

            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Succes");

                cn.Close();
                key = "2";
                SumDetails();
                cek = "1";
                Handler();
            }
            catch (Exception ex)
            {

                key = "2";
                Handler();
            }
        }
        private static void resultSql()
        {
            Console.SetCursorPosition(0, 10);
            SqlConnection cn = new SqlConnection(DBlocal);
            SqlCommand cmd = new SqlCommand("select receiptkey,CartonID,sku,Notes from tbPLBSAMI_FG_UnloadDetails" +
                " WHERE Receiptkey = @RECEIPTKEY and fromPalletID=@FromPalletID order by TransID Desc", cn);
            cmd.Parameters.AddWithValue("@RECEIPTKEY", ASN);
            cmd.Parameters.AddWithValue("@FromPalletID", PalletID);
            cn.Open();
            Console.WriteLine(String.Format("CartonID | SKU "));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0} | {1} | {2}",
                reader[1], reader[2], reader[3]));
            }
            reader.Close();
        }
        private static void resultSql2()
        {


        }
        public static void SumDetails()
        {
            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("Select count(cartonid)as Received from tbplbsami_fg_unloaddetails where RECEIPTKEY=@RECEIPTKEY group by receiptkey", cn);
            cmd.Parameters.AddWithValue("@RECEIPTKEY", ASN);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                Received = reader.GetInt32(0);
            }
            cn.Close();
        }
        public static void SumScan()
        {
            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select cast(count(CartonID) as Int) as CartonID from tbPLBSAMI_FG_UnloadDetails" +
                " where Receiptkey= @RECEIPTKEY  and Notes='OK' " +
                " and  FromPalletID = @FromPalletID " +
                " Group By Receiptkey,FromPalletID", cn);
            cmd.Parameters.AddWithValue("@RECEIPTKEY", ASN);
            cmd.Parameters.AddWithValue("@FromPalletID", PalletID);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Received = reader.GetInt32(0);
            }
            cn.Close();
        }
        public static void sumNoMobil()
        {
            SqlConnection cn = new SqlConnection(conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select b.Containerkey,a.receiptkey from RECEIPTDETAIL a " +
                " inner join receipt b on a.RECEIPTKEY = b.RECEIPTKEY and a.STORERKEY = b.STORERKEY " +
                " where a.receiptkey=@RECEIPTKEY" +
                " group by a.RECEIPTKEY,b.containerkey ", cn);
            cmd.Parameters.AddWithValue("@RECEIPTKEY", ASN);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                NoMobil = reader.GetString(0);
            }
            cn.Close();
        }
        public static void SumExpected()
        {
            SqlConnection cn = new SqlConnection(conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select cast(Count(Lottable10)as Int)as CartonQty,b.containerKey from RECEIPTDETAIL a " +
                " inner join receipt b on a.RECEIPTKEY = b.RECEIPTKEY and a.STORERKEY = b.STORERKEY " +
                " where a.receiptkey=@RECEIPTKEY and a.ALTSKU = @TOID" +
                " group by a.RECEIPTKEY,b.containerKey", cn);
            cmd.Parameters.AddWithValue("@RECEIPTKEY", ASN);
            cmd.Parameters.AddWithValue("@TOID", PalletID);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Expected = reader.GetInt32(0);
                NoMobil = reader.GetString(1);
            }
            cn.Close();
        }
        public static void SumExcess()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand(" select FromPalletID,count(case when Notes= 'OK' then 1 end ) as OK,count(case when Notes= 'NotOK' then 1 end)as Selisih  " +
                " from tbPLBSAMI_FG_UnloadDetails " +
                " where receiptkey=@RECEIPTKEY and fromPalletID = @PalletID" +
                " group by RECEIPTKEY,FromPalletID", cn);
            cmd.Parameters.AddWithValue("@RECEIPTKEY", ASN);
            cmd.Parameters.AddWithValue("@PalletID", PalletID);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ScanOK = reader.GetInt32(1);
                ScanSelisih = reader.GetInt32(2);
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