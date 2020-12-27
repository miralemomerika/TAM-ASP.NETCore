using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class TipPolaznikaService : ITipPolaznikaService
    {

        readonly IRepository<TipPolaznika> TipPolaznikaRepository;

        public TipPolaznikaService(IRepository<TipPolaznika> repository)
        {
            TipPolaznikaRepository = repository;
        }

        public void Add(TipPolaznika tipPolaznika)
        {
            TipPolaznikaRepository.Add(tipPolaznika);
        }

        public void Delete(TipPolaznika tipPolaznika)
        {
            TipPolaznikaRepository.Delete(tipPolaznika);
        }

        public IEnumerable<TipPolaznika> GetAll()
        {
            return TipPolaznikaRepository.GetAll();
        }

        public TipPolaznika GetById(int Id)
        {
            return TipPolaznikaRepository.GetById(Id);
        }

        public void Update(TipPolaznika tipPolaznika)
        {
            TipPolaznikaRepository.Update(tipPolaznika);
        }
    }
}
