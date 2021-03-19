using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class ProstorijaService : IProstorijaService
    {
        readonly IRepository<Prostorija> ProstorijaRepository;

        public ProstorijaService(IRepository<Prostorija> repository)
        {
            ProstorijaRepository = repository;
        }

        public void Add(Prostorija prostorija)
        {
            try
            {
                if (prostorija.Naziv == null)
                    throw new Exception("Ne mogu se dodati null vrijednosti");
                if (prostorija.BrojMjesta < 0)
                    throw new Exception("Kapacitet prostorije ne moze biti negativan");
                ProstorijaRepository.Add(prostorija);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(Prostorija prostorija)
        {
            try
            {
                if (prostorija.Naziv == null)
                    throw new Exception("Ne mogu se obrisati null vrijednosti");
                ProstorijaRepository.Delete(prostorija);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Prostorija> GetAll()
        {
            try
            {
                return ProstorijaRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Prostorija GetById(int Id)
        {
            try
            {
                return ProstorijaRepository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Prostorija prostorija)
        {
            try
            {
                if (prostorija.Naziv == null)
                    throw new Exception("Ne mogu se urediti null vrijednosti");
                ProstorijaRepository.Update(prostorija);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
