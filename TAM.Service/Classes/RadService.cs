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
    public class RadService : IRadService
    {
        readonly IRepository<Rad> _repository;
        private KorisnickiRacun _korisnickiRacun;
        private ApplicationDbContext _context;
        private UserManager<KorisnickiRacun> _userManager;
        private IExceptionHandlerService exceptionHandler;

        public RadService(IRepository<Rad> repository,
                          IHttpContextAccessor httpContextAccessor,
                          ApplicationDbContext context,
                          UserManager<KorisnickiRacun> userManager)
        {
            _repository = repository;
            _context = context;
            _userManager = userManager;
            if (httpContextAccessor.HttpContext.User.Identity.Name != null)
                _korisnickiRacun = _context.Users.First(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public void Add(Rad rad)
        {
            try
            {
                _repository.Add(rad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Rad rad)
        {
            try
            {
                _repository.Delete(rad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Rad> GetAll()
        {
            try
            {
                if (_korisnickiRacun != null)
                {
                    var roles = _userManager.GetRolesAsync(_korisnickiRacun);
                    if(roles.Result.Count != 0)
                    {
                        bool isValidRole = false;
                        bool isPolaznik = false;
                        foreach(var item in roles.Result)
                        {
                            if (item == "Administrator" || item == "Predavac")
                                isValidRole = true;
                            if (item == "Polaznik")
                                isPolaznik = true;
                        }
                        if (isValidRole)
                        {
                            var radovi = _repository.GetAll().AsQueryable();
                            radovi = radovi.Include(x => x.Ispit).Include(x => x.Polaznik).ThenInclude(x => x.KorisnickiRacun);
                            return radovi;
                        }
                        else if (isPolaznik)
                        {
                            var radoviPolaznika = _repository.GetAll().AsQueryable();
                            radoviPolaznika = radoviPolaznika.Where(x => x.PolaznikId == _korisnickiRacun.Id)
                                .Include(x => x.Ispit).Include(x => x.Polaznik).ThenInclude(x => x.KorisnickiRacun);
                            return radoviPolaznika;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Rad GetById(int Id)
        {
            try
            {
                var rad = _repository.GetAll().AsQueryable();
                rad = rad.Include(x => x.Ispit).Include(x => x.Polaznik).ThenInclude(x => x.KorisnickiRacun);
                return rad.First(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
