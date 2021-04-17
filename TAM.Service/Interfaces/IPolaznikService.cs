using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IPolaznikService
    {
        public IEnumerable<Polaznik> GetAll();
        public Polaznik GetById(string Id);
        public void Update(Polaznik polaznik);
        public void Delete(Polaznik polaznik);
        public void Add(Polaznik polaznik);
    }
}
