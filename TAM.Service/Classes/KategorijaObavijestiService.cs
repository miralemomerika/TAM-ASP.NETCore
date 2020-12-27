using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class KategorijaObavijestiService : IKategorijaObavijestiService
    {

        readonly IRepository<KategorijaObavijesti> KategorijaObavijestiRepository;

        public KategorijaObavijestiService(IRepository<KategorijaObavijesti> repository)
        {
            KategorijaObavijestiRepository = repository;
        }

        public void Add(KategorijaObavijesti kategorijaObavijesti)
        {
            KategorijaObavijestiRepository.Add(kategorijaObavijesti);
        }

        public void Delete(KategorijaObavijesti kategorijaObavijesti)
        {
            KategorijaObavijestiRepository.Delete(kategorijaObavijesti);
        }

        public IEnumerable<KategorijaObavijesti> GetAll()
        {
            return KategorijaObavijestiRepository.GetAll();
        }

        public KategorijaObavijesti GetById(int id)
        {
            return KategorijaObavijestiRepository.GetById(id);
        }

        public void Update(KategorijaObavijesti kategorijaObavijesti)
        {
            KategorijaObavijestiRepository.Update(kategorijaObavijesti);
        }
    }
}
