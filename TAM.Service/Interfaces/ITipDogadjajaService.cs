using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface ITipDogadjajaService
    {
        public IEnumerable<TipDogadjaja> GetAll();
        public TipDogadjaja GetById(int Id);
        public void Update(TipDogadjaja tipDogadjaja);
        public void Delete(TipDogadjaja tipDogadjaja);
        public void Add(TipDogadjaja tipDogadjaja);
    }
}
