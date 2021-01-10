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
            try
            {
                TipPolaznikaRepository.Add(tipPolaznika);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public void Delete(TipPolaznika tipPolaznika)
        {
            try
            {
                TipPolaznikaRepository.Delete(tipPolaznika);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public IEnumerable<TipPolaznika> GetAll()
        {
            return TipPolaznikaRepository.GetAll();
        }

        public TipPolaznika GetById(int Id)
        {
            try
            {
                return TipPolaznikaRepository.GetById(Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void Update(TipPolaznika tipPolaznika)
        {
            TipPolaznikaRepository.Update(tipPolaznika);
        }
    }
}
