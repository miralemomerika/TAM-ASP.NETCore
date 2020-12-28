using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.Core
{
    public class Prostorija
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(30, ErrorMessage = "Naziv prostorije mora sadržavati između 3 i 30 slova.", MinimumLength = 3)]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Broj mjesta u prostoriji ne smije biti manji od 0.")]
        public int BrojMjesta { get; set; }
    }
}
