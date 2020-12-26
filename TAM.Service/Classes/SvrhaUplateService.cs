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
        readonly IRepository<SvrhaUplate> SvrhaUplateRepository;

        public SvrhaUplateService(IRepository<SvrhaUplate> repository)
        {
            SvrhaUplateRepository = repository;
        }

        public IEnumerable<SvrhaUplate> GetAll()
        {
            return SvrhaUplateRepository.GetAll();
        }

        public SvrhaUplate GetById(int Id)
        {
            return SvrhaUplateRepository.GetById(Id);
        }

        public void Update(SvrhaUplate svrha)
        {
            SvrhaUplateRepository.Update(svrha);
        }

        public void Delete(SvrhaUplate svrha)
        {
            SvrhaUplateRepository.Delete(svrha);
        }

        public void Add(SvrhaUplate svrha)
        {
            SvrhaUplateRepository.Add(svrha);
        }
    }
}
