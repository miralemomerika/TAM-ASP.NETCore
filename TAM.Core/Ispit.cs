using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.Core
{
    public class Ispit
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public DateTime VrijemePocetka { get; set; }
        public DateTime VrijemeZavrsetka { get; set; }
        public string UrlDokumenta { get; set; }
        public int OrganizacijaKursaId { get; set; }
        public OrganizacijaKursa OrganizacijaKursa { get; set; }
    }
}
