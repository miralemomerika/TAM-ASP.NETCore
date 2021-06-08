using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.API.Dto
{
    public class RadOcijeni
    {
        [Required]
        public string PolaznikId { get; set; }
        [Required]
        public int OrganizacijaKursaId { get; set; }
        [Required]
        public int Ocjena { get; set; }
    }
}
