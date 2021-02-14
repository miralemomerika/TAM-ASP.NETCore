using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IPortirService
    {
        public IEnumerable<Portir> GetAll();
        public Portir GetById(int Id);
        public void Update(Portir portir);
        public void Delete(Portir portir);
        public void Add(Portir portir);
    }
}
