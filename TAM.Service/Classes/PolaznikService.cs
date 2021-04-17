using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class PolaznikService : IPolaznikService
    {
        private IRepository<Polaznik> PolaznikRepository;

        public PolaznikService(IRepository<Polaznik> repository)
        {
            PolaznikRepository = repository;
        }

        public void Add(Polaznik polaznik)
        {
            PolaznikRepository.Add(polaznik);
        }

        public void Delete(Polaznik polaznik)
        {
            PolaznikRepository.Delete(polaznik);
        }

        public IEnumerable<Polaznik> GetAll()
        {
            return PolaznikRepository.GetAll();
        }

        public Polaznik GetById(string Id)
        {
            return PolaznikRepository.GetById(Id);
        }

        public void Update(Polaznik polaznik)
        {
            PolaznikRepository.Update(polaznik);
        }
    }
}
