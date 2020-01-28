using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class CartonReader
    {
        private static string SKU;
        public void Start()
        {


            Console.Clear();
            MainMenu.FormName = "Cek Carton";
            Console.WriteLine("Scan Carton :");
            Parser.QRinput = Console.ReadLine();
            Console.WriteLine("-------------------");
            Parser l = new Parser();
            l.Load();
        }
        public void ResultParsing()
        {
            Console.WriteLine(Parser.SKU);
            Console.ReadKey();
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("insert into tbplbsami_fg_recordManual " +
                "(SKU,CartonID,QR) " +
                "values (@SKU,@CartonID,@QR)", cn);
            cmd.Parameters.Add(new SqlParameter("SKU", Parser.SKU));
            cmd.Parameters.Add(new SqlParameter("CartonID", Parser.CartonNo));
            cmd.Parameters.Add(new SqlParameter("QR", Parser.QRinput));

            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                cn.Close();
                Console.SetCursorPosition(0, 11);
                Console.WriteLine("Data berhasil di simpan");
                Console.ReadKey();
                Start();

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex);
                Console.ReadKey();
                cn.Close();
            }


        }
    }
}
