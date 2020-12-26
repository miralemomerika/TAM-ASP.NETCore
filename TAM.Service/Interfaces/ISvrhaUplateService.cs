using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface ISvrhaUplateService
    {
        public IEnumerable<SvrhaUplate> GetAll();
        public SvrhaUplate GetById(int Id);
        public void Update(SvrhaUplate svrha);
        public void Delete(SvrhaUplate svrha);
        public void Add(SvrhaUplate svrha);
    }
}
