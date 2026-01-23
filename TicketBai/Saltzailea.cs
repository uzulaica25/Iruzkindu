using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBai
{
    public class Saltzailea
    {
        public int Id { get; set; }
        public string Izena { get; set; }

        public Saltzailea(int id, string izena)
        {
            Id = id;
            Izena = izena;
        }
    }
}
