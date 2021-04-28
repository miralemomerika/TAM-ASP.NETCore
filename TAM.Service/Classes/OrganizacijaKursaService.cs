using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class OrganizacijaKursaService : IOrganizacijaKursaService
    {
        readonly IRepository<OrganizacijaKursa> _repository;

        public OrganizacijaKursaService(IRepository<OrganizacijaKursa> repository)
        {
            _repository = repository;
        }

        public void Add(OrganizacijaKursa organizacija)
        {
            try
            {
                _repository.Add(organizacija);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(OrganizacijaKursa organizacija)
        {
            try
            {
                _repository.Delete(organizacija);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<OrganizacijaKursa> GetAll()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public OrganizacijaKursa GetById(int id)
        {
            try
            {
                return _repository.GetById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Update(OrganizacijaKursa kurs)
        {
            try
            {
                _repository.Update(kurs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
