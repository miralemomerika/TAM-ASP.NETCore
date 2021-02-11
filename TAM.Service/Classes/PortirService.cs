using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class PortirService : IPortirService
    {
        private IRepository<Portir> PortirRepository;

        public PortirService(IRepository<Portir> repository)
        {
            PortirRepository = repository;
        }

        public void Add(Portir portir)
        {
            PortirRepository.Add(portir);
        }

        public void Delete(Portir portir)
        {
            PortirRepository.Delete(portir);
        }

        public IEnumerable<Portir> GetAll()
        {
            return PortirRepository.GetAll();
        }

        public Portir GetById(int Id)
        {
            return PortirRepository.GetById(Id);
        }

        public void Update(Portir portir)
        {
            PortirRepository.Update(portir);
        }
    }
}
