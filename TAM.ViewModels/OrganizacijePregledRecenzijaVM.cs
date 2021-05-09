using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class OrganizacijePregledRecenzijaVM
    {
        public double ProsjecnaOcjenaKursa { get; set; }
        public double ProsjecnaOcjenaPredavaca { get; set; }
        public List<string> Komentari { get; set; } = new List<string>();
        public bool PostojeRecenzije { get; set; }
    }
}
