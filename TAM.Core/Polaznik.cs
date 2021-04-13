using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TAM.Core
{
    public class Polaznik
    {
        [Key, ForeignKey("KorisnickiRacun")]
        public string Id { get; set; }
        public virtual KorisnickiRacun KorisnickiRacun { get; set; }
        public int TipPolaznikaId { get; set; }
        public virtual TipPolaznika TipPolaznika { get; set; }
    }
}
