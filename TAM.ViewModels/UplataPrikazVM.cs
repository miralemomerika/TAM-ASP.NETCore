using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.ViewModels
{
    public class UplataPrikazVM
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Iznos { get; set; }
        public int? PrijavaId { get; set; }
        public string? NazivKursa { get; set; }
        public string? Polaznik { get; set; }
        public int? DogadjajId { get; set; }
        public string? NazivDogadjaja { get; set; }
        public string? Organizator { get; set; }
    }
}
