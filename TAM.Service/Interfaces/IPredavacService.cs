using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IPredavacService
    {
        public IEnumerable<Predavac> GetAll();
        public Predavac GetById(string Id);
        public void Update(Predavac predavac);
        public void Delete(Predavac predavac);
        public void Add(Predavac predavac);
    }
}
