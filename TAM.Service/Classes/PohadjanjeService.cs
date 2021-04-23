using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class PohadjanjeService : IPohadjanjeService
    {
        private IRepository<Pohadjanje> _repository;

        public PohadjanjeService(IRepository<Pohadjanje> repository)
        {
            _repository = repository;
        }

        public void Add(Pohadjanje pohadjanje)
        {
            _repository.Add(pohadjanje);
        }
    }
}
