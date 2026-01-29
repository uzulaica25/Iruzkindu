using System;
using System.Collections.Generic;
using System.IO;
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
                    WriteHeader("===== MENU NAGUSIA =====");

                    Console.WriteLine("1. Ticketak PROZESATU");
                    Console.WriteLine("2. Estatistika ERAKUTSI");
                    Console.WriteLine("0. Irten");
                    Console.WriteLine();
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
                            WriteInfo("Programa amaitzen...");
                            break;

                        default:
                            WriteError("Aukera okerra.");
                            break;
                    }

                } while (aukera != 0);
            }

            static void TicketakProzesatu()
            {
                WriteHeader("Ticketak prozesatzen...");

                string karpetaNagusia = @"\\UnaxZulaika\TicketBAI\Baskulak";

                if (!Directory.Exists(karpetaNagusia))
                {
                    WriteError("Sarrera karpeta ez da existitzen.");
                    return;
                }

                
                List<Ticket> todosTicketak = new List<Ticket>();

                
                string[] baskulak = Directory.GetDirectories(karpetaNagusia);


                foreach (string baskulaPath in baskulak)
                {
                    string baskulaIzena = Path.GetFileName(baskulaPath);
                    WriteStep($"Baskula: {baskulaIzena}");

                    string[] txtFitxategiak = Directory.GetFiles(Path.Combine(baskulaPath, "Tiketak"), "*.txt");

                    foreach (string txtPath in txtFitxategiak)
                    {
                        List<string> lerroak = File.ReadAllLines(txtPath).ToList();

                       
                        List<Ticket> ticketak = TicketFactory.SortuTicketak(lerroak, baskulaIzena, txtPath);

                        foreach (Ticket t in ticketak)
                        {
                            
                          

                            ExcelLogger.Erregistratu(t.Id, DateTime.Now);

                            
                            Database db = new Database();
                            db.Konektatu();
                            db.GordeTicket(t);
                            db.Itxi();

                           
                            todosTicketak.Add(t);
                        }

                        
                        WriteBlank();
                    }

                   
                    WriteBlank();
                }

                WriteStep($"Sortu diren tiket guztiak: {todosTicketak.Count}");
                WriteBlank();

                
                if (todosTicketak.Count > 0)
                {
                    string xmlPath = XmlGenerator.Sortu(todosTicketak);

                    bool ondo = XmlValidator.Balidatu(xmlPath);

                    if (ondo)
                    {
                        
                        EmailSender.Bidali(xmlPath);

                  
                        BackupManager.Egin(xmlPath);
                    }
                    else
                    {
                        WriteWarning("XML baliogabea, ez da bidaliko/backup egingo.");
                    }
                }

                WriteInfo("Prozesua amaituta.");
                Console.WriteLine();
            }

        static void EstatistikakErakutsi()
        {
            string connString = "server=localhost;user=root;password=root;database=TicketBaiDB;";

            WriteHeader("Aukeratu zer erakutsi:");
            Console.WriteLine("1 - Saltzaile bakoitzaren ticket bateko salmenta handiena");
            Console.WriteLine("2 - Saltzaile bakoitzak zenbat ticket saldu dituen");
            Console.WriteLine("3 - Egun bakoitzean sortutako ticket kopurua");
            Console.WriteLine("4 - Produktu bakoitza zenbat ticket desberdinetan agertzen den");
            Console.WriteLine();
            Console.Write("Aukera: ");

            string aukera = Console.ReadLine();
            Console.WriteLine();

            switch (aukera)
            {
                case "1":
                    WriteStep("1. Saltzaile bakoitzaren ticket bateko salmenta handiena");
                    Estatistika.SalmentaHandiena(connString);
                    break;

                case "2":
                    WriteStep("2. Saltzaile bakoitzak zenbat ticket saldu dituen");
                    Estatistika.BakoitzakZenbat(connString);
                    break;

                case "3":
                    WriteStep("3. Egun bakoitzean sortutako ticket kopurua");
                    Estatistika.EguneanZenbat(connString);
                    break;

                case "4":
                    WriteStep("4. Egunean batez besteko salmenta");
                    Estatistika.SalmentaMaiztasuna(connString);
                    break;

                default:
                    WriteError("Aukera okerra.");
                    break;
            }

            Console.WriteLine();
        }

        
        private static void WriteHeader(string text)
        {
            Console.WriteLine();
            Console.WriteLine("==================================");
            Console.WriteLine(text);
            Console.WriteLine("==================================");
        }

        private static void WriteStep(string text)
        {
            Console.WriteLine($"• {text}");
        }

       

        private static void WriteWarning(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"! {text}");
            Console.ForegroundColor = prev;
        }

        private static void WriteError(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✖ {text}");
            Console.ForegroundColor = prev;
        }

        private static void WriteInfo(string text)
        {
            Console.WriteLine(text);
        }

        private static void WriteBlank()
        {
            Console.WriteLine();
        }
    }
}



