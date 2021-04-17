using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class PrijavaService : IPrijavaService
    {
        private ApplicationDbContext _context;
        private UserManager<KorisnickiRacun> _userManager;
        private KorisnickiRacun _korisnickiRacun;

        public PrijavaService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
            UserManager<KorisnickiRacun> userManager)
        {
            _context = context;
            _userManager = userManager;
            _korisnickiRacun = _context.KorisnickiRacun.FirstOrDefault(x => x.UserName == httpContextAccessor
              .HttpContext.User.Identity.Name);
        }

        public async Task<Prijava> Add(int kursId)
        {
            var prijava = new Prijava
            {
                KursId = kursId,
                PolaznikId = _context.Polaznik.Find(_korisnickiRacun.Id).Id,
                Datum = DateTime.Now
            };
            _context.Prijava.Add(prijava);
            _context.SaveChanges();
            return prijava;
        }

        public async Task<IEnumerable<Prijava>> GetAll()
        {
            return _context.Prijava.AsEnumerable();
        }
    }
}
