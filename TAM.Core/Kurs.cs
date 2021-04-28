using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TAM.Core
{
    public class Kurs
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(30, ErrorMessage = "Naziv kursa mora sadržavati između 3 i 30 slova.", MinimumLength = 3)]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [Range(0, float.MaxValue, ErrorMessage = "Cijena ne smije biti manja od 0.")]
        public float Cijena { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [Range(10, Int32.MaxValue, ErrorMessage = "Broj casova ne smije biti manji od 10.")]
        public int BrojCasova { get; set; }
        [ForeignKey(nameof(KategorijaKursaId))]
        public virtual KategorijaKursa KategorijaKursa { get; set; }
        public int KategorijaKursaId { get; set; }
        [Required]
        [Range(5, Int32.MaxValue, ErrorMessage = "Kapacitet kursa ne smije biti manji od 5")]
        public int Kapacitet { get; set; }
        [DefaultValue(false)]
        public bool PotrebnoOrganizovati { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "Kratki opis kursa mora sadržavati između 10 i 50 slova.", MinimumLength = 10)]
        public string Opis { get; set; }
    }
}
