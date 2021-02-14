using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IObavijestService
    {
        public IEnumerable<Obavijest> GetAll();
        public Obavijest GetById(int id);
        public void Update(Obavijest obavijest);
        public void Delete(Obavijest obavijest);
        public void Add(Obavijest obavijest);
    }
}
