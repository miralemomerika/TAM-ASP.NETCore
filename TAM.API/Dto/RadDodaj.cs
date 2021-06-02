using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.API.Dto
{
    public class RadDodaj
    {
        [Required]
        public int IspitId { get; set; }
        [Required]
        public string UrlDokumenta { get; set; }
    }
}
