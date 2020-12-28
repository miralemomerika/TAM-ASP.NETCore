using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class ProstorijaService : IProstorijaService
    {
        readonly IRepository<Prostorija> ProstorijaRepository;

        public ProstorijaService(IRepository<Prostorija> repository)
        {
            ProstorijaRepository = repository;
        }

        public void Add(Prostorija prostorija)
        {
            ProstorijaRepository.Add(prostorija);
        }

        public void Delete(Prostorija prostorija)
        {
            ProstorijaRepository.Delete(prostorija);
        }

        public IEnumerable<Prostorija> GetAll()
        {
            return ProstorijaRepository.GetAll();
        }

        public Prostorija GetById(int Id)
        {
            return ProstorijaRepository.GetById(Id);
        }

        public void Update(Prostorija prostorija)
        {
            ProstorijaRepository.Update(prostorija);
        }
    }
}
