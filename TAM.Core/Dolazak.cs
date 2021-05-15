using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TAM.Core
{
    public class Dolazak
    {
        public int Id { get; set; }
        [ForeignKey(nameof(OdrzanaNastavaId))]
        public virtual OdrzanaNastava OdrzanaNastava { get; set; }
        public int OdrzanaNastavaId { get; set; }
        [ForeignKey(nameof(PohadjanjeId))]
        public virtual Pohadjanje Pohadjanje { get; set; }
        public int PohadjanjeId { get; set; }
        public bool Prisutan { get; set; }
    }
}
