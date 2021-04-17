using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.API.Dto
{
    public class DogadjajGetDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string DatumIVrijemeOdrzavanja { get; set; }
        public bool Odobren { get; set; }
        public string TipDogadjaja { get; set; }
        public string ImeOrganizatora { get; set; }
    }
}
