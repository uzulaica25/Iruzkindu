using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBai
{
    /// <summary>
    /// Saltzailea klasea, saltzailearen ID-a eta izena gordetzeko erabiltzen da. Konstruktorea ID-a eta izena jasotzen du, eta propietateak ID eta izena gordetzeko erabiltzen dira. Saltzailea taulan saltzaileak gordetzeko erabiltzen da, eta Ticket objektuak saltzailearekin lotzeko erabiltzen da.
    /// </summary>
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
