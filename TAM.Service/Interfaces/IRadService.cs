using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IRadService
    {
        public IEnumerable<Rad> GetAll();
        public Rad GetById(int Id);
        public void Delete(Rad rad);
        public void Add(Rad rad);
    }
}
