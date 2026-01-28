using System;
using System.Collections.Generic;
using System.IO;
using TicketBai;

static class TicketFactory
{
    public static List<Ticket> SortuTicketak(List<string> lerroak, string baskula, string txtPath)
    {
        List<Ticket> ticketak = new List<Ticket>();

        
        string fileName = Path.GetFileNameWithoutExtension(txtPath);
        DateTime fechaTicket = DateTime.Now; 
        string[] partes = fileName.Split('_');
        if (partes.Length >= 2)
        {
            if (!DateTime.TryParse(partes[1], out fechaTicket))
            {
                Console.WriteLine("Fecha inválida en nombre del archivo: " + partes[1]);
                fechaTicket = DateTime.Now;
            }
        }

        foreach (string lerroa in lerroak)
        {
            if (string.IsNullOrWhiteSpace(lerroa))
                continue;

            string[] zatitu = lerroa.Split('$'); 

            if (zatitu.Length < 5)
            {
                Console.WriteLine("Formato incorrecto: " + lerroa);
                continue;
            }

            
            string produktuaIzena = zatitu[0];
            string saltzaileaId = zatitu[1].Trim();

            string saltzaileaIzena;
            switch (saltzaileaId)
            {
                case "001": saltzaileaIzena = "Lander"; break;
                case "002": saltzaileaIzena = "Ander"; break;
                case "003": saltzaileaIzena = "Mateo"; break;
                case "004": saltzaileaIzena = "Unai"; break;
                case "005": saltzaileaIzena = "Eber"; break;
                case "006": saltzaileaIzena = "Igor"; break;
                case "007": saltzaileaIzena = "Mario"; break;
                default: saltzaileaIzena = "Autosalmenta"; break;
            }
            if (!int.TryParse(saltzaileaId, out int idZenbakia))
                idZenbakia = 0;

            
            Saltzailea s = new Saltzailea(idZenbakia, saltzaileaIzena);

            if (!int.TryParse(zatitu[2], out int PrezioaKg))
                PrezioaKg = 0;
            if (!decimal.TryParse(zatitu[3], out decimal Pisua))
                Pisua = 0;
            if (!decimal.TryParse(zatitu[4], out decimal Prezioa))
                Prezioa = 0;

           

           
            Ticket t = new Ticket(produktuaIzena, fechaTicket, s);
            t.Baskula = baskula;

            
            t.GehituProduktua(produktuaIzena,PrezioaKg, Prezioa, Pisua);

            ticketak.Add(t);
            Console.WriteLine($"Ticket creado: {t.Id} - {saltzaileaIzena} - {fechaTicket.ToShortDateString()}");
        }

        Console.WriteLine("Tickets totales creados: " + ticketak.Count);
        return ticketak;
    }
}
