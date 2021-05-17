using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class DolazakService : IDolazakService
    {
        private ApplicationDbContext _context;
        private IRepository<Dolazak> DolazakRepository;
        public DolazakService(ApplicationDbContext context, IRepository<Dolazak> repository)
        {
            _context = context;
            DolazakRepository = repository;
        }
        public void Add(Dolazak dolazak)
        {
            try
            {
                DolazakRepository.Add(dolazak);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(Dolazak dolazak)
        {
            try
            {
                DolazakRepository.Delete(dolazak);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Dolazak> GetAll()
        {
            try
            {
                return DolazakRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Dolazak GetById(int id)
        {
            try
            {
                return DolazakRepository.GetById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Update(Dolazak dolazak)
        {
            try
            {
                DolazakRepository.Update(dolazak);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
