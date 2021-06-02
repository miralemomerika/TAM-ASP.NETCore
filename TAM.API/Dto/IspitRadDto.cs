using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.API.Dto
{
    public class IspitRadDto
    {
        public int RadId { get; set; }
        public DateTime VrijemePostavljanjaRada { get; set; }
        public string UrlRada { get; set; }
        public int IspitId { get; set; }
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public string UrlIspita { get; set; }
        public DateTime VrijemePocetka { get; set; }
        public DateTime VrijemeZavrsetka { get; set; }
        public string OrganizacijaKursa { get; set; }
        public int OrganizacijaKursaId { get; set; }
    }
}
