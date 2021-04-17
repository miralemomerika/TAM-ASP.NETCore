using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IPrijavaService
    {
        public Task<IEnumerable<Prijava>> GetAll();
        public Task<Prijava> Add(int kursId);
    }
}
