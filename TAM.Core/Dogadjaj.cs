using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TAM.Core
{
    public class Dogadjaj
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(30, ErrorMessage = "Naziv događaja mora sadržavati između 3 i 30 slova.", MinimumLength = 3)]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(250, ErrorMessage = "Opis događaja mora sadržavati između 10 i 250 slova.", MinimumLength = 10)]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]       
        public DateTime DatumIVrijemeOdrzavanja { get; set; }
        public bool Odobren { get; set; }
        [ForeignKey(nameof(TipDogadjajaId))]
        public virtual TipDogadjaja TipDogadjaja { get; set; }
        public int TipDogadjajaId { get; set; }
        [ForeignKey(nameof(OrganizatorId))]
        public virtual Organizator Organizator { get; set; }
        public string OrganizatorId { get; set; }
    }
}
