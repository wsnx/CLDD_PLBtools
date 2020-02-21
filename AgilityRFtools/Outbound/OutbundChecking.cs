using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AgilityRFtools
{
    class OutbundChecking
    {

        SqlConnection ConnWMS = new SqlConnection(ConfigDB.conWMS);
        SqlConnection ConnLocal = new SqlConnection(ConfigDB.DBlocal);
        SqlConnection Conn = new SqlConnection(ConfigDB.conWMS);
        private static string txt_QRPallet;
        private static string r_Pallet;
        private static string Notes;
        private static string txt_SKU;
        private static string txt_Qty;
        private static string strKey = "";
        private static string Orderkey = "";
        private static string key = "";
        private static DateTime Editdate = DateTime.Now;
        private static string PalletID = "";
        private static string PalletID02 = "";

        private static string txt_Pallet = "";
        private static string txt_Pallet02 = "";

        private static string txt_CartonID;
        private static string ExternalOrderkey;
        private static string Storerkey;
        private static int Expected;
        private static int Scan;
        private static int ScanEmpty;
        private static string QRLabel01;
        private static string QRLabel02;
        private static string txt_status;
        private static string r_SKU;
        private static string r_Qty;

        private static string sum_order;
        private static string sum_Empty;

        public void Head()
        {
            MainMenu.FormName = "Outbound";
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black; ;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("         Check Hasil Picking       ");
            Console.WriteLine("____________________________________");

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
        public void start()
        {
            Console.Clear();
            Head();
            Foot();
            MainMenu.FormName = "Outbound";
            Console.BackgroundColor = ConsoleColor.Black;
            Head();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("OrderKey       :");
            ScanSO();

        }
        public void Handler(string key)
        {


            if (key == "0")
            {
                sum_Empty = "0";
                sum_order = "0";
                Scan = 0;
                ScanEmpty = 0;
                txt_status = "";

                Storerkey = "";
                FormHeader(txt_status);
                ScanSO();
            }
            //Get Order
            else if (key == "1")
            {
                getOrderDetail();
                sum_Empty = "0";
                sum_order = "0";
                Scan = 0;
                ScanEmpty = 0;
                txt_SKU = "";
                txt_status = "";
                FormHeader(txt_status);
                Console.SetCursorPosition(16, 2);
                Console.Write(Orderkey);
                Console.SetCursorPosition(16, 3);
                Console.Write(ExternalOrderkey);
                scanPallet();
            }
            //get Pallet
            else if (key == "2")
            {

                FormHeader(txt_status);
                Console.SetCursorPosition(16, 2);
                Console.Write(Orderkey);
                Console.SetCursorPosition(16, 3);
                Console.Write(ExternalOrderkey);
                Console.SetCursorPosition(16, 4);
                Console.Write(txt_Pallet);
                scanPallet2();

            }

            else if (key == "3")
            {

                SumScan();
                SumScanEmpty();
                FormHeader(txt_status);
                Console.SetCursorPosition(16, 2);
                Console.Write(Orderkey);
                Console.SetCursorPosition(16, 3);
                Console.Write(ExternalOrderkey);
                Console.SetCursorPosition(16, 4);
                Console.Write(txt_Pallet);
                Console.SetCursorPosition(16, 5);
                Console.Write(txt_Pallet02);
                if (sum_order == Scan.ToString() && sum_Empty==ScanEmpty.ToString())
                {
                    Console.SetCursorPosition(0,10);
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("          Closed          ");
                    Console.ReadKey();
                    key ="2";
                    Handler(key);
                }
                else 
                {
                    ScanCarton();
                }

            }
        }
        private void Error()
        {
            Console.SetCursorPosition(0, 13);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                                ");
            Console.WriteLine(" Press [Esc]                    ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        ulang:
            string strkey = "";
            ConsoleKeyInfo cki;
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
            {
                Handler(key);
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
        private void getOrderDetail()
        {

            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("Select Orderkey,externorderkey,STORERKEY,b.description from Orders a inner join orderstatussetup b on a.status = b.code " +
             " where Orderkey = @Orderkey group by Orderkey,externorderkey,STORERKEY,b.description", cn);
            cmd.Parameters.AddWithValue("@Orderkey", Orderkey);
            cn.Open();

            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ExternalOrderkey = reader.GetString(1);
                    Storerkey = reader.GetString(2);
                    txt_status = reader.GetString(3);
                }
            }
            else
            {

                Console.SetCursorPosition(0, 12);

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(" ╔══════════════════════════════╗ ");
                Console.WriteLine(" ║       Order Not Found        ║ ");
                Console.WriteLine(" ╚══════════════════════════════╝ ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                ScanSO();

            }

        }

        private void FormHeader(
            string txt_status
            )
        {

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Head();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(" Orderkey      :");
            Console.WriteLine(" External Order:");
            Console.WriteLine(" Pallet No.    :");
            Console.WriteLine(" Pallet No.    :");
            Console.WriteLine(" Scan Carton   :");
            Console.SetCursorPosition(0, 7);
            Console.WriteLine(" Status        :" + txt_status);
            Console.SetCursorPosition(0, 8);
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write(" ∑ Qty Order : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(sum_order);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" |Qty Scaned : ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Scan );

            Console.SetCursorPosition(0, 9);
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write(" ∑ Qty Empty : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(sum_Empty);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" |Qty Scaned : ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ScanEmpty);

            Console.ForegroundColor = ConsoleColor.White;

        }
        private void ScanSO()
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
                        Orderkey = "";
                        goto Ulang;
                    }
                    else
                    {

                        key = "1";
                        Orderkey = strKey;
                        Handler(key);
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }
        private void scanPallet()
        {
        Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 4);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(16, 4);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    PalletID = "";
                    key = "0";

                    Handler(key);

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
                        QRLabel01 = "";
                        goto Ulang;
                    }
                    else
                    {
                        key = "1";
                        PalletValidator.QRlabelInput = strKey;
                        QRLabel01 = strKey;
                        CekPallet();
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }

        private void ScanCarton()
        {
            resultSQL2();
        Ulang:
            Console.SetCursorPosition(16, 6);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(16, 6);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    Parser.QRinput = "";
                    key = "2";
                    Handler(key);
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
            if (Parser.QRinput.Contains("EMPTY"))
            {

                txt_SKU = "EMPTY POLYTAINER";
                Notes = "Sesuai";
                InsertSql();
            }

            else


            {
                SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
                cn.Close();
                SqlCommand cmd = new SqlCommand("select b.lottable09,a.ORDERKEY,a.SKU,b.LOTTABLE10 as CartonID,a.ID as PaletID from PICKDETAIL a inner join LOTATTRIBUTE b on a.lot = b.LOT " +
                    " where a.ID like '%"+PalletID02+"%' and a.orderkey='"+Orderkey+"' and lottable10=@cartonID and a.sku= @sku  " +
                    " group by  a.ORDERKEY,a.SKU,b.lottable10,a.ID,b.lottable09 ", cn);
                cmd.Parameters.AddWithValue("@PalletID", PalletID02);
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
                        Notes = "Sesuai";
                        InsertSql();
                        cn.Close();
                    }
                    else
                    {
                        
                        Console.SetCursorPosition(0,12);
                        Console.WriteLine("                           ");
                        Console.WriteLine("  Data tidak di temukan    ");
                        Console.WriteLine("                           ");

                        Console.ReadKey();
                        key = "2";
                        Error();

                    }
                }
                catch (Exception ex)
                {
                    key = "2";
                    Handler(key);
                }
            }
        }
        private static string lottable09;
        private void InsertSql()
        {

            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("insert into tbplbsami_fg_PickingCheck (orderkey,palletid,sku,cartonid,notes,editdate,editwho,QRcontent,lottable09,QRLabel,QRLabel02) values  " +
                " ( @orderkey,@palletid,@sku,@cartonid,@notes,getdate(),@editwho,@QRcontent,@lottable09,@QRLabel,@QRLabel02)", cn);
            cmd.Parameters.Add(new SqlParameter("Orderkey", Orderkey));
            cmd.Parameters.Add(new SqlParameter("PalletID", PalletID));
            cmd.Parameters.Add(new SqlParameter("SKU", Parser.SKU));
            cmd.Parameters.Add(new SqlParameter("CartonID", Parser.CartonNo));
            cmd.Parameters.Add(new SqlParameter("Notes", Notes));
            cmd.Parameters.Add(new SqlParameter("EditWho", LoginForm.NIK + '-' + LoginForm.UserName));
            cmd.Parameters.Add(new SqlParameter("QRcontent", Parser.QRinput));
            cmd.Parameters.Add(new SqlParameter("Lottable09", lottable09));
            cmd.Parameters.Add(new SqlParameter("QRLabel", QRLabel01));
            cmd.Parameters.Add(new SqlParameter("QRLabel02", QRLabel02));

            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                cn.Close();
                key = "3";
                Handler(key);
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex);
                //Console.ReadKey();
                key = "3";
                Handler(key);
                cn.Close();
            }
        }


        // Dyna Mix
        private void Cek_LabelMixed(
            string Pallet,
            string PalletQR,
            string key
            )
        {

            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            cn.Open();
            SqlCommand cmd = new SqlCommand
            ("declare @result varchar(500)   " +
            "declare @paletID varchar(500)  " +
            "set @paletID='"+Pallet+"'  " +
            "select @result = coalesce(@result+',','')+concat(sku,',',jumlahorder,',',Carton) from   " +
"(select t1.SKU,cast(sum(qty) as int)as jumlahOrder,Carton=stuff(  " +
"	(select  ','+ CAST(b.lottable10 as varchar(max))  from   " +
"	 pickdetail a inner join lotattribute b on a.lot = b.lot   " +
"	where a.sku = t1.sku and a.id =@paletID  " +
"		order by a.sku,cast(b.lottable10 as int)  " +
"		for XML PATH('')  " +
"	)  " +
"	,1,1,''  " +
" )  " +
" from (select a.sku,b.lottable10,a.qty from pickdetail a inner join lotattribute b on a.lot = b.lot   " +
" where id=@paletID )t1  " +
" group by t1.SKU)  " +
" a  " +
"select  " +
"case when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=1 then concat(QRcontent,'3')  " +
"when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=2 then concat(QRcontent,'2')   " +
"when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=3 then concat(QRcontent,'1')  " +
"else concat(QRcontent,'0') end QR, " + //0
"case when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=1 then '3' " +
"when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=2 then '2' " +
"when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=3 then '1'  " +
"else '0' end JumlahEmpty, " + //1
" cast(count(LOTTABLE10)as varchar) jumlahCtn, " + //2
"id as Pallet " + //3
            "from(select a.ID, c.susr6,b.LOTTABLE10,concat(@result,',') as QRcontent,a.QTY from pickdetail a inner join sku c on a.sku = c.sku inner join lotattribute b on a.lot = b.lot   " +
            "where id=@paletID)a " +
            "group by id,QRcontent,SUSR6 ", cn);

            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    r_Pallet = reader.GetString(0);

                    sum_order = reader.GetString(2);
                    sum_Empty = reader.GetString(1);
                    PalletID = reader.GetString(3);

                }
            }
            else
            {
                string vCek = "Pallet Not Found";
                Console.SetCursorPosition(0, 12);
                Console.WriteLine(vCek);
                ScanSO();

            }
            cn.Close();

            if (PalletQR == r_Pallet)
            {

                Handler(key);
            }
            else
            {

                Console.SetCursorPosition(0, 10);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("                            ");
                Console.WriteLine("     Label tidak sesuai     ");
                Console.WriteLine("                            ");

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Serial WMS  : " + r_Pallet);

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Serial Label: " + PalletQR);
                Console.ReadKey();
                
                key = "1";
                Error();

            }

        }

        //Lebel with ""--"
        private void Cek_LabelMixed002(
            string Pallet,
            string sku,
            string qty,
            string PalletQR,
            string key
            )
        {

            Console.SetCursorPosition(0, 12);
            Console.WriteLine("Sedang mengecek isi QR Pallet..");
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            cn.Open();
            SqlCommand cmd = new SqlCommand
            (

            "declare @result varchar(500)  " +
            "declare @paletID varchar(500)   " +
            "set @paletID='" + Pallet + "'   " +
            "select @result = coalesce(@result+',','')+b.lottable10 from   " +
            "pickdetail a inner join lotattribute b on a.lot = b.lot    " +
            "where (id = concat('J',@paletID) or id = concat('T',@paletID)) and orderkey='" + Orderkey + "'" +
            " order by cast(b.lottable10 as int)  " +
            "select  " +
            "case when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=1 then concat(QRcontent,'3') " +
            "when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=2 then concat(QRcontent,'2')  " +
            "when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=3 then concat(QRcontent,'1') " +
            "else concat(QRcontent,'0') " +
            "end QR," + //0

            "a.sku," + //1

            "cast(sum(a.qty)as varchar)as Qty , " + //2
            "cast(count(lottable10) as varchar) as JumlahCtn, " + //3

            "case when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=1 then '3' " +
            "when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=2 then '2' " +
            "when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=3 then '1' " +
            "else '0' end JumlahEmpty ,Pallet " +  //4

            "from ( select  a.ID as Pallet,concat(@result,',')as QRcontent,a.sku,a.qty,lottable10 ,c.susr6 " +
            "from pickdetail a  " +
            "inner join sku c on a.sku = c.sku  " +
            "inner join lotattribute b on a.lot = b.lot    " +
            "where (id = concat('J',@paletID) or id = concat('T',@paletID) ) and a.orderkey='" + Orderkey + "' " +
            ")a group by a.QRcontent,a.sku,susr6 ,Pallet"
            ,
            cn);

            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    r_Pallet = reader.GetString(0);
                    r_SKU = reader.GetString(1);
                    r_Qty = reader.GetString(2);

                    sum_order = reader.GetString(3);
                    sum_Empty = reader.GetString(4);
                    PalletID = reader.GetString(5);
                }

                if (PalletQR == r_Pallet)
                {

                }
                else
                {
                    key = "2";
                    Console.SetCursorPosition(0, 10);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("                            ");
                    Console.WriteLine("     Label tidak sesuai     ");
                    Console.WriteLine("                            ");

                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Serial WMS  : " + r_Pallet);

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Serial Label: " + PalletQR);
                    Console.ReadKey();
                    key = "1";
                    Handler(key);
                }
                if (sku == r_SKU)
                {

                }
                else
                {
                    key = "2";
                    Console.SetCursorPosition(0, 10);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("                       ");
                    Console.WriteLine("     SKU Salah         ");
                    Console.WriteLine("                       ");
                    Console.WriteLine("SKU WMS  : " + r_SKU);
                    Console.WriteLine("SKU Label: " + sku);
                    Console.ReadKey();
                    key = "1";
                    Handler(key);
                }
                if (qty + ".00000" == r_Qty)
                {

                }
                else
                {

                    Console.SetCursorPosition(0, 10);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("                       ");
                    Console.WriteLine("     Qty SALAH         ");
                    Console.WriteLine("                       ");
                    Console.WriteLine(qty);
                    Console.WriteLine(r_Qty);
                    Console.ReadKey();
                    key = "1";
                    Handler(key);
                }
                Handler(key);

            }
            else
            {
                string vCek = "Pallet Not Found";
                Console.SetCursorPosition(0, 12);
                Console.WriteLine(vCek);
                key = "1";
                Error();

            }
            cn.Close();

            if (PalletQR == r_Pallet)
            {


                Handler(key);
            }
            else
            {

                key = "1";
                Error();

            }

        }

        private void Cek_LabelMixed003(
        string Pallet,
        string sku,
        string qty,
        string PalletQR,
        string key
         )
        {

            Console.SetCursorPosition(0, 12);
            Console.WriteLine("Sedang mengecek isi QR Pallet..");
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            cn.Open();
            SqlCommand cmd = new SqlCommand
            (

            "declare @result varchar(500)  " +
            "declare @paletID varchar(500)   " +
            "set @paletID='" + Pallet + "'   " +
            "select @result = coalesce(@result+',','')+b.lottable10 from   " +
            "pickdetail a inner join lotattribute b on a.lot = b.lot    " +
            "where (id = @paletID) and orderkey='" + Orderkey + "'" +
            " order by cast(b.lottable10 as int)  " +
            "select  " +
            "case when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=1 then concat(QRcontent,'3') " +
            "when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=2 then concat(QRcontent,'2')  " +
            "when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=3 then concat(QRcontent,'1') " +
            "else concat(QRcontent,'0') " +
            "end QR," + //0

            "a.sku," + //1

            "cast(sum(a.qty)as varchar)as Qty , " + //2
            "cast(count(lottable10) as varchar) as JumlahCtn, " + //3

            "case when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=1 then '3' " +
            "when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=2 then '2' " +
            "when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=3 then '1' " +
            "else '0' end JumlahEmpty ,Pallet " +  //4

            "from ( select  a.ID as Pallet,concat(@result,',')as QRcontent,a.sku,a.qty,lottable10 ,c.susr6 " +
            "from pickdetail a  " +
            "inner join sku c on a.sku = c.sku  " +
            "inner join lotattribute b on a.lot = b.lot    " +
            "where (id = @paletID) and a.orderkey='" + Orderkey + "' " +
            ")a group by a.QRcontent,a.sku,susr6 ,Pallet"
            ,
            cn);

            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    r_Pallet = reader.GetString(0);
                    r_SKU = reader.GetString(1);
                    r_Qty = reader.GetString(2);

                    sum_order = reader.GetString(3);
                    sum_Empty = reader.GetString(4);
                    PalletID = reader.GetString(5);
                }

                if (PalletQR == r_Pallet)
                {

                }
                else
                {
                    key = "2";
                    Console.SetCursorPosition(0, 10);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("                                 ");
                    Console.WriteLine("       Label Tidak Sesuai        ");
                    Console.WriteLine("                                 ");

                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Serial WMS  : " + r_Pallet);

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Serial Label: " + PalletQR);
                    Console.ReadKey();
                    key = "1";
                    Handler(key);
                }
                if (sku == r_SKU)
                {

                }
                else
                {
                    key = "2";
                    Console.SetCursorPosition(0, 10);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("                       ");
                    Console.WriteLine("     SKU Salah         ");
                    Console.WriteLine("                       ");
                    Console.WriteLine("SKU WMS  : " + r_SKU);
                    Console.WriteLine("SKU Label: " + sku);
                    Console.ReadKey();
                    key = "1";
                    Handler(key);
                }
                if (qty + ".00000" == r_Qty)
                {

                }
                else
                {

                    Console.SetCursorPosition(0, 10);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("                       ");
                    Console.WriteLine("     Qty SALAH         ");
                    Console.WriteLine("                       ");
                    Console.WriteLine(qty);
                    Console.WriteLine(r_Qty);
                    Console.ReadKey();
                    key = "1";
                    Handler(key);
                }
                Handler(key);

            }
            else
            {
                string vCek = "Pallet Not Found";
                Console.SetCursorPosition(0, 12);
                Console.WriteLine(vCek);
                key = "1";
                Error();

            }
            cn.Close();

            if (PalletQR == r_Pallet)
            {


                Handler(key);
            }
            else
            {


                key = "1";
                Error();

            }

        }


        //Pallet Non Mixed

        private void Cek_LabelNonMixed
            (string pallet,
             string SKU,
             string Qty,
             string key)
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select orderkey ,id,SKU,cast(sum(qty) as varchar)as QtyOrder from pickdetail " +
                "where id like'%" + pallet.ToString() + "' and orderkey=@orderkey" +
                " group by id,orderkey,SKU order by orderkey desc", cn);
            cmd.Parameters.AddWithValue("@Orderkey", Orderkey);
            cn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txt_SKU = reader.GetString(2);
                    txt_Qty = reader.GetString(3);
                }
            }
            else
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Pallet Not Found     ");
                Error();

            }

            if (txt_SKU == SKU)
            {

            }
            else
            {

                Console.SetCursorPosition(0, 10);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("                       ");
                Console.WriteLine("       SKU SALAH       ");
                Console.WriteLine("                       ");
                Console.ReadKey();

            }

            if (txt_Qty.ToString() == Qty + ".00000")
            {

            }
            else
            {
                Console.SetCursorPosition(0, 10);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("                          ");
                Console.WriteLine("         QTY SALAH        ");
                Console.WriteLine("                          ");
                Console.ReadKey();

            }

            Handler(key);
        }

        //Validasi Pallet Depan
        private void CekPallet()
        {
            //PalletMazda TAP
            if (QRLabel01.Contains("MAZDA - 19-") || QRLabel01.Contains("MAZDA - 20-"))
            {

                QRLabel02 = QRLabel01;
                string[] x = QRLabel01.Split(new string[] { "," }, StringSplitOptions.None);
                string a = x[0].ToString();
                string b = x[1].ToString();
                string c = a.Substring(15, 5);

                PalletID = "1MZD9-" + c;
                PalletID02 = PalletID;
                txt_Pallet = PalletID;
                txt_Pallet02 = PalletID;

                string d = b.Replace(".", ",");
                string[] y = d.Split(new string[] { "," }, StringSplitOptions.None);
                string sku = y[0].ToString();
                string qty = y[1].ToString();
                int xx = a.ToString().Length - 1;

                string[] xy = QRLabel01.Split(new string[] { "--" }, StringSplitOptions.None);
                string Content = xy[1].ToString();

                key = "3";
                Cek_LabelMixed003(txt_Pallet, sku, qty, Content, key);

            }

            //Pallet Dyna
            if (QRLabel01.Contains("- 19,") || QRLabel01.Contains("- 20,"))
            {

                string[] x = QRLabel01.Split(new string[] { "," }, StringSplitOptions.None);
                string a = x[0].ToString();
                int b = a.Length + 1;
                int c = QRLabel01.Length;
                txt_QRPallet = QRLabel01.Substring(b, c - b);
                txt_Pallet = a;
                int xx = a.ToString().Length - 1;
                if (QRLabel01.Contains("DYNA"))
                {
                    int xxX = a.ToString().Length - 1;
                    PalletID = a.Substring(4, 15);
                    PalletID = "TS. " + PalletID;
                    key = "2";
                    Cek_LabelMixed(PalletID,
                        txt_QRPallet,
                        key);

                }
                else
                {
                    //                    QRLabel01 = "J" + a.Substring(1, xx - 5);
                    key = "1";
                    Handler(key);
                }
            }
            //Pallet non Mixed "General Label"
            if (QRLabel01.Contains("--"))
            {
                key = "2";
                string[] x = QRLabel01.Split(new string[] { "," }, StringSplitOptions.None);
                string a = x[0].ToString();
                int xx = a.ToString().Length - 1;
                string pallet = a.Substring(1, xx);

                //Get SKU
                string aa = x[1].ToString();
                string[] y = aa.Split(new string[] { "." }, StringSplitOptions.None);
                string sku = y[0].ToString();
                string qty = y[1].ToString();

                string[] xy = QRLabel01.Split(new string[] { "--" }, StringSplitOptions.None);
                string Content = xy[1].ToString();
                txt_Pallet = a;
                PalletID = pallet;
                PalletID02 = pallet;


                key = "2";
                Cek_LabelMixed002(pallet, sku, qty, Content, key);
            }
            if (QRLabel01.Contains("-.") && QRLabel01.Contains("MAZDA"))
            {
                key = "2";
                string[] x = QRLabel01.Split(new string[] { "," }, StringSplitOptions.None);
                string a = x[0].ToString();
                int xx = a.ToString().Length - 1;
                string pallet = a.Substring(1, xx);

                //Get SKU
                string aa = x[1].ToString();
                string[] y = aa.Split(new string[] { "." }, StringSplitOptions.None);
                string sku = y[0].ToString();
                string qty = y[1].ToString();

                string[] xy = QRLabel01.Split(new string[] { "-." }, StringSplitOptions.None);
                string Content = xy[1].ToString();
                txt_Pallet = a;
                key = "2";
                PalletID = pallet;
                PalletID02 = pallet;

                Cek_LabelMixed002(pallet, sku, qty, Content, key);


            }

            if (QRLabel01.Contains(","))
            {

                key = "2";
                string[] x = QRLabel01.Split(new string[] { "," }, StringSplitOptions.None);
                string aa = QRLabel01.Replace('.', ',');
                string[] y = aa.Split(new string[] { "," }, StringSplitOptions.None);
                string a = x[0].ToString();
                string sku = y[2].ToString();
                string qty = y[3].ToString();
                int xx = a.ToString().Length - 1;
                string pallet = a.Substring(1, xx);
                txt_Pallet = a;
                key = "2";
                PalletID = pallet;
                PalletID02 = pallet;

                string xyz = ","+qty.ToString() + ",";
                string[] xy = QRLabel01.Split(new string[] { xyz.ToString() }, StringSplitOptions.None);
                string Content = xy[1].ToString();

                Cek_LabelMixed002(pallet, sku, qty, Content, key);
            }
            //Master Pallet
            if (QRLabel01.Contains("1HND") || QRLabel01.Contains("4SYT"))
            {

                QRLabel02 = QRLabel01;
                PalletID02 = QRLabel01;
                txt_Pallet = QRLabel01;
                txt_Pallet02 = "Master Pallet";
                SumOrder();
                key = "3";
                Handler(key);
            }
            else
            {
                Error();
            }

        }

        private void scanPallet2()
        {
        Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
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
                    QRLabel02 = "";
                    key = "1";
                    Handler(key);
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
                        QRLabel02 = "";
                        goto Ulang;
                    }
                    else
                    {

                        QRLabel02 = strKey;
                        CekPallet2();
                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }

        private void CekPallet2()
        {


            if ((QRLabel01.Contains("DS.") && QRLabel02.Contains("DS.")) || (QRLabel01.Contains("BS.") && QRLabel02.Contains("BS.")))
            {
                Console.SetCursorPosition(0, 10);
                Console.Write("Pallet Tidak sesuai");
                key = "2";
                Error();
            }

            else if (


                ((QRLabel01.Contains("DS*") && QRLabel02.Contains("BS*"))
                || (QRLabel01.Contains("BS*") && QRLabel02.Contains("DS*")))
                || QRLabel01.Contains("1HND")
                || QRLabel01.Contains("4SYT")
                || QRLabel01.Contains("1MZD")
                || ((QRLabel01.Contains("DS.") && QRLabel02.Contains("BS."))
                || (QRLabel01.Contains("BS.") && QRLabel02.Contains("DS."))))

            {



                //Pallet non Mixed "General Label"

                if (QRLabel02.Contains(","))
                {
                    //Validate QR Depan dan QR Belakang
                    string[] x = QRLabel02.Split(new string[] { "," }, StringSplitOptions.None);
                    string a = x[0].ToString();
                    txt_Pallet02 = a;
                    
                    int xx = QRLabel02.Length - 1;
                    string b = QRLabel02.Substring(1, xx);

                    int xy = QRLabel01.Length - 1;
                    string c = QRLabel01.Substring(1, xy);

                    if (b==c)
                    {
                        key = "3";
                        Handler(key);
                    }
                    else
                    {
                        Console.SetCursorPosition(0,12);
                        Console.WriteLine("Pallet Depan dan Belakang tdk Sesuai");
                        key = "2";
                        Error();
                    }

                }
                //Master Pallet
                if (PalletID.Contains("1HND") || PalletID.Contains("4SYT"))
                {

                    QRLabel02 = PalletID;
                    QRLabel01 = PalletID;
                    txt_Pallet = PalletID;
                    txt_Pallet02 = "";
                    key = "3";
                    Handler(key);
                }
                else
                {
                    Error();
                }
            }
            else
            {
                Console.SetCursorPosition(0, 10);

                Console.Write("Pallet Tidak sesuai");
                key = "2";
                Error();
            }

        }

        private void SumOrder()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand(
                "Select a.ORDERKEY," +//0
                "cast(Count(LOTTABLE10)as Int) as JumlahCarton," + //1
                "a.ID as PaletID , " + //2
"case when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=1 then '3' " +
"when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=2 then '2' " +
"when (susr6 like '%PT%' or susr6 like '%POLYTAINER%') and count(lottable10)%4=3 then '1'  " +
"else '0' end JumlahEmpty " + //3
"from PICKDETAIL a  inner join sku c  on a.sku = c.sku inner join receiptdetail b on a.lot = b.toLOT " +
"where a.ID = '"+PalletID02+"' and a.Orderkey like '"+Orderkey+"' " +
"group by a.ORDERKEY,a.ID,c.SUSR6,a.ORDERKEY "
                , cn);

            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Expected = reader.GetInt32(1);
                sum_Empty = reader.GetString(3);
                PalletID = reader.GetString(2);
                //txt_SKU = reader.GetString(1);
            }
            sum_order = Expected.ToString();
            cn.Close();
        }
        //Old
        private void SumScan()
        {

            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("Select count(distinct(QRcontent)) as JumlahCarton from tbPLBSAMI_fg_PickingCheck " +
            " where PalletID like '%" + PalletID + "%' and orderkey like'%" + Orderkey + "%'and sku not like '%Empty%' " +
            " group by orderkey,Palletid ", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Scan = reader.GetInt32(0);
            }
            cn.Close();

        }
        private void SumScanEmpty()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("Select count(distinct(CartonID)) as JumlahCarton from tbPLBSAMI_fg_PickingCheck " +
            " where PalletID like '%"+PalletID+"%'  and orderkey ='"+Orderkey+"'and sku  like '%Empty%' " +
            " group by orderkey,Palletid ", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ScanEmpty = reader.GetInt32(0);

            }
            cn.Close();

        }

        private void resultSQL2()
        {
            try
            {
                //WMS

                ConnWMS.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnWMS;
                cmd.CommandText = ("select top 10 Concat(a.sku,LOTTABLE10)as ID,LOTTABLE10 as CartonID from PICKDETAIL a inner join LOTATTRIBUTE b on a.lot = b.LOT  " +
                " where a.orderkey = '"+Orderkey+"' and a.ID like'%" + PalletID02 + "' " +
                "group by a.orderkey, a.SKU, a.ID, LOTTABLE10 order by cast(lottable10 as int)");
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("Orderkey", Orderkey);
                cmd.Parameters.AddWithValue("PalletID", PalletID02);

                DataSet dsWMS = new DataSet();
                DA.Fill(dsWMS);
                DataTable Order = dsWMS.Tables[0];
                ConnWMS.Close();

                //Local
                ConnLocal.Open();
                cmd.Connection = ConnLocal;
                cmd.CommandText = " select Concat(SKU,CARTONID)as ID,CartonID from tbPLBSAMI_FG_PickingCheck " +
                    " where orderkey = '"+Orderkey+"' and PalletID like '%" + PalletID02 + "'" +
                    " group by Orderkey,SKU,CARTONID";

                SqlDataAdapter DALocal = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("Orderkey2", Orderkey);
                cmd.Parameters.AddWithValue("PalletID2", PalletID);
                DataSet dsLocal = new DataSet();
                DALocal.Fill(dsLocal);
                DataTable OrdersLocal = dsLocal.Tables[0];
                ConnLocal.Close();

                //Joining
                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("CartonID", typeof(string));
                dtResult.Columns.Add("Notes", typeof(string));


                var diff = Order.AsEnumerable().Except(OrdersLocal.AsEnumerable(),
                                                                    DataRowComparer.Default);
                Console.SetCursorPosition(0, 10);
                Console.WriteLine(String.Format("                           "));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(String.Format("List Carton Belum Scan :"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("CartonID");
                foreach (var q in diff)
                {
                    Console.WriteLine("{0}   ",
                    q.Field<string>("CartonID"));
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
            }
        }
    }
}


