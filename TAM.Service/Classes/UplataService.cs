using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class UplataService : IUplataService
    {
        readonly IRepository<Uplata> repository;

        public UplataService(IRepository<Uplata> _repository)
        {
            repository = _repository;
        }

        public void Add(Uplata uplata)
        {
            try
            {
                repository.Add(uplata);
            }
            catch (Exception er)
            {

                throw er;
            }
        }

        public void Delete(Uplata uplata)
        {
            try
            {
                repository.Delete(uplata);
            }
            catch (Exception er)
            {

                throw er;
            }
        }

        public IEnumerable<Uplata> GetAll()
        {
            try
            {
                var uplate = repository.GetAll().AsQueryable();
                uplate = uplate.Include(x => x.Dogadjaj).ThenInclude(x => x.Organizator).ThenInclude(x => x.KorisnickiRacun)
                    .Include(x => x.Dogadjaj)
                    .Include(x => x.Prijava).ThenInclude(x => x.Kurs)
                    .Include(x => x.Prijava).ThenInclude(x => x.Polaznik).ThenInclude(x => x.KorisnickiRacun);
                var lista = uplate.ToList();
                return lista;
            }
            catch (Exception er)
            {

                throw er;
            }
        }

        public Uplata GetById(int Id)
        {
            try
            {
                var uplate = repository.GetAll().AsQueryable();
                uplate = uplate.Include(x => x.Dogadjaj).ThenInclude(x => x.Organizator).ThenInclude(x => x.KorisnickiRacun)
                    .Include(x => x.Dogadjaj)
                    .Include(x => x.Prijava).ThenInclude(x => x.Kurs)
                    .Include(x => x.Prijava).ThenInclude(x => x.Polaznik).ThenInclude(x => x.KorisnickiRacun);
                var uplata = uplate.First(x => x.Id == Id);
                return uplata;
            }
            catch (Exception er)
            {

                throw er;
            }
        }

        public void Update(Uplata uplata)
        {
            try
            {
                repository.Update(uplata);
            }
            catch (Exception er)
            {

                throw er;
            }
        }
    }
}
