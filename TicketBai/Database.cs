using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mysqlx.Expect.Open.Types.Condition.Types;


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
                ( TicketOrdua, TicketEguna)
                VALUES (@ordua,@eguna)";
                using (MySqlCommand cmd3 = new MySqlCommand(sql3, conn))
                {
                    cmd3.Parameters.AddWithValue("@Ordua", t.Ordua);
                    cmd3.Parameters.AddWithValue("@Eguna", t.Eguna);

                    cmd3.ExecuteNonQuery();


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
                if (!Directory.Exists(xmlKarpeta))
                {
                    Console.WriteLine("❌ XML karpeta ez da existitzen");
                    return;
                }

                
                if (!Directory.Exists(backupKarpeta))
                {
                    Directory.CreateDirectory(backupKarpeta);
                }

                string[] baskulak = Directory.GetDirectories(karpetaNagusia);

                foreach (string baskulaPath in baskulak)
                {
                    string baskulaIzena = Path.GetFileName(baskulaPath);
                    Console.WriteLine($"→ Baskula: {baskulaIzena}");

                 
                    string backupBaskula = Path.Combine(backupKarpeta, baskulaIzena);
                    if (!Directory.Exists(backupBaskula))
                    {
                        Directory.CreateDirectory(backupBaskula);
                    }

                    // 3️⃣ XML fitxategi guztiak lortu
                    string[] xmlFitxategiak = Directory.GetFiles(xmlKarpeta, "*.xml");

                    if (xmlFitxategiak.Length == 0)
                    {
                        Console.WriteLine("⚠ Ez dago XML fitxategirik backup egiteko");
                        return;
                    }

                    // 4️⃣ XML bakoitza BACKUP karpetara kopiatu
                    foreach (string xmlPath in xmlFitxategiak)
                    {
                        string fitxIzen = Path.GetFileName(xmlPath);
                        string helmuga = Path.Combine(backupKarpeta, fitxIzen);

                        File.Copy(xmlPath, helmuga, true); // true = gainidatzi
                        Console.WriteLine($"✅ Backup eginda: {fitxIzen}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Errorea backup egitean: {ex.Message}");
            }
        }
    }
}
    

