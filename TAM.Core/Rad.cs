using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.Core
{
    public class Rad
    {
        public int Id { get; set; }
        public DateTime DatumPostavljanja { get; set; }
        public string UrlDokumenta { get; set; }
        public int IspitId { get; set; }
        public Ispit Ispit { get; set; }
        public string PolaznikId { get; set; }
        public Polaznik Polaznik { get; set; }
    }
}
