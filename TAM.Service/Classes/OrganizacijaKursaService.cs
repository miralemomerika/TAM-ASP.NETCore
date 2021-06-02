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
    public class OrganizacijaKursaService : IOrganizacijaKursaService
    {
        readonly IRepository<OrganizacijaKursa> _repository;
        private KorisnickiRacun _korisnickiRacun;
        private ApplicationDbContext _context;
        private UserManager<KorisnickiRacun> _userManager;

        public OrganizacijaKursaService(IRepository<OrganizacijaKursa> repository,
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
                if(_korisnickiRacun != null)
                {
                    var roles = _userManager.GetRolesAsync(_korisnickiRacun);
                    if(roles.Result.Count != 0)
                    {
                        bool isAdmin = false;
                        bool isPredavac = false;

                        foreach(var item in roles.Result)
                        {
                            if (item == "Administrator")
                                isAdmin = true;
                            if (item == "Predavac")
                                isPredavac = true;
                        }
                        if (isAdmin)
                        {
                            var organizacijaAdmin = _repository.GetAll().AsQueryable();
                            organizacijaAdmin = organizacijaAdmin.Include(x => x.Kurs);
                            return organizacijaAdmin.ToList();
                        }
                        else if(isPredavac)
                        {
                            var organizacijaPredavac = _repository.GetAll().AsQueryable();
                            organizacijaPredavac = organizacijaPredavac.Include(x => x.Kurs);
                            organizacijaPredavac = organizacijaPredavac.Where(x => x.PredavacId == _korisnickiRacun.Id);
                            return organizacijaPredavac.ToList();
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
