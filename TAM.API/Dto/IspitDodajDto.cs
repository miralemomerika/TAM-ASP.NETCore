using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.API.Dto
{
    public class IspitDodajDto
    {
        public int Id { get; set; }
        [Required]
        public string Naslov { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        public string UrlDokumenta { get; set; }
        [Required]
        public DateTime VrijemePocetka { get; set; }
        [Required]
        public DateTime VrijemeZavrsetka { get; set; }
        [Required]
        public int OrganizacijaKursaId { get; set; }

    }
}
