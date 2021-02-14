using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class ObavijestService : IObavijestService
    {
        readonly IRepository<Obavijest> ObavijestRepository;
        public ObavijestService(IRepository<Obavijest> repository)
        {
            ObavijestRepository = repository;
        }
        public void Add(Obavijest obavijest)
        {
            try
            {
                if (obavijest.Naslov == null || obavijest.KorisnickiRacunId ==""|| obavijest.Sadrzaj == null)
                    throw new Exception("Ne mogu se dodati null vrijednosti");
                ObavijestRepository.Add(obavijest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Obavijest obavijest)
        {
            try
            {
                if (obavijest.Naslov == null || obavijest.KorisnickiRacunId == null || obavijest.Sadrzaj == null)
                    throw new Exception("Ne mogu se obrisati null vrijednosti");
                ObavijestRepository.Delete(obavijest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Obavijest> GetAll()
        {
            try
            {
                return ObavijestRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }    
        }

        public Obavijest GetById(int id)
        {
            try
            {
                return ObavijestRepository.GetById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Update(Obavijest obavijest)
        {
            try
            {
                if (obavijest.Naslov == null || obavijest.KorisnickiRacunId == null || obavijest.Sadrzaj == null)
                    throw new Exception("Ne mogu se urediti null vrijednosti");
                ObavijestRepository.Update(obavijest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
