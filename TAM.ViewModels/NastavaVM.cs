using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class NastavaVM
    {
        public class Zapis
        {
            public int Id { get; set; }
            public string Kurs { get; set; }
            public string DatumPocetka { get; set; }
            public string DatumZavrsetka { get; set; }
        }
        
        public List<Zapis> Zapisi { get; set; }
    }
}
