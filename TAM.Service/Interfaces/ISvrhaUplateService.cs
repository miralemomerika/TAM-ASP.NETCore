using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface ISvrhaUplateService
    {
        public IEnumerable<SvrhaUplate> GetAll();
    }
}
