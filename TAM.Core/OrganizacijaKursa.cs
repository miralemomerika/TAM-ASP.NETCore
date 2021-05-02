using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.Core
{
    public class OrganizacijaKursa
    {
        public int Id { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public bool AktivnaRecenzija { get; set; }
        public string PredavacId { get; set; }
        public virtual Predavac Predavac { get; set; }
        public int KursId { get; set; }
        public virtual Kurs Kurs { get; set; }
    }
}
