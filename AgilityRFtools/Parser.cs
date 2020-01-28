using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class Parser
    {
        public static string QRinput;
        public static string SKU;
        public static string CartonNo;
        public static string CartonNo2;
        public static string DestinationCode;
        public static string Delimiter;
        public static Int32 AssySeq;
        public static Int32 CartonIDSeq;
        public static Int32 Qty;
        public static string Carline;
        public static string ParserNotes;
        public static string AssyCode;

        public void Load()
        {
            ParserRegulerProduct();

        }
        private void QrReader()
        {
            if (QRinput.Length > 2)
            {
                string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                string a;
                string c;
                string d;

                a = b[0].ToString();
                c = b[0].ToString();

            }
        }
        private void ParserRegulerProduct()
        {


            if (QRinput.Length > 15 && QRinput.Contains(","))
            {
                string a = QRinput.Substring(0, 2);
                if (a == "TF" || a == "SM" || a == "SB" || a == "PS" || a == "JF")
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = b[4].ToString();
                    string carton = b[5].ToString();
                    Int32 aaa = Convert.ToInt32(carton);
                    CartonNo = aaa.ToString();
                    AssyCode = b[3].ToString();
                    string x = b[6].ToString();
                    Qty = Convert.ToInt32(x);
                    Handler();
                }
                else if (a == "HN")
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = b[4].ToString();
                    string carton = b[5].ToString();
                    string cartonb = carton.Substring(carton.Length - 5);
                    Int32 aaa = Convert.ToInt32(cartonb);
                    CartonNo = aaa.ToString();
                    AssyCode = b[3].ToString();
                    string x = b[6].ToString();
                    Qty = Convert.ToInt32(x);
                    Handler();
                }
                else if (QRinput.Contains("EMPTY"))
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = "EMPTY POLYTAINER";
                    string carton = b[2].ToString();
                    Int32 aaa = Convert.ToInt32(carton);
                    CartonNo = aaa.ToString();
                    Handler();
                }

                else if (a == "28")
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = b[0].ToString();
                    string carton = b[2].ToString();
                    AssyCode = b[3].ToString();
                    Int32 aaa = Convert.ToInt32(carton);
                    CartonNo = aaa.ToString();
                    string x = b[1].ToString();
                    Qty = Convert.ToInt32(x);

                    Handler();
                }
                else if (a == "32")
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = b[0].ToString();
                    string carton = b[2].ToString();
                    AssyCode = b[4].ToString();
                    Int32 aaa = Convert.ToInt32(carton);
                    CartonNo = aaa.ToString();
                    string x = b[1].ToString();
                    Qty = Convert.ToInt32(x);

                    Handler();
                }
                else if (a.Contains("S5"))
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = b[1].ToString();
                    string carton = b[0].ToString();
                    string cartonb = carton.Substring(carton.Length - 6);
                    Int32 aaa = Convert.ToInt32(cartonb);
                    CartonNo = aaa.ToString();
                    string x = b[2].ToString();
                    Qty = Convert.ToInt32(x);

                    AssyCode = carton.Substring(4, 4);
                    Handler();
                }


                else if (a.Contains("CD") || a.Contains("CG"))
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = b[1].ToString();
                    string carton = b[0].ToString();
                    string cartonb = carton.Substring(carton.Length - 6);
                    Int32 aaa = Convert.ToInt32(cartonb);
                    CartonNo = aaa.ToString();
                    string x = b[2].ToString();
                    Qty = Convert.ToInt32(x);

                    AssyCode = carton.Substring(0, 4);
                    Handler();
                }
                else if (a.Contains("G") || a.Contains("K") || a.Contains("D"))
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = b[0].ToString();
                    string carton = b[1].ToString();
                    string cartonb = carton.Substring(carton.Length - 5);
                    Int32 aaa = Convert.ToInt32(cartonb);
                    CartonNo = aaa.ToString();
                    AssyCode = b[3].ToString();
                    string x = b[2].ToString();
                    Qty = Convert.ToInt32(x);

                    Handler();
                }
                else if (a.Contains("S0"))
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = b[1].ToString();
                    string carton = b[0].ToString();
                    string cartonb = carton.Substring(carton.Length - 6);
                    Int32 aaa = Convert.ToInt32(cartonb);
                    CartonNo = aaa.ToString();
                    string x = b[2].ToString();
                    Qty = Convert.ToInt32(x);
                    AssyCode = carton.Substring(4, 4);
                    Handler();
                }
                else if (a.Contains("C") || a.Contains("B") || a.Contains("S"))
                {
                    string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                    SKU = b[1].ToString();
                    string carton = b[0].ToString();
                    string cartonb = carton.Substring(carton.Length - 5);
                    Int32 aaa = Convert.ToInt32(cartonb);
                    CartonNo = aaa.ToString();
                    string x = b[2].ToString();
                    Qty = Convert.ToInt32(x);

                    AssyCode = carton.Substring(0, 4);
                    Handler();
                }




                else
                {
                    try
                    {
                        string[] b = QRinput.Split(new string[] { "," }, StringSplitOptions.None);
                        SKU = b[0].ToString();
                        string carton = b[1].ToString();
                        string cartonb = carton.Substring(carton.Length - 5);
                        Int32 aaa = Convert.ToInt32(cartonb);
                        CartonNo = aaa.ToString();

                        string carton2 = b[2].ToString();
                        string cartonb2 = carton.Substring(carton.Length - 5);
                        Int32 aaa2 = Convert.ToInt32(cartonb);
                        CartonNo2 = aaa.ToString();
                        Handler();

                    }
                    catch (Exception ex)
                    {
                        SqlConnection cn = new SqlConnection(ConfigDB.conWMS);
                        SqlCommand cmd = new SqlCommand("Select top 1  sku,Lottable10 from receiptdetail " +
                            " where Notes=@Pallet ", cn);
                        cmd.Parameters.AddWithValue("@Pallet", QRinput);
                        cn.Open();
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            SqlDataReader reader = null;
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                SKU = reader.GetString(0);
                                CartonNo = reader.GetString(0);
                            }
                        }
                        else
                        {
                            if (MainMenu.FormName == "MappingCek")
                            {
                                Handler();
                            }
                            else if (MainMenu.FormName == "Outbound Checking")
                            {

                                Handler();
                            }
                            else
                            {
                            ulang:
                                Console.Clear();
                                Console.WriteLine("Salah ");
                                ConsoleKeyInfo cki = Console.ReadKey();
                                while (true)
                                {
                                    if (cki.Key == ConsoleKey.Enter)
                                    {
                                        UnloadForm f2 = new UnloadForm();
                                        UnloadForm.key = "2";
                                        f2.Handler();
                                    }
                                    else
                                    {
                                        goto ulang;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            // Error Handler
            else
            {
                if (MainMenu.FormName == "MappingCek")
                {
                    Handler();
                }

                else if (MainMenu.FormName == "Outbound Checking")
                {
                    ParserNotes = "1";
                    Handler();
                }
                else if
                (MainMenu.FormName == "Cek Carton")
                {
                    ParserNotes = "1";
                    Handler();
                }
                else if
               (MainMenu.FormName == "Dyna")
                {

                    Handler();
                }
                else if
                (MainMenu.FormName == "Label Check")
                {

                    Handler();
                }
                else
                {
                ulang:
                    ConsoleKeyInfo cki;
                    while (true)
                    {
                        cki = Console.ReadKey();
                        if (cki.Key == ConsoleKey.Enter)
                        {
                            UnloadForm f2 = new UnloadForm();
                            UnloadForm.key = "2";
                            f2.Handler();
                        }
                        else
                        {
                            goto ulang;
                        }
                    }
                }
            }

        }

        private void Handler()
        {
            if (MainMenu.FormName == "UnloadForm")
            {

                Console.SetCursorPosition(0, 7);
                Console.WriteLine("Transmiting Data");
                UnloadForm f = new UnloadForm();
                f.CekSKU();
            }
            if (MainMenu.FormName == "UnloadFormV1")
            {

                Console.SetCursorPosition(0, 9);
                Console.WriteLine("Transmiting Data.............");
                UnloadFormV1 f = new UnloadFormV1();
                f.ResultParsing();
            }
            else if (MainMenu.FormName == "Cek Pallet")
            {
                Console.SetCursorPosition(0, 5);
                Console.WriteLine("Transmiting Data");
                CekPallet l = new CekPallet();
                l.LOAD();
            }

            else if (MainMenu.FormName == "MappingCek")
            {
                Console.SetCursorPosition(0, 5);
                Console.WriteLine("Transmiting Data");
                PalletChceking l = new PalletChceking();
                l.ResultParsing();
            }
            else if (MainMenu.FormName == "Outbound")
            {
                Console.SetCursorPosition(0, 7);
                Console.WriteLine(Parser.QRinput);
                Console.WriteLine("Transmiting Data");
                OutbundChecking load = new OutbundChecking();
                load.ResultParsing();
            }
            else if (MainMenu.FormName == "Label Check")
            {
                Console.SetCursorPosition(0, 16);
                Console.WriteLine("Transmiting Data...");
                CekLabel l = new CekLabel();
                l.ResultParsing();

            }
            else if (MainMenu.FormName == "Cek Carton")
            {
                Console.SetCursorPosition(0, 7);
                Console.WriteLine("Transmiting Data");
                CartonReader l = new CartonReader();
                l.ResultParsing();

            }

            else if (MainMenu.FormName == "Dyna")
            {
                Console.SetCursorPosition(0, 7);
                Console.WriteLine("Transmiting Data");
                Dyna l = new Dyna();
                l.InsertSQL();

            }
        }
    }
}
