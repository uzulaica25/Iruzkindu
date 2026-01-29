using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using TicketBai;
namespace TicketBai
{
    static class XmlGenerator
    {


        public static string Sortu(Ticket t)
        {
            string karpeta = @"C:\TicketBAI\XML";
            Directory.CreateDirectory(karpeta);
            string fitxategiIzena = "Ticket_Ezezaguna";
            if (t.Produktuak.Count > 0)
            {

                fitxategiIzena = t.Produktuak[0].Izena;
            }


            string xmlPath = Path.Combine(karpeta, $"{fitxategiIzena}.xml");

            try
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineOnAttributes = false
                };

                using (XmlWriter writer = XmlWriter.Create(xmlPath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Ticket");
                    writer.WriteAttributeString("id", t.Id.ToString());

                    writer.WriteElementString("Eguna", t.Eguna.ToString() ?? "");
                    writer.WriteElementString("Ordua", t.Ordua.ToString() ?? "");
                    writer.WriteElementString("Saltzailea", t.Saltzailea?.Izena ?? "");

                    foreach (var p in t.Produktuak)
                    {
                        writer.WriteStartElement("Produktua");
                        writer.WriteElementString("Izena", p.Izena ?? "");
                        writer.WriteElementString("PrezioKg", p.PrezioaKg.ToString(CultureInfo.InvariantCulture)); // 2.5
                        writer.WriteElementString("Pisua", p.Pisua.ToString(CultureInfo.InvariantCulture));       // 1.2
                        writer.WriteElementString("Prezioa", p.Prezioa.ToString(CultureInfo.InvariantCulture));
                        writer.WriteEndElement(); // Produktua
                    }

                    writer.WriteEndElement(); // Ticket
                    writer.WriteEndDocument();
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
            bool baliozkoa = true;

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", xsdPath);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas = schemas;
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += (sender, e) =>
            {
                Console.WriteLine($"Errorea: {e.Message}");
                baliozkoa = false;
            };

            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                try
                {
                    while (reader.Read()) { }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    baliozkoa = false;
                }
            }

            if (baliozkoa)
                Console.WriteLine("XML baliozkoa da XSDaren arabera.");
            else
                Console.WriteLine("XML ez da baliozkoa.");

            return baliozkoa;
        }
    }
}



