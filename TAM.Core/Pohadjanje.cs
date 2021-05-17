using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TAM.Core
{
    public class Pohadjanje
    {
        public int Id { get; set; }
        public Nullable<int> OcjenaPohadjanja { get; set; }
        [DefaultValue(true)]
        public bool Pohadja { get; set; }
        public string PolaznikId { get; set; }
        public Polaznik Polaznik { get; set; }
        public int OrganizacijaKursaId { get; set; }
        public OrganizacijaKursa OrganizacijaKursa { get; set; }
        public bool Aktivan { get; set; }
    }
}
