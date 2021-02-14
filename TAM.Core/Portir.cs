using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TAM.Core
{
    public class Portir
    {
        [Key, ForeignKey("KorisnickiRacun")]
        public string Id { get; set; }
        public virtual KorisnickiRacun KorisnickiRacun { get; set; }
    }
}
