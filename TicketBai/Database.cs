using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using System.IO;

namespace TicketBai
{
    class Database
    {
        private MySqlConnection conn;
        private bool konektatuta = false;

        public void Konektatu()
        {
            string connString = "server=localhost;user=root;password=root;database=TicketBaiDB;";
            conn = new MySqlConnection(connString);

            try
            {
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

        public void GordeTicket(Ticket t)
        {
            if (!konektatuta)
            {
                Console.WriteLine("Errorea: DB ez dago konektatuta.");
                return;
            }

            try
            {
                
                string sql = @"INSERT IGNORE INTO Saltzailea
                (idSaltzailea, SaltzaileaIzena)
                    VALUES (@id, @izena)
                 ON DUPLICATE KEY UPDATE SaltzaileaIzena = @izena";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", t.Saltzailea.Id);
                    cmd.Parameters.AddWithValue("@izena", t.Saltzailea.Izena);
                    cmd.ExecuteNonQuery();
                }

                string sql2 = @"INSERT INTO Produktua
                (ProduktuaIzena, ProduktuaPrezioa, ProduktuaPisua,PrezioaKg)
                VALUES (@izena, @prezioa,@pisua,@prezioaKg)";

                foreach (var p in t.Produktuak)
                {
                    using (MySqlCommand cmd2 = new MySqlCommand(sql2, conn))
                    {
                        cmd2.Parameters.AddWithValue("@izena", p.Izena);
                        cmd2.Parameters.AddWithValue("@prezioa", p.Prezioa);
                        cmd2.Parameters.AddWithValue("@pisua", p.Pisua);
                        cmd2.Parameters.AddWithValue("@prezioaKg", p.PrezioaKg);
                        cmd2.ExecuteNonQuery();
                    }
                }

               
                string sql3 = @"INSERT INTO Ticket
                (TicketOrdua, TicketEguna, PrezioOsoa, idSaltzailea)
                VALUES (@ordua, @eguna, @prezioOsoa, @idSaltzailea)";

                using (MySqlCommand cmd3 = new MySqlCommand(sql3, conn))
                {
                    cmd3.Parameters.AddWithValue("@ordua", t.Ordua);
                    cmd3.Parameters.AddWithValue("@eguna", t.Eguna);
                    cmd3.Parameters.AddWithValue("@prezioOsoa", t.PrezioOsoa);
                    cmd3.Parameters.AddWithValue("@idSaltzailea", t.Saltzailea.Id);

                    cmd3.ExecuteNonQuery();

                    
                    long insertedId = cmd3.LastInsertedId;
                    if (insertedId > 0)
                        t.Id = insertedId.ToString();
                }

                Console.WriteLine($"Ticket DB-n gorde da: {t.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea ticket gordetzean: {ex.Message}");
            }
        }

        public void Itxi()
        {
            if (konektatuta)
            {
                conn.Close();
                konektatuta = false;
                Console.WriteLine("DB itxita.");
            }
        }
    }

    static class BackupManager

    {
        private static string karpetaNagusia = @"C:\TicketBAI\Baskulak";
        private static string xmlKarpeta = @"C:\TicketBAI\XML";
        private static string backupKarpeta = @"C:\TicketBAI\BACKUP";

        public static void Egin(string fitxategia)
        {
            try
            {
                if (string.IsNullOrEmpty(fitxategia) || !File.Exists(fitxategia))
                {
                    Console.WriteLine($"⚠ Ez da aurkitu backup egiteko fitxategia: {fitxategia}");
                    return;
                }

                if (!Directory.Exists(backupKarpeta))
                {
                    Directory.CreateDirectory(backupKarpeta);
                }

                string fitxIzen = Path.GetFileName(fitxategia);
                string helmuga = Path.Combine(backupKarpeta, fitxIzen);

                File.Copy(fitxategia, helmuga, true); // true = gainidatzi
                Console.WriteLine($"✅ Backup eginda: {fitxIzen}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Errorea backup egitean: {ex.Message}");
            }
        }
    }
}

