using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class TipDogadjajaService : ITipDogadjajaService
    {
        public IRepository<TipDogadjaja> TipDogadjajaRepository;

        public TipDogadjajaService(IRepository<TipDogadjaja> tipDogadjajaService)
        {
            TipDogadjajaRepository = tipDogadjajaService;
        }
        public void Add(TipDogadjaja tipDogadjaja)
        {
            try
            {
                TipDogadjajaRepository.Add(tipDogadjaja);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(TipDogadjaja tipDogadjaja)
        {
            try
            {
                TipDogadjajaRepository.Delete(tipDogadjaja);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<TipDogadjaja> GetAll()
        {
            try
            {
                return TipDogadjajaRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TipDogadjaja GetById(int Id)
        {
            try
            {
                return TipDogadjajaRepository.GetById(Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Update(TipDogadjaja tipDogadjaja)
        {
            try
            {
                TipDogadjajaRepository.Update(tipDogadjaja);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
