using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class KursPrikazVM
    {
        public class Zapis
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string KategorijaKursa { get; set; }
            public float Cijena { get; set; }
            public int BrojCasova { get; set; }
            public int Kapacitet { get; set; }
            public string Opis { get; set; }
        }
        public List<Zapis> Zapisi { get; set; }
    }
}
