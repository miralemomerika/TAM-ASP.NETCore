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
    public class RecenzijeService : IRecenzijeService
    {
        private ApplicationDbContext _context;
        private UserManager<KorisnickiRacun> _userManager;
        private KorisnickiRacun _korisnickiRacun;
        private IRepository<Recenzija> _repository;

        public RecenzijeService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
            UserManager<KorisnickiRacun> userManager, IRepository<Recenzija> repository)
        {
            _context = context;
            _userManager = userManager;
            _korisnickiRacun = _context.KorisnickiRacun.FirstOrDefault(x => x.UserName == httpContextAccessor
              .HttpContext.User.Identity.Name);
            _repository = repository;
        }

        public void Add(Recenzija recenzija)
        {
            try
            {
                _repository.Add(recenzija);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Recenzija recenzija)
        {
            try
            {
                _repository.Delete(recenzija);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Recenzija> GetAll()
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

        public IEnumerable<OrganizacijaKursa> GetAllAktivne()
        {
            return _context.Pohadjanje
                .Where(x => x.PolaznikId == _korisnickiRacun.Id && x.Pohadja == true)
                .Where(x => x.OrganizacijaKursa.AktivnaRecenzija == true)
                .Include(x => x.OrganizacijaKursa.Kurs)
                .Include(x => x.OrganizacijaKursa.Predavac).ThenInclude(x => x.KorisnickiRacun)
                .Select(x => x.OrganizacijaKursa).ToList();
        }

        public Recenzija GetById(int Id)
        {
            try
            {
                return _repository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Recenzija recenzija)
        {
            try
            {
                _repository.Update(recenzija);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
