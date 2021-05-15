using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class OdrzanaNastavaDodajVM
    {
        public int OrganizacijaKursaId { get; set; }
        public List<SelectListItem> Prostorije { get; set; }
        public int ProstorijaId { get; set; }
    }
}
