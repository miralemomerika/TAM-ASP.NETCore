using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class DogadjajService : IDogadjajService
    {
        private ApplicationDbContext _context;
        private KorisnickiRacun _korisnickiRacun;
        readonly IRepository<Dogadjaj> DogadjajRepository;
        private UserManager<KorisnickiRacun> _userManager;
        public DogadjajService(IRepository<Dogadjaj> repository,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context,
            UserManager<KorisnickiRacun> userManager)
        {
            DogadjajRepository = repository;
            _context = context;
            if(httpContextAccessor.HttpContext.User.Identity.Name!=null)
            {
                _korisnickiRacun = _context.Users.First(x=>x.UserName==httpContextAccessor.HttpContext.User.Identity.Name);
            }
            _userManager = userManager;
        }
        public void Add(Dogadjaj dogadjaj)
        {
            try
            {
                DogadjajRepository.Add(dogadjaj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Dogadjaj dogadjaj)
        {
            try
            {
                DogadjajRepository.Delete(dogadjaj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Dogadjaj> GetAll()
        {
            try
            {
                var roles = _userManager.GetRolesAsync(_korisnickiRacun);
                bool isAdmin = false;
                bool isOrganizator = false;
                foreach (var item in roles.Result)
                {
                    if (item == "Organizator")
                        isOrganizator = true;
                    if (item == "Administrator")
                        isAdmin = true;
                }
                if (isAdmin)
                {
                    var dogadjaji = DogadjajRepository.GetAll().AsQueryable();
                    dogadjaji = dogadjaji.Include(x => x.TipDogadjaja).Include(x => x.Organizator)
                        .ThenInclude(x => x.KorisnickiRacun);
                    var lista = dogadjaji.ToList();
                    return lista;
                }
                else
                {
                    var dogadjaji = DogadjajRepository.GetAll().AsQueryable();
                    dogadjaji = dogadjaji.Include(x => x.TipDogadjaja).Include(x => x.Organizator)
                        .ThenInclude(x => x.KorisnickiRacun);
                    dogadjaji = dogadjaji.Where(x => x.OrganizatorId == _korisnickiRacun.Id);
                    var lista = dogadjaji.ToList();
                    return lista;
                }

                //var dogadjaji = DogadjajRepository.GetAll().AsQueryable();
                //dogadjaji = dogadjaji.Include(x => x.Organizator).ThenInclude(x => x.KorisnickiRacun);
                //var lista = dogadjaji.ToList();
                //return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dogadjaj GetById(int id)
        {
            try
            {
                var dogadjaji = DogadjajRepository.GetAll().AsQueryable();
                dogadjaji=dogadjaji.Include(x => x.TipDogadjaja).Include(x => x.Organizator)
                        .ThenInclude(x => x.KorisnickiRacun);
                var dogadjaj = dogadjaji.First(x => x.Id == id);
                return dogadjaj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Dogadjaj dogadjaj)
        {
            try
            {
                DogadjajRepository.Update(dogadjaj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
