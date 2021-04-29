using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.Core
{
    public class Uplata
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Iznos { get; set; }

        public int? PrijavaId { get; set; }
        public virtual Prijava Prijava { get; set; }
        public int? DogadjajId { get; set; }
        public virtual Dogadjaj Dogadjaj { get; set; }
    }
}
