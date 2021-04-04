using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAM.API.Dto
{
    public class OdgovorRegistracijaDto
    {
        public bool JeUspjesnaRegistracija { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
