using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IIspitService
    {
        public IEnumerable<Ispit> GetAll();
        public Ispit GetById(int Id);
        public void Update(Ispit ispit);
        public void Delete(Ispit ispit);
        public void Add(Ispit ispit);
    }
}
