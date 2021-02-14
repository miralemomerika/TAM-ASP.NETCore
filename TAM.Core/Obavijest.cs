using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TAM.Core
{
    public class Obavijest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(30, ErrorMessage = "Naslov obavijesti mora sadržavati između 3 i 30 slova.", MinimumLength = 3)]
        public string Naslov { get; set; }       
        public DateTime DatumIVrijeme { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]        
        public string Sadrzaj { get; set; }
        [ForeignKey(nameof(KategorijaObavijestiId))]
        public virtual KategorijaObavijesti KategorijaObavijesti { get; set; }
        public int KategorijaObavijestiId { get; set; }
        [ForeignKey(nameof(KorisnickiRacunId))]
        public virtual KorisnickiRacun KorisnickiRacun { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string KorisnickiRacunId { get; set; }
    }
}
