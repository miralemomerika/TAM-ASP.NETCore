using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class KursService : IKursService
    {
        readonly IRepository<Kurs> KursRepository;

        public KursService(IRepository<Kurs> repository)
        {
            KursRepository = repository;
        }
        public void Add(Kurs kurs)
        {
            try
            {
                if (kurs.Naziv == null)
                    throw new Exception("Nije moguce dodati null vrijednost");
                KursRepository.Add(kurs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Kurs kurs)
        {
            try
            {
                if (kurs.Naziv == null)
                    throw new Exception("Nije moguce izbrisati null vrijednost");
                KursRepository.Delete(kurs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Kurs> GetAll()
        {
            try
            {
                return KursRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Kurs GetById(int id)
        {
            try
            {
                return KursRepository.GetById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public void Update(Kurs kurs)
        {
            try
            {
                if (kurs.Naziv == null)
                    throw new Exception("Nije moguce urediti null vrijednost");
                KursRepository.Update(kurs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
