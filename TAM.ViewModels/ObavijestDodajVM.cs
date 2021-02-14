using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.ViewModels
{
    public class ObavijestDodajVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(30, ErrorMessage = "Naslov obavijesti mora sadržavati između 3 i 30 slova.", MinimumLength = 3)]
        public string Naslov { get; set; }
        public DateTime DatumIVrijeme { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string Sadrzaj { get; set; }
        public List<SelectListItem> KategorijaObavijesti { get; set; }
        public int KategorijaObavijestiId { get; set; }
        public bool Dodaj { get; set; }
    }
}
