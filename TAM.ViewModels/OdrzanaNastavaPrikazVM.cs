using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class OdrzanaNastavaPrikazVM
    {
        public int OrganizacijaId { get; set; }
        public string DatumPocetka { get; set; }
        public string DatumZavrsetka { get; set; }
        public class NastavaZapis
        {
            public int Id { get; set; }
            public string Kurs { get; set; }
            public string Prostorija { get; set; }
            public string DatumOdrzavanja { get; set; }
            public bool Zakljucen { get; set; }
        }
        public List<NastavaZapis> NastavaZapisi { get; set; }
    }
}
