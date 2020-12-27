using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface ITipPolaznikaService
    {
        public IEnumerable<TipPolaznika> GetAll();
        public TipPolaznika GetById(int Id);
        public void Update(TipPolaznika tipPolaznika);
        public void Delete(TipPolaznika tipPolaznika);
        public void Add(TipPolaznika tipPolaznika);
    }
}
