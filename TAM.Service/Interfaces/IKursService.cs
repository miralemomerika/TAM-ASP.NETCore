using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IKursService
    {
        public IEnumerable<Kurs> GetAll();
        public Kurs GetById(int id);
        public void Update(Kurs kurs);
        public void Delete(Kurs kurs);
        public void Add(Kurs kurs);
    }
}
