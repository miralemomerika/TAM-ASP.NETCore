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
            KategorijaKursaRepository.Add(kategorijaKursa);
        }

        public void Delete(KategorijaKursa kategorijaKursa)
        {
            KategorijaKursaRepository.Delete(kategorijaKursa);
        }

        public IEnumerable<KategorijaKursa> GetAll()
        {
            return KategorijaKursaRepository.GetAll();
        }

        public KategorijaKursa GetById(int id)
        {
            return KategorijaKursaRepository.GetById(id);
        }

        public void Update(KategorijaKursa kategorijaKursa)
        {
            KategorijaKursaRepository.Update(kategorijaKursa);
        }
    }
}
