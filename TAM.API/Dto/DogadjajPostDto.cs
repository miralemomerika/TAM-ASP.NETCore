using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.API.Dto
{
    public class DogadjajPostDto
    {
        public string Naziv { get; set; }
        public DateTime DatumIVrijemeOdrzavanja { get; set; }
        public int TipDogadjajaId { get; set; }        
    }
}
