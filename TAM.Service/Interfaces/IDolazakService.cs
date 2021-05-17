using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IDolazakService
    {
        public IEnumerable<Dolazak> GetAll();
        public Dolazak GetById(int id);
        public void Update(Dolazak dolazak);
        public void Delete(Dolazak dolazak);
        public void Add(Dolazak dolazak);
    }
}
