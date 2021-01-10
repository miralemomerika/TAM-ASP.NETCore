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
            try
            {
                KategorijaObavijestiRepository.Add(kategorijaObavijesti);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(KategorijaObavijesti kategorijaObavijesti)
        {
            try
            {
                KategorijaObavijestiRepository.Delete(kategorijaObavijesti);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<KategorijaObavijesti> GetAll()
        {
            try
            {
                return KategorijaObavijestiRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public KategorijaObavijesti GetById(int id)
        {

            try
            {
                return KategorijaObavijestiRepository.GetById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Update(KategorijaObavijesti kategorijaObavijesti)
        {

            try
            {
                KategorijaObavijestiRepository.Update(kategorijaObavijesti);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
