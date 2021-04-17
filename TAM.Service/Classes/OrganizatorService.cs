using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class OrganizatorService : IOrganizatorService
    {
        private IRepository<Organizator> OrganizatorRepository;

        public OrganizatorService(IRepository<Organizator> repository)
        {
            OrganizatorRepository = repository;
        }

        public void Add(Organizator polaznik)
        {
            OrganizatorRepository.Add(polaznik);
        }

        public void Delete(Organizator polaznik)
        {
            OrganizatorRepository.Delete(polaznik);
        }

        public IEnumerable<Organizator> GetAll()
        {
            return OrganizatorRepository.GetAll();
        }

        public Organizator GetById(string Id)
        {
            return OrganizatorRepository.GetById(Id);
        }

        public void Update(Organizator polaznik)
        {
            OrganizatorRepository.Update(polaznik);
        }
    }
}
