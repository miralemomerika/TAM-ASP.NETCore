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
            try
            {
                return SvrhaUplateRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SvrhaUplate GetById(int Id)
        {
            try
            {
                return SvrhaUplateRepository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(SvrhaUplate svrha)
        {
            try
            {
                SvrhaUplateRepository.Update(svrha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(SvrhaUplate svrha)
        {
            try
            {
                SvrhaUplateRepository.Delete(svrha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Add(SvrhaUplate svrha)
        {
            try
            {
                SvrhaUplateRepository.Add(svrha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
