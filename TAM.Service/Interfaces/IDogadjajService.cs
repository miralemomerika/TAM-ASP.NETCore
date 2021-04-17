using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IDogadjajService
    {
        public IEnumerable<Dogadjaj> GetAll();
        public Dogadjaj GetById(int id);
        public void Update(Dogadjaj dogadjaj);
        public void Delete(Dogadjaj dogadjaj);
        public void Add(Dogadjaj dogadjaj);
    }
}
