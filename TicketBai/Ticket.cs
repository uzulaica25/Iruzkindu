using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBai
{
    public class Ticket
    {
        private string id, izena;
        private DateTime data;
        private Saltzailea saltzailea;
        public List<Produktua> Produktuak { get; set; }

        public string Id { get => id; set => id = value; }
        public DateTime Data { get => data; set => data = value; }
        public Saltzailea Saltzailea { get => saltzailea; set => saltzailea = value; }

        public Ticket()
        {
            Produktuak = new List<Produktua>();
        }

    }
}
