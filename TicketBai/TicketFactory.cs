using System;
using System.Collections.Generic;
using System.IO;
using TicketBai;

static class TicketFactory
{
    public static List<Ticket> SortuTicketak(List<string> lerroak)
    {
        List<Ticket> ticketak = new List<Ticket>();

        foreach (string lerroa in lerroak)
        {
            if (string.IsNullOrWhiteSpace(lerroa))
                continue; // hutsik dagoen lerroa saltatu

            // Adibidez, lerro formatua:
            // ID;Data;SaltzaileIzena;Produktu1:Prezioa1,Produkt2:Prezioa2
            // T001;2026-01-22;Ane;Esnea:1.5,Ogia:1.2
            string[] zatitu = lerroa.Split(';');
            if (zatitu.Length < 4)
                continue; // formatu okerra

            string id = zatitu[0];
            DateTime data = DateTime.Parse(zatitu[1]);
            string saltzaileaIzena = zatitu[2];
            Saltzailea s = new Saltzailea(0, saltzaileaIzena);

            Ticket t = new Ticket(id, data, s);

            // Produktuak gehitu
            string produktuakStr = zatitu[3]; // "Esnea:1.5,Ogia:1.2"
            string[] produktuakArray = produktuakStr.Split(',');

            foreach (string pStr in produktuakArray)
            {
                if (string.IsNullOrWhiteSpace(pStr))
                    continue;

                string[] pZatiak = pStr.Split(':');
                if (pZatiak.Length != 2)
                    continue;

                string izena = pZatiak[0];
                if (!decimal.TryParse(pZatiak[1], out decimal prezioa))
                    prezioa = 0;

                t.GehituProduktua(izena, prezioa);
            }

            ticketak.Add(t);
        }

        return ticketak;
    }
}
    
