using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.API.Dto
{
    public class RadGet
    {
        public int Id { get; set; }
        public DateTime DatumPostavljanja { get; set; }
        public string UrlDokumenta { get; set; }
        public string PolaznikId { get; set; }
        public string ImePrezimePolaznika { get; set; }
    }
}
