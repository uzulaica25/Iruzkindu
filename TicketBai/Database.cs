using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TicketBai
{
    class Database
    {

        private MySqlConnection conn;
        private bool konektatuta = false;

        // ===== DB KONEKTATU =====
        public void Konektatu()
        {
            string connString = "server=localhost;user=root;password=root;database=TicketBaiDB;";
            conn = new MySqlConnection(connString);

            try
            {
                // Hemen MySQL konektatzeko logika jarri daiteke
                // adibidez: MySqlConnection conn = new MySqlConnection(connString);
                // conn.Open();
                conn.Open();
                konektatuta = true;
                Console.WriteLine("DB konektatuta.");
            }
            catch (Exception ex)
            {
                konektatuta = false;
                Console.WriteLine($"Errorea DB konektatzean: {ex.Message}");
            }
        }

        // ===== TICKET GORDE =====
        public void GordeTicket(Ticket t)
        {
            if (!konektatuta)
            {
                Console.WriteLine("Errorea: DB ez dago konektatuta.");
                return;
            }

            try
            {
                string sql = @"INSERT INTO Saltzailea
                (idSaltzailea, SaltzaileaIzena)
                    VALUES (@id, @izena)";  
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", t.Saltzailea.Id);
                cmd.Parameters.AddWithValue("@izena", t.Saltzailea.Izena);

                string sql2 = @"INSERT INTO Produktua
                 (ProduktuaIzena, ProduktuaPrezioa, ProduktuaPisua)
                VALUES ( @izena, @prezioa, @pisua)";

                
                

                    cmd.Parameters.AddWithValue("@izena", p.Izena);
                    cmd.Parameters.AddWithValue("@prezioa", p.Prezioa);
                    cmd.Parameters.AddWithValue("@pisua", p.Pisua);

                    cmd.ExecuteNonQuery();
                
               


                

                Console.WriteLine($"Ticket DB-n gorde da: {t.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea ticket gordetzean: {ex.Message}");
            }
        }

        // ===== BIDALKETA ERREGISTRATU =====
        public void ErregistratuBidalketa(string ticketId)
        {
            if (!konektatuta)
            {
                Console.WriteLine("Errorea: DB ez dago konektatuta.");
                return;
            }

            try
            {
                // Adibidez: INSERT INTO bidalketak (TicketID, Data) VALUES (...)
                Console.WriteLine($"Bidalketa DB-n erregistratua: {ticketId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea bidalketa erregistratzean: {ex.Message}");
            }
        }

        // ===== DB ITXI =====
        public void Itxi()
        {
            if (konektatuta)
            {
                conn.Close(); //benetako DB konektatzeko
                konektatuta = false;
                Console.WriteLine("DB itxita.");
            }
        }
    }
    static class BackupManager
    {
        private static string backupKarpeta = "Backup";

        public static void Egin(string fitxategia)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fitxategia) || !File.Exists(fitxategia))
                {
                    Console.WriteLine($"Errorea: fitxategia ez da aurkitu: {fitxategia}");
                    return;
                }

                // Backup karpeta existitzen dela egiaztatu, ez bada sortu
                if (!Directory.Exists(backupKarpeta))
                {
                    Directory.CreateDirectory(backupKarpeta);
                }

                // Fitxategia kopiatu
                string fileName = Path.GetFileName(fitxategia);
                string destPath = Path.Combine(backupKarpeta, fileName);

                File.Copy(fitxategia, destPath, true); // replace=true

                Console.WriteLine($"Backup eginda: {destPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea backup egitean: {ex.Message}");
            }
        }
    }
}
