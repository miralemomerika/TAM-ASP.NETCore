using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.ViewModels
{
    public class UplataDodajVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Range(1, float.MaxValue, ErrorMessage = "Iznos ne smije biti manji ili jednak 0.")]
        public int Iznos { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        public string SvrhaId { get; set; }
        public List<SelectListItem> Svrhe { get; set; }
        public int? KursId { get; set; }
        public string? PolaznikId { get; set; }
        public int? DogadjajId { get; set; }
        public string? OrganizatorId { get; set; }
        public bool NovaUplata { get; set; }
        public bool UplataKursa { get; set; }
    }
}
