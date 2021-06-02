using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;

namespace TAM.Service.Interfaces
{
    public interface IOdrzanaNastavaService
    {
        public IEnumerable<OdrzanaNastava> GetAll();
        public OdrzanaNastava GetById(int id);
        public void Update(OdrzanaNastava odrzanaNastava);
        public void Delete(OdrzanaNastava odrzanaNastava);
        public void Add(OdrzanaNastava odrzanaNastava);
    }
}
