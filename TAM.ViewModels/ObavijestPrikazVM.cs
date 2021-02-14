using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class ObavijestPrikazVM
    {
        public class Zapis
        {
            public int Id { get; set; }
            public string Naslov { get; set; }
            public string Autor { get; set; }
            public string Datum { get; set; }
            public string Sadrzaj { get; set; }
        }
        public List<Zapis> Zapisi { get; set; }
    }
}
