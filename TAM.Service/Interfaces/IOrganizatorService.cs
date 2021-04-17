using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IOrganizatorService
    {
        public IEnumerable<Organizator> GetAll();
        public Organizator GetById(string Id);
        public void Update(Organizator polaznik);
        public void Delete(Organizator polaznik);
        public void Add(Organizator polaznik);
    }
}
