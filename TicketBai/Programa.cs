using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBai
{
    internal class Programa
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

                string karpetaNagusia = @"\\UnaxZulaika\TicketBAI\Baskulak";

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
                string[] txtFitxategiak = Directory.GetFiles(Path.Combine(baskulaPath, "Tiketak"), "*.txt");


                foreach (string txtPath in txtFitxategiak)
                    {
                        List<string> lerroak = File.ReadAllLines(txtPath).ToList();

                        // 3️⃣ Ticket objektuak sortu (baskula pasatuta)
                        List<Ticket> ticketak = TicketFactory.SortuTicketak(lerroak,baskulaIzena,txtPath);

                        foreach (Ticket t in ticketak)
                        {


                            // 5️⃣ XML sortu
                            string xmlPath = XmlGenerator.Sortu(t);



                        // 6️⃣ XML balidatu

                      

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
            string connString = "server=localhost;user=root;password=root;database=TicketBaiDB;";

            Console.WriteLine("Aukeratu zer erakutsi:");
            Console.WriteLine("1 - Saltzaile bakoitzaren ticket bateko salmenta handiena");
            Console.WriteLine("2 - Saltzaile bakoitzak zenbat ticket saldu dituen");
            Console.WriteLine("3 - Egun bakoitzean sortutako ticket kopurua");
            Console.WriteLine("4 - Produktu bakoitza zenbat ticket desberdinetan agertzen den");
            Console.Write("Aukera: ");

            string aukera = Console.ReadLine();
            Console.WriteLine();

            switch (aukera)
            {
                case "1":
                    Console.WriteLine("1. Saltzaile bakoitzaren ticket bateko salmenta handiena");
                    Estatistika.SalmentaHandiena(connString);
                    break;

                case "2":
                    Console.WriteLine("2. Saltzaile bakoitzak zenbat ticket saldu dituen");
                    Estatistika.BakoitzakZenbat(connString);
                    break;

                case "3":
                    Console.WriteLine("3. Egun bakoitzean sortutako ticket kopurua");
                    Estatistika.EguneanZenbat(connString);
                    break;

                case "4":
                    Console.WriteLine("4. Produktu bakoitza zenbat ticket desberdinetan agertzen den");
                    Estatistika.ProduktuaTicketko(connString);
                    break;

                default:
                    Console.WriteLine("Aukera okerra.");
                    break;
            }

            Console.WriteLine();
        }

    }
}



