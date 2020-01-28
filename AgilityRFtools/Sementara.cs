using System;
using System.Data.SqlClient;

namespace AgilityRFtools
{
    class Sementara
    {
        private static string PalletASAL;
        private static string Stiker;

        private static string JumlahPerBox;
        private static DateTime Editdate = DateTime.Now;
        public void Start()
        {

        ulang:
            Console.Clear();
            Console.WriteLine("Scan Pallet Asal  :");
            PalletASAL = Console.ReadLine();

            Console.WriteLine("Scan Stiker       :");
            Stiker = Console.ReadLine();
            SqlConnection cn = new SqlConnection(ConfigDB.DBlocal);
            cn.Close();

            SqlCommand cmd = new SqlCommand("insert into tbplbsami_fg_PalletCek " +
                "(PalletAsal,Stiker) " +
                "values (@PalletAwal,@Stiker)", cn);
            cmd.Parameters.Add(new SqlParameter("PalletAwal", PalletASAL));
            cmd.Parameters.Add(new SqlParameter("Stiker", Stiker));
            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                cn.Close();
                Console.WriteLine("Data berhasil di simpan");
                Console.ReadKey();
                goto ulang;


            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex);
                Console.ReadKey();
                cn.Close();
                goto ulang;

            }

            goto ulang;


        }
    }
}
