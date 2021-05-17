using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TAM.Core
{
    public class OdrzanaNastava
    {
        public int Id { get; set; }
        public DateTime DatumIVrijemeOdrzavanja { get; set; }
        [ForeignKey(nameof(ProstorijaId))]
        public virtual Prostorija Prostorija { get; set; }
        public int ProstorijaId { get; set; }
        [ForeignKey(nameof(OrganizacijaKursaId))]
        public virtual OrganizacijaKursa OrganizacijaKursa { get; set; }
        public int OrganizacijaKursaId { get; set; }
        [ForeignKey(nameof(PredavacId))]
        public virtual Predavac Predavac { get; set; }
        public string PredavacId { get; set; }
        public bool Zakljucen { get; set; }

    }
}
