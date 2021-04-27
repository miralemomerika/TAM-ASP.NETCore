using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IPohadjanjeService
    {
        public void Add(Pohadjanje pohadjanje);
        public IEnumerable<Pohadjanje> GetAll();
    }
}
