﻿using System;
using System.Collections.Generic;
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
    }
}
