using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IProstorijaService
    {
        public IEnumerable<Prostorija> GetAll();
        public Prostorija GetById(int Id);
        public void Update(Prostorija prostorija);
        public void Delete(Prostorija prostorija);
        public void Add(Prostorija prostorija);
    }
}
