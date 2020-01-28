using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilityRFtools
{
    class UnloadFormV1:ConfigDB
    {

        private static string strKey;
        private static string txt_ASN="";
        private static string txt_Storerkey = "";
        private static string txt_ExternReceiptkey = "";
        private static string txt_LOC = "";
        private static string txt_PalletID = "";
        private static string txt_Expected ="0";
        private static string txt_receipt = "0";
        private static string txt_Status = "New";
        private static string txt_PalletASN = "";
        private static string txt_Container = "";
        private static string Notes = "";
        private static string key;

        public void Start()
        {
            Head();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Receiptkey[ASN]: ");
            SummaryASN();
            f_ASN();

        }
        public void Head()
        {
            Console.BackgroundColor=ConsoleColor.Black;
            MainMenu.FormName = "UnloadFormV1";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("          Inbound Checking          ");
            Console.WriteLine("____________________________________");
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void FormHeader()

        {
            Head();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Receiptkey[ASN]: "); //1
            Console.WriteLine("Ext.Receiptkey : " );//2
            Console.WriteLine("Storerkey      : " );//3
            Console.WriteLine("Stage Loc      : " ); //4
            Console.WriteLine("PalletID       : " );//5
            Console.WriteLine("Scan QR        : ");//7
            Console.ForegroundColor = ConsoleColor.White;

            //Value
            Console.SetCursorPosition(17, 2);
            Console.WriteLine(txt_ASN);
            Console.SetCursorPosition(17, 3);
            Console.WriteLine(txt_ExternReceiptkey);
            Console.SetCursorPosition(17, 4);
            Console.WriteLine(txt_Storerkey);
            Console.SetCursorPosition(17, 5);
            Console.WriteLine(txt_LOC);
            Console.SetCursorPosition(17, 6);
            Console.WriteLine(txt_PalletID);
            Console.SetCursorPosition(7, 8);
            Console.WriteLine(txt_receipt);

            Console.SetCursorPosition(15, 8);
            Console.WriteLine(txt_Expected);


            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Total :" ) ;
            Console.SetCursorPosition(11, 8);
            Console.WriteLine("OF ");
            Console.SetCursorPosition(18, 8);
            Console.WriteLine("CTN");
            Console.SetCursorPosition(0, 9);

            if (txt_Status=="Closed") {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("               " + txt_Status + "               ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("               " + txt_Status + "               ");

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;

        }

        private void Handler()
        {

            if (key=="0") //ASN_FORM
            {
                txt_ASN = "";
                txt_Storerkey = "";
                txt_ExternReceiptkey = "";
                txt_LOC = "";
                txt_PalletID = "";
                txt_Expected = "0";
                txt_receipt = "0";
                txt_Status = "";
                FormHeader();
                f_ASN();

            }
            else if (key=="1")//Loc_Form
            {

                txt_LOC = "";
                txt_PalletID = "";
                txt_Expected = "0";
                txt_receipt = "0";
                txt_Status = "";
                FormHeader();
                f_LOC();

            }
            else if(key=="2")//Pallet_Form
            {


                txt_Expected = "0";
                txt_receipt = "0";
                txt_Status = "";
                FormHeader();
                f_Pallet();
            }
            else if (key == "3")//SKU_Form
            {
                FormHeader();
                resultSql();
                f_QRContent();
            }
            else if (key == "4")//SKU_Form
            {
                SumScan();
                FormHeader();
                resultSql();
                f_QRContent();
            }
            else if (key == "5")//SKU_Form
            {
                FormHeader();
                ErrorParsing();
            }
        }


        private void backtomenu()
        {
            MainMenu L = new MainMenu();
            L.Menu_Home();

        }
        private void f_ASN()
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
                        txt_ASN = "";
                        goto Ulang;
                    }
                    else
                    {
                        txt_ASN = strKey;
                        CekASN();

                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }
        private  void CekASN()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select  Storerkey,Receiptkey,Externreceiptkey,containerkey from tbplbsami_fg_stgreceiptdetail" +
                " where receiptkey ='"+txt_ASN+ "' group by Storerkey,Receiptkey,Warehousereference,Externreceiptkey,containerkey", cn);
                cn.Open();
            
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txt_Storerkey = reader.GetString(0);
                    txt_ExternReceiptkey = reader.GetString(2);
                    txt_Container = reader.GetString(3);
                }

                key = "1";
                Handler();
                cn.Close();
            }
            else
            {
                Console.SetCursorPosition(0, 14);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                  ");
                Console.WriteLine("                                  ");
                Console.WriteLine("         ASN Not Found            ");
                Console.WriteLine("                                  ");
                Console.WriteLine("                                  ");
                Console.ReadKey();
                key = "0";
                Handler();

            }
        }
        private void f_LOC()
        {
            Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 5);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("                    ");
            Console.SetCursorPosition(16, 5);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape|| cki.Key == ConsoleKey.UpArrow)
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
                        txt_LOC = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "2";
                        txt_LOC = strKey;
                        Handler();
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }
        private void f_Pallet()
        {
            Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 6);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("                    ");
            Console.SetCursorPosition(16, 6);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape|| cki.Key == ConsoleKey.UpArrow)
                {
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
                        txt_PalletID = "";
                        goto Ulang;
                    }
                    else
                    {
                        string[] b = strKey.Split(new string[] { "," }, StringSplitOptions.None);
                        txt_PalletID = b[0].ToString();
                        key = "3";
                        CekPalletASN();
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }

        private void CekPalletASN()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select  a.Receiptkey,cast(count(distinct(a.QRcontent))as varchar)as Expected," +
                "cast(count(distinct(b.qrcontent))as varchar)as Received," +
                "case when count(distinct(a.QRcontent)) = count(distinct(b.qrcontent)) then 'Closed' " +
                "when count(distinct(a.QRcontent)) > count(distinct(b.qrcontent)) then 'InProses' " +
                "when count(distinct(a.QRcontent)) < count(distinct(b.qrcontent)) then 'Over' " +
                "end Status from tbplbsami_fg_stgreceiptdetail a " +
                " left join tbplbsami_fg_unloaddetails b on a.receiptkey = b.receiptkey and a.toid = b.frompalletid and " +
                "a.sku = b.sku and a.lottable10 = b.cartonid " +
                "where a.receiptkey = '"+txt_ASN+"' and toid = '"+txt_PalletID+"' group by a.Receiptkey ", cn);
            cn.Open();

            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    txt_Expected = reader.GetString(1);
                    txt_receipt = reader.GetString(2);
                    txt_Status = reader.GetString(3);
                }

                Handler();
                cn.Close();
            }
            else
            {

                Console.SetCursorPosition(0, 14);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                  ");
                Console.WriteLine("                                  ");
                Console.WriteLine("       Pallet Not Found           ");
                Console.WriteLine("                                  ");
                Console.WriteLine("                                  ");

                key = "2";
                Handler();

            }
        }
        private void f_QRContent()
        {
            if (txt_Status=="Closed")
            {
                Console.SetCursorPosition(0,0);
                Console.ReadKey();
                key = "2";
                Handler();
            }
            else {
                Ulang:
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(16, 7);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("                    ");
                Console.SetCursorPosition(16, 7);
                ConsoleKeyInfo cki;
                strKey = "";
                while (true)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                    {
                        key = "2";
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
                            key = "4";
                            if (strKey.Contains(","))
                            {
                                Parser.QRinput = strKey;
                                Parser l = new Parser();
                                l.Load();
                            }
                            else
                            {
                                goto Ulang;
                            }

                        }
                    }
                    else
                    {
                        strKey = strKey + cki.KeyChar;
                    }
                }
            }
        }
        public void ResultParsing()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            cn.Open();
            SqlCommand cmd = new SqlCommand(" select receiptkey,toid from tbplbsami_fg_stgreceiptdetail " +
                " where receiptkey = @receiptkey " +
                " and sku = @sku " +
                " and lottable10 = @serial " +
                " and substring(lottable09, 0, charindex('~', lottable09)) = @AssyCode " +
                " and replace(QRcontent,'|',',')=@QrContent" +
                " group by receiptkey,toid", cn);
            cmd.Parameters.AddWithValue("@receiptkey", txt_ASN);
            cmd.Parameters.AddWithValue("@sku", Parser.SKU);
            cmd.Parameters.AddWithValue("@serial", Parser.CartonNo);
            cmd.Parameters.AddWithValue("@AssyCode", Parser.AssyCode);
            cmd.Parameters.AddWithValue("@QrContent", Parser.QRinput);
            var result = cmd.ExecuteScalar();
            if (result != null)
            {

                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txt_PalletASN = reader.GetString(1);
                }

                if (txt_PalletASN==txt_PalletID)
                {
                    Notes = "OK";
                }
                else
                {
                    Notes = "OK-Barang tidak Sesuai Pallet";
                }

                cn.Close();
                InsertSql2();
            }
            else
            {
                key = "5";
                Handler();
            }
        }


        private void InsertSql2()
        {
            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Open();
            SqlCommand cmd = new SqlCommand("RWM_SP_UnloadChecking", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("Receiptkey", txt_ASN));
            cmd.Parameters.Add(new SqlParameter("Container", txt_Container));
            cmd.Parameters.Add(new SqlParameter("ExternReceiptkey", txt_ExternReceiptkey));
            cmd.Parameters.Add(new SqlParameter("Loc", txt_LOC));
            cmd.Parameters.Add(new SqlParameter("PalletID", txt_PalletID));
            cmd.Parameters.Add(new SqlParameter("SKU", Parser.SKU));
            cmd.Parameters.Add(new SqlParameter("Qty", Parser.Qty));
            cmd.Parameters.Add(new SqlParameter("CartonID", Parser.CartonNo));
            cmd.Parameters.Add(new SqlParameter("UserName", LoginForm.NIK + '-' + LoginForm.UserName));
            cmd.Parameters.Add(new SqlParameter("Notes", Notes));
            cmd.Parameters.Add(new SqlParameter("QRcontent", Parser.QRinput));
            cmd.Parameters.Add(new SqlParameter("AssyCode", Parser.AssyCode));
            cmd.Parameters.Add(new SqlParameter("LPN", txt_PalletASN));
            cmd.Parameters.Add(new SqlParameter("Storerkey", txt_Storerkey));

            try
            {

                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Succes");
                cn.Close();
                key = "4";
                Handler();
            }
            catch (Exception ex)
            {

                cn.Close();
                key = "4";
                Handler();

            }
        }

        private static void resultSql()
        {

            Console.SetCursorPosition(0, 10);
            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select receiptkey,CartonID,sku,Notes from tbPLBSAMI_FG_UnloadDetails" +
                " WHERE Receiptkey = @RECEIPTKEY and fromPalletID=@FromPalletID order by TransID Desc", cn);
            cmd.Parameters.AddWithValue("@RECEIPTKEY", txt_ASN);
            cmd.Parameters.AddWithValue("@FromPalletID", txt_PalletID);
            cn.Open();
            Console.WriteLine(String.Format("{0,7} | {1,9}  ","CartonID", "SKU"));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0,7}  | {1,9}  #{2}", reader[1], reader[2], reader[3]));
            }
            reader.Close();
            cn.Close();
        }

        private static void SummaryASN()
        {

            Console.SetCursorPosition(0, 4);

            Console.WriteLine("______________________________________");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("            ASN on Process            ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select a.receiptkey,a.storerkey,concat(substring(cast((cast(cast(count(distinct(b.Qrcontent))as float)/cast(count(distinct(a.qrcontent))as float)as decimal(18,3)))*100 as varchar),0,6),'%') from tbplbsami_fg_stgreceiptdetail a  " +
                "left join tbplbsami_fg_unloaddetails b on a.receiptkey = b.receiptkey and a.sku = b.sku and a.lottable10 = b.cartonid and replace(a.qrcontent,'|',',') = b.qrcontent " +
                "where a.storerkey like 'PLBSAM%' and len(a.qrcontent)>1 group by a.receiptkey,a.storerkey", cn);
            cn.Open();
            Console.WriteLine(String.Format("{0,7} | {1,9} | {2,9}|  ", "Receiptkey", "Storerkey", "Percentage" ));
            SqlDataReader reader = cmd.ExecuteReader();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            while (reader.Read())
            {

                if (reader[1].ToString() == "PLBSAMTFG")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(String.Format("{0,7} | {1,9} | {2,9} | ",
                    reader[0], reader[1], reader[2]));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(String.Format("{0,7} | {1,9} | {2,9} | ",
                    reader[0], reader[1], reader[2]));
                }

            }
            reader.Close();

            cn.Close();
        }
        private  void ErrorParsing()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0,10);
            Console.WriteLine("Barang tidak Sesuai ASN        ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hasil Scan");
            Console.WriteLine("SKU      : " +Parser.SKU);
            Console.WriteLine("AssyCode : " +Parser.AssyCode);
            Console.WriteLine("CartonID : " +Parser.CartonNo);
            Console.WriteLine("QR:" + Parser.QRinput);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Scan QR Label yang Benar   !");
            Console.WriteLine("Hubungi Admin untuk Cek ASN");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ReadKey();
            key = "3";
            Handler();
        }

        public void SumScan()
        {
            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select cast(count(CartonID) as varchar) as CartonID from tbPLBSAMI_FG_UnloadDetails" +
                " where Receiptkey= @RECEIPTKEY " +
                " and  FromPalletID = @FromPalletID " +
                " Group By Receiptkey,FromPalletID", cn);
            cmd.Parameters.AddWithValue("@RECEIPTKEY", txt_ASN);
            cmd.Parameters.AddWithValue("@FromPalletID", txt_PalletID);
            cn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txt_receipt = reader.GetString(0);
            }
            cn.Close();
            if (txt_Expected==txt_receipt)
            {
                txt_Status = "Closed";
            }
        }
    }
}
