using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class SvrhaUplateService : ISvrhaUplateService
    {
        IRepository<SvrhaUplate> SvrhaUplateRepository;

        public SvrhaUplateService(IRepository<SvrhaUplate> repository)
        {
            SvrhaUplateRepository = repository;
        }

        public IEnumerable<SvrhaUplate> GetAll()
        {
            return SvrhaUplateRepository.GetAll();
        }
    }
}
