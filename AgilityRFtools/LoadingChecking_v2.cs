using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class LoadingChecking_v2
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
        private static string Notes;

        private static string QRLabel01;
        private static string r_Pallet;
        private static string r_SKU;
        private static string r_Qty;

        private static string sum_order;
        private static string sum_Empty;

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
            Console.WriteLine("Orderkey      :" );
            Console.WriteLine("ExternOrder   :");
            Console.WriteLine("__________________________________");
            Console.SetCursorPosition(0, 8);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Qty Order WMS: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" " +sum_order);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Qty Empty : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" " + sum_Empty);

            Console.SetCursorPosition(0, 9);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Qty Cek Pick : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" " + txt_EmptypickingCek);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Qty Empty : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" " + txt_pickingCek);


            Console.SetCursorPosition(0, 10);
            Console.WriteLine("__________________________________");
            Console.SetCursorPosition(0, 11);
            Console.Write("Scan ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Scan);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Of ");
            Console.Write(Expected);
            Console.Write(" PALLET ");
            Console.SetCursorPosition(0, 12);
            Console.WriteLine("__________________________________");
            Console.Write("Container No >>");

        }
        private void Handler(string key)
        {
            if (key == "0")
            {

                Scan = 0;
                Expected = 0;
                sum_Empty = "0";
                sum_order = "0";
                txt_pickingCek = "0";
                txt_EmptypickingCek = "0";

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
                CekPickingStatus();
            }
            else if (key == "3")
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
                ScanContainerForm();
            }
        }
        private static int qtyScan;
        private static int qtyEmpty;

        private static string txt_pickingCek;
        private static string txt_EmptypickingCek;

        private void CekPickingStatus()
        {

            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand
                (
                "declare @pallet varchar(50) " +
                "declare @orderkey varchar(20) " +
                "set @pallet='"+PalletID+"' " +
                "set @orderkey='"+Orderkey+"' " +
                "select orderkey,PalletID,cast(sum(EmptyPolly) as varchar)as EmptyPolly,cast(sum(Ctn)as Varchar)as Carton  from " +
                "(select orderkey,PalletID,case when QRcontent like '%EMPTY%' then 1 else 0 end EmptyPolly, " +
                "case when QRcontent like '%EMPTY%' then 0 else 1 end Ctn  from tbPLBSAMI_fg_PickingCheck " +
                "where PalletID =@pallet and Orderkey=@orderkey group by orderkey,PalletID,QRcontent )a  " +
                "group by orderkey,PalletID "
                ,
                cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txt_pickingCek = reader.GetString(2);
                txt_EmptypickingCek = reader.GetString(3);
            }
            cn.Close();

            int a = Convert.ToInt32(sum_order)+ Convert.ToInt32(sum_Empty);
            int b = Convert.ToInt32(txt_EmptypickingCek) + Convert.ToInt32(txt_pickingCek);
            if (a==b)
            {
                GetDataOrder();

            }
            else

            {
                Console.SetCursorPosition(0,12);
                Console.WriteLine("* Blm Scan Picking               ");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" qty Order :"+ sum_order + " Empty "+ sum_Empty );
                Console.WriteLine(" qty Scan  :"+ txt_pickingCek+ " Empty " + txt_EmptypickingCek);
                Console.ReadKey();
                Error();
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
                        QRLabel01 = "";
                        goto Ulang;
                    }
                    else
                    {
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

        private void CekPallet()
        {
            PalletID = "";
            Orderkey = "";
            sum_Empty = "";
            sum_order = "";

            string txt_QRPallet;
            //PalletMazda TAP
            if (QRLabel01.Contains("MAZDA - 19-") || QRLabel01.Contains("MAZDA - 20-"))
            {


                string[] x = QRLabel01.Split(new string[] { "," }, StringSplitOptions.None);
                string a = x[0].ToString();
                string b = x[1].ToString();
                string c = a.Substring(15, 5);

                PalletID = "1MZD9-" + c;

                string d = b.Replace(".", ",");
                string[] y = d.Split(new string[] { "," }, StringSplitOptions.None);
                string sku = y[0].ToString();
                string qty = y[1].ToString();
                int xx = a.ToString().Length - 1;

                string[] xy = QRLabel01.Split(new string[] { "--" }, StringSplitOptions.None);
                string Content = xy[1].ToString();

                key = "2";
                Cek_LabelMixed003(PalletID, sku, qty, Content, key);

            }

            //Pallet Dyna
            if (QRLabel01.Contains("- 19,") || QRLabel01.Contains("- 20,"))
            {

                string[] x = QRLabel01.Split(new string[] { "," }, StringSplitOptions.None);
                string a = x[0].ToString();
                int b = a.Length + 1;
                int c = QRLabel01.Length;
                txt_QRPallet = QRLabel01.Substring(b, c - b);
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
                PalletID = pallet;
                

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
                key = "2";
                PalletID = pallet;
               
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
                key = "2";
                PalletID = pallet;
                string xyz = "," + qty.ToString() + ",";
                string[] xy = QRLabel01.Split(new string[] { xyz.ToString() }, StringSplitOptions.None);
                string Content = xy[1].ToString();

                Cek_LabelMixed002(pallet, sku, qty, Content, key);
            }

            //Master Pallet
            if (QRLabel01.Contains("1HND") || QRLabel01.Contains("4SYT"))
            {
                PalletID =QRLabel01;
                CekLabelMaster();
                
            }
            else
            {
                Error();
            }

        }
        private void CekLabelMaster()
        {

            Console.SetCursorPosition(0,12);
            Console.WriteLine("sedang Mengecek isi QR");

            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand(

              "Select a.id,a.orderkey,cast(count(lot)as varchar)as JumlahCtn, " +
"case when (c.SUSR6 like '%PT%' or c.SUSR6 like '%POLYTAINER%') and count(lot)%4=1 then '3'  " +
"when (c.SUSR6 like '%PT%' or c.SUSR6 like '%POLYTAINER%') and count(lot)%4=2 then '2'  " +
"when (c.SUSR6 like '%PT%' or c.SUSR6 like '%POLYTAINER%') and count(lot)%4=3 then '1'  " +
"else '0' end JumlahEmpty from pickdetail a " +
"inner join sku c on a.sku = c.SKU  " +
"where a.ID = '"+PalletID+"' " +
"group by a.id,a.ORDERKEY,susr6 "
                , cn);
            cn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                       PalletID = reader.GetString(0);
                       Orderkey = reader.GetString(1);

                       sum_order = reader.GetString(2);
                       sum_Empty = reader.GetString(3);
                    }
                    key = "2";
                    Handler(key);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(0, 11);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("              Pallet Not Found                "+ex);
                    key = "0";
                    Error();
                    Handler(key);

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
                Handler(key);

            }
        }


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
            "set @paletID='" + Pallet + "'  " +
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
"id as Pallet ," +//3
"orderkey " + //4
            "from(select a.orderkey,a.ID, c.susr6,b.LOTTABLE10,concat(@result,',') as QRcontent,a.QTY from pickdetail a inner join sku c on a.sku = c.sku inner join lotattribute b on a.lot = b.lot   " +
            "where id=@paletID)a " +
            "group by id,QRcontent,SUSR6 ,orderkey", cn);

            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    r_Pallet = reader.GetString(0);
                    sum_Empty = reader.GetString(1);
                    sum_order = reader.GetString(2);
                    PalletID = reader.GetString(3);
                    Orderkey = reader.GetString(4);

                }
            }
            else
            {
                string vCek = "Pallet Not Found";
                Console.SetCursorPosition(0, 12);
                Console.WriteLine(vCek);
                Error();
            }
            cn.Close();

            if (PalletQR == r_Pallet)
            {
                key = "2";
                Handler(key);
            }
            else
            {
                PalletValidateFailure(PalletQR);

            }

        }

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
                "pickdetail a inner join lotattribute b on a.lot = b.lot   " +
                " inner join orderdetail d on a.orderlinenumber = d.orderlinenumber and a.orderkey = d.orderkey " +
                "where (a.id = concat('J',@paletID) or a.id = concat('T',@paletID)) and d.status<95  " +
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
                "else '0' end JumlahEmpty " + //4
                ",Pallet " + //5
                ",orderkey " +  //6

                "from ( select  a.orderkey,a.ID as Pallet,concat(@result,',')as QRcontent,a.sku,a.qty,b.lottable10 ,c.susr6 " +
                "from pickdetail a  " +
                "inner join sku c on a.sku = c.sku  " +
                "inner join lotattribute b on a.lot = b.lot " +
                " inner join orderdetail d on a.orderlinenumber = d.orderlinenumber and a.orderkey = d.orderkey " +
                "where (a.id = concat('J',@paletID) or a.id = concat('T',@paletID) ) and d.status<95  " +
                ")a group by a.QRcontent,a.sku,susr6 ,Pallet,orderkey"
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
                    sum_Empty= reader.GetString(4);
                    sum_order = reader.GetString(3);

                    PalletID = reader.GetString(5);
                    Orderkey = reader.GetString(6);
                }

                if (PalletQR == r_Pallet)
                {

                }
                else
                {

                    PalletValidateFailure(PalletQR);
                }
                if (sku == r_SKU)
                {

                }
                else
                {

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
                cn.Close();
                key = "2";
                Handler(key);
            }
            else
            {
                string vCek = " Pallet Not Found             ";
                Console.SetCursorPosition(0, 11);
                Console.WriteLine(vCek);
                key = "1";
                Error();

            }
            cn.Close();
            key = "2";
            Handler(key);
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
            "inner join orderdetail d on a.orderlinenumber = d.orderlinenumber and a.orderkey = d.orderkey " +
            "where (a.id = @paletID) and d.status<95" +
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
            "else '0' end JumlahEmpty ," + //4
            "Pallet,orderkey " +  

            "from ( select  a.orderkey,a.ID as Pallet,concat(@result,',')as QRcontent,a.sku,a.qty,b.lottable10 ,c.susr6 " +
            "from pickdetail a  " +
            "inner join sku c on a.sku = c.sku  " +
            "inner join lotattribute b on a.lot = b.lot  " +
            "inner join orderdetail d on a.orderlinenumber = d.orderlinenumber and a.orderkey = d.orderkey  " +
            "where (a.id = @paletID)  and d.status<95" +
            ")a group by a.QRcontent,a.sku,susr6 ,Pallet,orderkey"
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
                    sum_Empty = reader.GetString(4);
                    sum_order = reader.GetString(3);

                    PalletID = reader.GetString(5);
                    Orderkey = reader.GetString(6);
                }

                if (PalletQR == r_Pallet)
                {

                }
                else
                {
                    PalletValidateFailure(PalletQR);

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
                    key = "0";
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
                    key = "0";
                   Handler(key);
                }
                key = "2";
                Handler(key);

            }
            else
            {
                
                Console.SetCursorPosition(0, 12);
                Console.WriteLine("                           ");
                Console.WriteLine("    Data Tidak Ditemukan   ");
                Console.WriteLine("                           ");
                key = "0";
                Error();

            }
            cn.Close();


        }

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



        private void ScanContainerForm()
        {
        Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 13);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("               ");
            Console.SetCursorPosition(16, 13);
            ConsoleKeyInfo cki; 
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    key = "0";
                    Handler(key);
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
                        Handler(key);
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
            Console.WriteLine(" Press Esc ... ");
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape)
                {
                    key = "0";
                    Handler(key);
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


            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 13);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Transmiting Data....           ");
            SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select a.STORERKEY,a.ORDERKEY,a.EXTERNORDERKEY,case when isnull(a.TrailerNumber,'')='' then null else a.TrailerNumber end TrailerNumber3,case when isnull(c.BC33,'')='' then 'blm ada doc' else c.bc33 end BC33,  " +
                " case when isnull(c.BC33,'')='' then 'belum bisa di muat' else c.bc33 end Status,b.ID as PalletID,StatusSO,b.SKU,b.JumlahCarton from Orders a  inner join (select orderkey,id,SKU,count(distinct(LOT))as JumlahCarton from PICKDETAIL group by orderkey,id,SKU ) b on a.orderkey = b.ORDERKEY  " +
                " inner join (select orderkey,sku,susr3 as BC33 ,b.description as StatusSO from orderdetail a inner join orderstatussetup b on a.status = b.code group by b.description,orderkey,sku,susr3) c on a.orderkey = c.orderkey and b.sku = c.sku" +
                "  " +
                " where  b.ID = '" + PalletID + "' and a.status>'17' and a.orderkey='"+Orderkey+"' --and a.status <'95' ", cn);

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
                        StatusPallet = reader.GetString(7);
                        PalletID = reader.GetString(6);
                    }
                    key = "3";
                    Handler(key);
                    cn.Close();
                }
                catch (Exception ex)
                {

                    Error();
                }
            }
            else
            {
                Console.SetCursorPosition(0, 11);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                      ");
                Console.WriteLine("          Pallet Not Found            ");
                Console.WriteLine("                                      ");
                Console.WriteLine("                                      ");
                key = "0";
                Error();
                Handler(key);

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
                    Handler(key);
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
                    Handler(key);

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
                Handler(key);

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

                Console.SetCursorPosition(0, 14);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                  ");
                Console.WriteLine("                                  ");
                Console.WriteLine("              SALAH               ");
                Console.WriteLine("                                  ");
                Console.WriteLine("                                  ");

                key = "0";
                Error();
                Handler(key);


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
            cmd.Parameters.Add(new SqlParameter("QRcontent", QRLabel01));
            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                cn.Close();
                Console.SetCursorPosition(0, 15);
                Console.WriteLine("                                 ");
                Console.WriteLine("       Container Sesuai          ");
                Console.WriteLine("                                 ");

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press any key.. ");
                Console.ReadKey();
                key = "0";
                Handler(key);
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

        private void PalletValidateFailure(string PalletQR)
        {

            FormHeader();
            Console.SetCursorPosition(16, 2);
            Console.Write(PalletID);
            Console.SetCursorPosition(16, 6);
            Console.Write(Orderkey);
            Console.SetCursorPosition(0, 11);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("                                    ");
            Console.WriteLine("         Label Tidak Sesuai         ");
            Console.WriteLine("                                    ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Serial WMS  : " + r_Pallet);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Serial Label: " + PalletQR);
            key = "0";
            Error();

           
        }
    }
}
