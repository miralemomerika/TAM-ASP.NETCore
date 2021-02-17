using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class PredavacService : IPredavacService
    {
        private IRepository<Predavac> PredavacRepository;
        public PredavacService(IRepository<Predavac> repository)
        {
            PredavacRepository = repository;
        }

        public void Add(Predavac predavac)
        {
            PredavacRepository.Add(predavac);
        }

        public void Delete(Predavac predavac)
        {
            PredavacRepository.Delete(predavac);
        }

        public IEnumerable<Predavac> GetAll()
        {
            return PredavacRepository.GetAll();
        }

        public Predavac GetById(string Id)
        {
            return PredavacRepository.GetById(Id);
        }

        public void Update(Predavac predavac)
        {
            PredavacRepository.Update(predavac);
        }
    }
}
