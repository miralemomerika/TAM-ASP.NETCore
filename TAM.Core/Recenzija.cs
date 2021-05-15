using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.Core
{
    public class Recenzija
    {
        public int Id { get; set; }
        [Range(5, 10, ErrorMessage = "Ocjena mora biti između 5 i 10")]
        public int OcjenaKursa { get; set; }
        [Range(5, 10, ErrorMessage = "Ocjena mora biti između 5 i 10")]
        public int OcjenaPredavaca { get; set; }
        public string Komentar { get; set; }
        public int OrganizacijaKursaId { get; set; }
        public virtual OrganizacijaKursa OrganizacijaKursa { get; set; }
    }
}
