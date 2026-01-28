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
            Directory.CreateDirectory(karpeta);
            string fitxategiIzena = "Ticket_Ezezaguna";
            if (t.Produktuak.Count > 0)
            {
                // Lehenengo produktuaren izena hartzen dugu fitxategiaren izenerako
                fitxategiIzena = t.Produktuak[0].Izena;
            }

            // Hemen izena aldatzen dugu
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

                    writer.WriteElementString("Eguna", t.Eguna.ToString()?? "");
                    writer.WriteElementString("Ordua", t.Ordua.ToString() ?? "");
                    writer.WriteElementString("Saltzailea", t.Saltzailea?.Izena ?? "");

                    foreach (var p in t.Produktuak)
                    {
                        writer.WriteStartElement("Produktua");
                        writer.WriteElementString("Izena", p.Izena ?? "");
                        writer.WriteElementString("PrezioKg", p.PrezioaKg.ToString());
                        writer.WriteElementString("Pisua", p.Pisua.ToString());
                        writer.WriteElementString("Prezioa", p.Prezioa.ToString());
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
            bool esValido = true;

            try
            {
                // Crear el conjunto de esquemas y agregar el XSD
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("", xsdPath); // "" = namespace vacío

                // Configurar el lector para validar
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas = schemas;
                settings.ValidationType = ValidationType.Schema;

                // Capturar errores de validación
                settings.ValidationEventHandler += (sender, e) =>
                {
                    Console.WriteLine($"Error de validación: {e.Message}");
                    esValido = false;
                };

                // Leer el XML con validación
                using (XmlReader reader = XmlReader.Create(xmlPath, settings))
                {
                    while (reader.Read()) { } // Recorrer todo el XML
                }

                if (esValido)
                    Console.WriteLine("XML válido ✅");
                else
                    Console.WriteLine("XML inválido ❌");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción durante validación: {ex.Message}");
                esValido = false;
            }

            return esValido;
        }
    }
}
