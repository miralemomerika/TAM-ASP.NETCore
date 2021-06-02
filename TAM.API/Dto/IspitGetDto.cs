using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.API.Dto
{
    public class IspitGetDto
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public string UrlDokumenta { get; set; }
        public DateTime VrijemePocetka { get; set; }
        public DateTime VrijemeZavrsetka { get; set; }
        public string OrganizacijaKursa { get; set; }
        public int OrganizacijaKursaId { get; set; }

    }
}
