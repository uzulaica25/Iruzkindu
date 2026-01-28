using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using TicketBai;
namespace TicketBai
{
    static class XmlGenerator
    {
        public static string Sortu(Ticket t)
        {
            string karpeta = @"C:\TicketBAI\XML";
            Directory.CreateDirectory(karpeta); //
            string xmlPath = Path.Combine(karpeta, $"{t.Id}.xml");



            try
            {
                using (StreamWriter sw = new StreamWriter(xmlPath))
                {
                    sw.WriteLine($"<Ticket id='{t.Id}'>");
                    sw.WriteLine($"  <Data>{t.Data}</Data>");
                    sw.WriteLine($"  <Saltzailea>{t.Saltzailea.Izena}</Saltzailea>");
                  
                    if (t.Produktuak.Count > 0)
                    {
                        var p = t.Produktuak[0];
                        sw.WriteLine("  <Produktua>");
                        sw.WriteLine($"    <Izena>{p.Izena}</Izena>");
                        sw.WriteLine($"    <PrezioKg>{p.PrezioaKg}</PrezioKg>");
                        sw.WriteLine($"    <Pisua>{p.Pisua}</Pisua>");
                        sw.WriteLine($"    <Prezioa>{p.Prezioa}</Prezioa>");
                        sw.WriteLine("  </Produktua>");
                    }
                    sw.WriteLine("</Ticket>");
                }

                Console.WriteLine($"XML sortua: {xmlPath}");
                return xmlPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea XML sortzean: {ex.Message}");
                return null;
            }
        }
    }
    static class XmlValidator
    {
        public static bool Balidatu(string xmlPath, string xsdPath)
        {
            bool baliogarria = true;

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", xsdPath); 

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas = schemas;
            settings.ValidationType = ValidationType.Schema;

            settings.ValidationEventHandler += (sender, e) =>
            {
                baliogarria = false;
                Console.WriteLine($"Errorea XSD egiaztapenean: {e.Message}");
            };

            try
            {
                using (XmlReader reader = XmlReader.Create(xmlPath, settings))
                {
                    while (reader.Read()) { } 
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine($"XML akatsa: {ex.Message}");
                return false;
            }

            return baliogarria;
        }
    }
}
