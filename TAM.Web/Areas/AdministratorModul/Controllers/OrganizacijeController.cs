using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Interfaces;
using TAM.ViewModels;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class OrganizacijeController : Controller
    {
        private IKursService _kursService;
        private IPredavacService _predavacService;

        public OrganizacijeController(IKursService kursService, IPredavacService predavacService)
        {
            _kursService = kursService;
            _predavacService = predavacService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dodaj(int KursId)
        {
            var model = new OrganizacijaDodajVM
            {
                Kurs = _kursService.GetById(KursId),
                Predavaci = _predavacService.GetAll().AsQueryable().Include(x => x.KorisnickiRacun)
                .Select(x => new SelectListItem
                {
                    Text = $"{x.KorisnickiRacun.FirstName} {x.KorisnickiRacun.LastName}",
                    Value = x.Id
                }).ToList(),
                DatumPocetka = DateTime.Now,
                DatumZavrsetka = DateTime.Now.AddDays(30)
            };
            model.Uredi = false;
            TempData["action"] = "Spasi";
            return View(model);
        }

        public IActionResult Spasi(OrganizacijaDodajVM model)
        {
            return RedirectToAction("Index");
        }
    }
}
