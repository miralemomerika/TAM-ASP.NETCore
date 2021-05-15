using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class NastavaPolazniciVM
    {
        public class PolazniciList
        {
            public int Id { get; set; }
            public string ImePrezime { get; set; }
            public bool Aktivan { get; set; }
        }
        public List<PolazniciList> Polaznici { get; set; }
    }
}
