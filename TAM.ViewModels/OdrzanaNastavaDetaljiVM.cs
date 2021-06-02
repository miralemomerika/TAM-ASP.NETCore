using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class OdrzanaNastavaDetaljiVM
    {
        public int Id { get; set; }
        public string DatumIVrijemeOdrzavanja { get; set; }
        public bool Zakljucen { get; set; }
        public int Ukupno { get; set; }
        public class Polaznici
        {
            public int Id { get; set; }
            public string ImePrezime { get; set; }
            public bool IsPrisutan { get; set; }
            public int Odsutan { get; set; }
            public int Prisutan { get; set; }
        }
        public List<Polaznici> Dolasci { get; set; }
    }
}
