using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IUplataService
    {
        public IEnumerable<Uplata> GetAll();
        public Uplata GetById(int Id);
        public void Update(Uplata uplata);
        public void Delete(Uplata uplata);
        public void Add(Uplata uplata);
    }
}
