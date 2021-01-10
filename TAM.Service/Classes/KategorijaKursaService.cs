using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class KategorijaKursaService : IKategorijaKursaService
    {

        readonly IRepository<KategorijaKursa> KategorijaKursaRepository;

        public KategorijaKursaService(IRepository<KategorijaKursa> repository)
        {
            KategorijaKursaRepository = repository;
        }

        public void Add(KategorijaKursa kategorijaKursa)
        {
            try
            {
                KategorijaKursaRepository.Add(kategorijaKursa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(KategorijaKursa kategorijaKursa)
        {
            try
            {
                KategorijaKursaRepository.Delete(kategorijaKursa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<KategorijaKursa> GetAll()
        {
            try
            {
                return KategorijaKursaRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public KategorijaKursa GetById(int id)
        {
            try
            {
                return KategorijaKursaRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(KategorijaKursa kategorijaKursa)
        {
            try
            {
                KategorijaKursaRepository.Update(kategorijaKursa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
