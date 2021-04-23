using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Core;
using TAM.Service.Interfaces;
using TAM.ViewModels;
using TAM.Web.Helper;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class OrganizacijeController : Controller
    {
        private IKursService _kursService;
        private IPredavacService _predavacService;
        private IOrganizacijaKursaService _organizacijaKursaService;
        private IPrijavaService _prijavaService;
        private IPohadjanjeService _pohadjanjeService;

        public OrganizacijeController(IKursService kursService, IPredavacService predavacService,
            IOrganizacijaKursaService organizacijaKursaService, IPrijavaService prijavaService,
            IPohadjanjeService pohadjanjeService)
        {
            _kursService = kursService;
            _predavacService = predavacService;
            _organizacijaKursaService = organizacijaKursaService;
            _prijavaService = prijavaService;
            _pohadjanjeService = pohadjanjeService;
        }

        public IActionResult Index(string pretrazivanje, int pageNumber = 1,
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;

            var model = _organizacijaKursaService.GetAll().AsQueryable().Include(x => x.Kurs)
                .Include(x => x.Predavac).ThenInclude(x => x.KorisnickiRacun).ToList().AsQueryable();

            var BrojKategorija = model.Count();
            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                model = model.Where(x => x.Kurs.Naziv.Contains(pretrazivanje));
                BrojKategorija = model.Count();
            }

            ViewData["Title"] = "Prikaz";
            ViewData["Controller"] = "Kursevi";
            ViewData["Action"] = "Prikaz";

            return View(PomocneMetode.Paginacija<OrganizacijaKursa>(pretrazivanje, model, pageNumber, pageSize));
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
            var organizacija = new OrganizacijaKursa
            {
                KursId = model.Kurs.Id,
                PredavacId = model.PredavacId,
                DatumPocetka = model.DatumPocetka,
                DatumZavrsetka = model.DatumZavrsetka
            };
            _organizacijaKursaService.Add(organizacija);
            var kurs = _kursService.GetById(model.Kurs.Id);
            var prijave = _prijavaService.GetAll().AsQueryable();
            var polaznici = prijave.Where(x => x.KursId == kurs.Id)
                .OrderByDescending(x => x.Datum).Take(kurs.Kapacitet).ToList();
            foreach (var item in polaznici)
            {
                Pohadjanje pohadjanje = new Pohadjanje
                {
                    OrganizacijaKursaId = organizacija.Id,
                    PolaznikId = item.PolaznikId,
                    Pohadja = true
                };
                _pohadjanjeService.Add(pohadjanje);
            }
            kurs.PotrebnoOrganizovati = false;
            _kursService.Update(kurs);
            return RedirectToAction("Index");
        }
    }
}
