using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class OdrzanaNastavaService : IOdrzanaNastavaService
    {
        private ApplicationDbContext _context;
        private IRepository<OdrzanaNastava> OdrzanaNastavaRepository;
        public OdrzanaNastavaService(ApplicationDbContext context,
            IRepository<OdrzanaNastava> repository)
        {
            _context = context;
            OdrzanaNastavaRepository = repository;
        }
        public void Add(OdrzanaNastava odrzanaNastava)
        {
            try
            {
                OdrzanaNastavaRepository.Add(odrzanaNastava);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(OdrzanaNastava odrzanaNastava)
        {
            try
            {
                OdrzanaNastavaRepository.Delete(odrzanaNastava);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<OdrzanaNastava> GetAll()
        {
            try
            {
                return OdrzanaNastavaRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public OdrzanaNastava GetById(int id)
        {
            try
            {
                return OdrzanaNastavaRepository.GetById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Update(OdrzanaNastava odrzanaNastava)
        {
            try
            {
                OdrzanaNastavaRepository.Update(odrzanaNastava);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
