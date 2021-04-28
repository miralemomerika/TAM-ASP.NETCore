using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IPrijavaService
    {
        public Task<Prijava> Add(int kursId);
        public Prijava GetById(int Id);
        public IEnumerable<Prijava> GetAll();

    }
}
