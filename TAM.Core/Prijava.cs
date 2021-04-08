using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.Core
{
    public class Prijava
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int KursId { get; set; }
        public virtual Kurs Kurs { get; set; }
        public string PolaznikId { get; set; }
        public virtual Polaznik Polaznik { get; set; }
    }
}
