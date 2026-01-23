using System.Reflection.Emit;
using System.IO;
using System;

namespace TicketBai
{
    internal class Program
    {
        static void Main()
        {
            int aukera;

            do
            {
                Console.WriteLine("===== MENU NAGUSIA =====");
                Console.WriteLine("1. Ticketak PROZESATU");
                Console.WriteLine("2. Estatistika ERAKUTSI");
                Console.WriteLine("0. Irten");
                Console.Write("Aukeratu aukera bat: ");

                try
                {
                    aukera = int.Parse(Console.ReadLine());
                }
                catch
                {
                    aukera = -1;
                }

                Console.WriteLine();

                switch (aukera)
                {
                    case 1:
                        TicketakProzesatu();
                        break;

                    case 2:
                        EstatistikakErakutsi();
                        break;

                    case 0:
                        Console.WriteLine("Programa amaitzen...");
                        break;

                    default:
                        Console.WriteLine("Aukera okerra.");
                        break;
                }

            } while (aukera != 0);
        }
        static void TicketakProzesatu()
        {
            Console.WriteLine("Ticketak prozesatzen...");

            string karpetaNagusia = "Baskulak";

            if (!Directory.Exists(karpetaNagusia))
            {
                Console.WriteLine("Sarrera karpeta ez da existitzen.");
                return;
            }

            // 1️⃣ Baskula-karpetak
            string[] baskulak = Directory.GetDirectories(karpetaNagusia);

            foreach (string baskulaPath in baskulak)
            {
                string baskulaIzena = Path.GetFileName(baskulaPath);
                Console.WriteLine($"→ Baskula: {baskulaIzena}");

                // 2️⃣ TXT fitxategiak baskula bakoitzean
                string[] txtFitxategiak = Directory.GetFiles(baskulaPath, "*.txt");

                foreach (string txtPath in txtFitxategiak)
                {
                    List<string> lerroak = File.ReadAllLines(txtPath).ToList();

                    // 3️⃣ Ticket objektuak sortu (baskula pasatuta)
                    List<Ticket> ticketak = TicketFactory.SortuTicketak(lerroak, baskulaIzena);

                    foreach (Ticket t in ticketak)
                    {
                        // 4️⃣ Saltzailea aldatu beharrezkoa bada
                        if (t.Saltzailea.Izena == "EZEZAGUNA")
                        {
                            t.Saltzailea = new Saltzailea(0, "SISTEMA");
                        }

                        // 5️⃣ XML sortu
                        string xmlPath = XmlGenerator.Sortu(t);

                        // 6️⃣ XML balidatu
                        if (!XmlValidator.Balidatu(xmlPath))
                        {
                            Console.WriteLine($"XML baliogabea: {t.Id}");
                            continue;
                        }

                        // 7️⃣ Email bidali
                        EmailSender.Bidali(xmlPath);

                        // 8️⃣ Excel log
                        ExcelLogger.Erregistratu(t.Id, DateTime.Now);

                        // 9️⃣ DB-an gorde
                        Database db = new Database();
                        db.Konektatu();
                        db.GordeTicket(t);
                        db.Itxi();

                        // 🔟 Backup
                        BackupManager.Egin(xmlPath);
                    }
                }
            }

            Console.WriteLine("Prozesua amaituta.\n");
        }

        static void EstatistikakErakutsi()
        {
            string fitx = "bidalketak.csv";

            if (!System.IO.File.Exists(fitx))
            {
                Console.WriteLine("Ez dago bidalketa fitxategirik.");
                return;
            }

            string[] lerroak = File.ReadAllLines(fitx);

            if (lerroak.Length <= 1)
            {
                Console.WriteLine("Ez dago bidalketa daturik.");
                return;
            }

            // Zenbatu ticketak (burua kenduta)
            int totalTicketak = lerroak.Length - 1;
            Console.WriteLine($"Bidalketa totalak: {totalTicketak}");

            // Adibidez, azken 5 ticketak erakutsi
            Console.WriteLine("Azken 5 ticketak:");
            int start = Math.Max(1, lerroak.Length - 5); // lerroak[0] = burua
            for (int i = start; i < lerroak.Length; i++)
            {
                Console.WriteLine(lerroak[i]);
            }

            Console.WriteLine();
        }
    }
}

