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
            TipDogadjajaRepository.Add(tipDogadjaja);
        }

        public void Delete(TipDogadjaja tipDogadjaja)
        {
            TipDogadjajaRepository.Delete(tipDogadjaja);
        }

        public IEnumerable<TipDogadjaja> GetAll()
        {
            return TipDogadjajaRepository.GetAll();
        }

        public TipDogadjaja GetById(int Id)
        {
            return TipDogadjajaRepository.GetById(Id);
        }

        public void Update(TipDogadjaja tipDogadjaja)
        {
            TipDogadjajaRepository.Update(tipDogadjaja);
        }
    }
}
