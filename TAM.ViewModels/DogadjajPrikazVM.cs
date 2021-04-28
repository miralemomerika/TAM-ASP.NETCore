using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class DogadjajPrikazVM
    {
        public class Zapis
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string DatumIVrijemeOdrzavanja { get; set; }
            public bool Odobren { get; set; }
            public string TipDogadjaja { get; set; }
            public string ImeOrganizatora { get; set; }
            public string Opis { get; set; }
        }
        public List<Zapis> Zapisi { get; set; }
    }
}
