using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class CekPallet
    {
        private static string PalletID;
        private static string CartonID;
        private static string SKU;
        private static string Invoice;
        private static DateTime Editdate = DateTime.Now;
        private static string strKey = "";
        private static int Scan;

        public void Start()

        {
            MainMenu.FormName = "Cek Pallet";
            InvoiceForm();
        }
        public void LOAD()
        {

            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();

            SqlCommand cmd = new SqlCommand("insert into tbPLBSAMI_FG_dispatchList (Invoice,PalletID,SKU,CartonID,QRContent,editdate,EditBy)" +
                " values (@Invoice,@PalletID,@SKU,@CartonID,@QRContent,getdate(),@EditBy)", cn);
            cmd.Parameters.Add(new SqlParameter("Invoice", Invoice));
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
                CartonForm();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex);
                Console.ReadKey();
                cn.Close();
                CartonForm();

            }


        }
        private void FormHeader()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Scan Docking");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("No. Document:");
            Console.WriteLine("Pallet/Loc      :");
            Console.WriteLine("Carton      :");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 8);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Jumlah : " + Scan);
        }
        private void backtomenu()
        {
            MainMenu L = new MainMenu();
            L.Menu_Home();
        }

        public void InvoiceForm()
        {
            FormHeader();
            Invoice = "";
            Console.SetCursorPosition(14, 2);
            Invoice = Console.ReadLine();
            Palletform();
        }
        private void SUMSCAN()
        {
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select cast(count(distinct(cartonID))as int)as JumlahScan from tbPLBSAMI_FG_DispatchList where PalletID=@palletID group by PalletID", cn);
            cmd.Parameters.AddWithValue("@PalletID", PalletID);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Scan = reader.GetInt32(0);
            }
            cn.Close();
        }
        public void Palletform()
        {
        Ulang:
            PalletID = "";
            FormHeader();
            Console.SetCursorPosition(14, 2);
            Console.WriteLine(Invoice);
            Console.SetCursorPosition(14, 3);
            ConsoleKeyInfo cki;
            strKey = "";

            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    InvoiceForm();
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
                        PalletID = strKey;
                        if (PalletID.Contains(","))
                        {
                            string[] b = PalletID.Split(new string[] { "," }, StringSplitOptions.None);
                            PalletID = b[0].ToString();
                            CartonForm();
                        }
                        else
                        {
                            CartonForm();
                        }


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
            SUMSCAN();
            FormHeader();
            CartonID = "";
            Console.SetCursorPosition(14, 2);
            Console.WriteLine(Invoice);
            Console.SetCursorPosition(14, 3);
            Console.WriteLine(PalletID);
            Console.SetCursorPosition(14, 4);
            ConsoleKeyInfo cki;
            strKey = "";

            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    Palletform();
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
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
        }

    }

}
