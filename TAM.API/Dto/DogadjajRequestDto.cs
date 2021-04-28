using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.API.Dto
{
    public class DogadjajRequestDto
    {
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public DateTime DatumIVrijemeOdrzavanja { get; set; }
        [Required]
        public int TipDogadjajaId { get; set; }
        [Required]
        public string Opis { get; set; }

    }
}
