using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.ViewModels
{
    public class OrganizacijaDodajVM
    {
        public string PredavacId { get; set; }
        public List<SelectListItem> Predavaci { get; set; }
        public Kurs Kurs { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public bool Uredi { get; set; }
    }
}
