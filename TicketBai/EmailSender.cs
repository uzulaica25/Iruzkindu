using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml;
using System.Xml.Schema;
namespace TicketBai
{
    static class EmailSender
    {
        
        public static void Bidali(string xmlPath)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlPath) || !File.Exists(xmlPath))
                    throw new Exception("Ez da aurkitu bidaltzeko XML fitxategia: " + xmlPath);

                string jaso = "tickfy28@gmail.com";

                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                if (outlookType == null)
                    throw new Exception("Outlook ez dago instalatuta ordenagailu honetan.");

                dynamic outlookApp = Activator.CreateInstance(outlookType);
                dynamic mail = outlookApp.CreateItem(0);

                mail.To = jaso;
                mail.Subject = "Ticketak XML formatuan";
                mail.Body = "Kaixo, hemen dago XML-a gure baskulek egindako ticketekin.";
                mail.Attachments.Add(xmlPath);

                mail.Send();

                Console.WriteLine($"Email bidalia: {xmlPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea email bidaltzean: {ex.Message}");
            }
        }
    }






    static class ExcelLogger
    {
        private static string fitx = "bidalketak.csv";

        public static void Erregistratu(string ticketId, DateTime data)
        {
            try
            {
                bool sortuBerria = !File.Exists(fitx);

                using (StreamWriter sw = new StreamWriter(fitx, true)) 
                {
                    
                    if (sortuBerria)
                    {
                        sw.WriteLine("TicketID,Data");
                    }

                  
                    sw.WriteLine($"{ticketId},{data}");
                }

                Console.WriteLine($"Bidalketa erregistratua Excel/CSV-an");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea Excel log-ean: {ex.Message}");
            }
        }
    }
}


