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
    public class IspitService : IIspitService
    {
        readonly IRepository<Ispit> repository;
        private KorisnickiRacun _korisnickiRacun;
        private ApplicationDbContext _context;
        private UserManager<KorisnickiRacun> _userManager;

        public IspitService(IRepository<Ispit> _repository,
                            IHttpContextAccessor httpContextAccessor,
                            ApplicationDbContext context,
                            UserManager<KorisnickiRacun> userManager)
        {
            repository = _repository;
            _context = context;
            _userManager = userManager;
            if (httpContextAccessor.HttpContext.User.Identity.Name != null)
                _korisnickiRacun = _context.Users.First(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public void Add(Ispit ispit)
        {
            try
            {
                repository.Add(ispit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Ispit ispit)
        {
            try
            {
                repository.Delete(ispit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Ispit> GetAll()
        {
            try
            {
                if(_korisnickiRacun != null)
                {
                    var roles = _userManager.GetRolesAsync(_korisnickiRacun);
                    if(roles.Result.Count != 0)
                    {
                        bool isAdmin = false;
                        //bool isPredavac = false;

                        foreach (var item in roles.Result)
                        {
                            if (item == "Administrator")
                                isAdmin = true;
                            //if (item == "Predavac")
                            //    isPredavac = true;
                        }
                        if (isAdmin)
                        {
                            var ispitiAdmin = repository.GetAll().AsQueryable();
                            ispitiAdmin = ispitiAdmin.Include(x => x.OrganizacijaKursa).ThenInclude(x => x.Kurs);
                            return ispitiAdmin.ToList();
                        }
                        //else if(isPredavac)
                        //{
                        //    var ispitiPredavac = repository.GetAll().AsQueryable();
                        //    ispitiPredavac = ispitiPredavac.Include(x => x.OrganizacijaKursa).ThenInclude(x => x.Kurs);
                        //    ispitiPredavac = ispitiPredavac.Where(x => x.OrganizacijaKursa.PredavacId == _korisnickiRacun.Id);
                        //    return ispitiPredavac.ToList();
                        //}
                        else
                        {
                            var ispiti = repository.GetAll().AsQueryable();
                            ispiti = ispiti.Include(x => x.OrganizacijaKursa).ThenInclude(x => x.Kurs);
                            return ispiti.ToList();
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

        public Ispit GetById(int Id)
        {
            try
            {
                var ispiti = repository.GetAll().AsQueryable();
                ispiti = ispiti.Include(x => x.OrganizacijaKursa).ThenInclude(x => x.Kurs);
                return ispiti.First(x => x.Id == Id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Ispit ispit)
        {
            try
            {
                repository.Update(ispit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
