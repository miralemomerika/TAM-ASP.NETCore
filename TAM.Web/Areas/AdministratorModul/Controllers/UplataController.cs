using Microsoft.AspNetCore.Mvc;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TAM.Service.Interfaces;
using TAM.ViewModels;
using TAM.Web.Helper;
using static TAM.ViewModels.UplataPrikazVM;
using TAM.Core;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class UplataController : Controller
    {
        readonly IUplataService uplataService;
        readonly IKursService kursService;
        readonly IPolaznikService polaznikService;
        readonly IDogadjajService dogadjajService;
        readonly IOrganizatorService organizatorService;
        readonly IPrijavaService prijavaService;
        readonly IExceptionHandlerService exceptionHandler;

        public UplataController(IUplataService _uplataService,
                                IExceptionHandlerService _exceptionHandler,
                                IKursService _kursService,
                                IPolaznikService _polaznikService,
                                IPrijavaService _prijavaService,
                                IDogadjajService _dogadjajService,
                                IOrganizatorService _organizatorService)
        {
            uplataService = _uplataService;
            exceptionHandler = _exceptionHandler;
            kursService = _kursService;
            polaznikService = _polaznikService;
            dogadjajService = _dogadjajService;
            organizatorService = _organizatorService;
            prijavaService = _prijavaService;
        }

        //[Authorize(Roles = "Portir,Administrator")]
        public IActionResult Prikaz(string pretrazivanje, int pageNumber = 1,
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;            
            
            var lista = uplataService.GetAll();
            List<UplataPrikazVM> uplate = new List<UplataPrikazVM>();
            foreach(var x in lista)
            {
                if(x.PrijavaId != null)
                {
                    uplate.Add(new UplataPrikazVM
                    {
                        Id = x.Id,
                        Datum = x.Datum,
                        Iznos = x.Iznos,
                        PrijavaId = x.PrijavaId,
                        NazivKursa = x.Prijava.Kurs.Naziv + " | Kurs",
                        Polaznik = x.Prijava.Polaznik.KorisnickiRacun.FirstName + " "
                                + x.Prijava.Polaznik.KorisnickiRacun.LastName,
                    });
                }
                else
                {
                    uplate.Add(new UplataPrikazVM
                    {
                        Id = x.Id,
                        Datum = x.Datum,
                        Iznos = x.Iznos,
                        DogadjajId = x.DogadjajId,
                        NazivDogadjaja = x.Dogadjaj.Naziv + " | Dogadjaj",
                        Organizator = x.Dogadjaj.Organizator.KorisnickiRacun.FirstName + " "
                                + x.Dogadjaj.Organizator.KorisnickiRacun.LastName
                    });
                }
            }

            var podaci = uplate.ToList().AsQueryable();
            var BrojKategorija = podaci.Count();
            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                podaci = podaci.Where(x => x.NazivKursa.ToLower().Contains(pretrazivanje.ToLower()) 
                    || x.NazivDogadjaja.ToLower().Contains(pretrazivanje.ToLower()));
                BrojKategorija = podaci.Count();
            }

            ViewData["Title"] = "Uplate";
            ViewData["Controller"] = "Uplata";
            ViewData["Action"] = "Prikaz";

            return View(PomocneMetode.Paginacija<UplataPrikazVM>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult Dodaj()
        {
            TempData["action"] = "Spasi";
            TempData["nazivTeksta"] = "Dodaj uplatu";

            UplataDodajVM model = new UplataDodajVM();

            model.Svrhe = GetSvrhe();
            model.NovaUplata = true;
            return View("Dodaj", model);
        }

        public List<SelectListItem> GetSvrhe()
        {
            List<SelectListItem> svrhe = new List<SelectListItem>
            {
                new SelectListItem{Value = null, Text = "Izaberite svrhu.."},
                new SelectListItem{Value = "Kurs", Text = "Kurs"},
                new SelectListItem{Value = "Dogadjaj", Text = "Dogadjaj"}
            };

            return svrhe;
        }

        public IActionResult PrijavaInput()
        {
            GetPrijavaInput();

            return View();
        }

        public IActionResult DogadjajInput()
        {
            GetDogadjajInput();

            return View();
        }

        public void GetPrijavaInput()
        {
            TempData["kursevi"] = kursService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            var polaznici = polaznikService.GetAll().AsQueryable().Include(x => x.KorisnickiRacun);
            TempData["polaznici"] = polaznici.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.KorisnickiRacun.FirstName + ' ' + x.KorisnickiRacun.LastName
            }).ToList();
        }

        public void GetDogadjajInput()
        {
            TempData["dogadjaji"] = dogadjajService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            var organizatori = organizatorService.GetAll().AsQueryable().Include(x => x.KorisnickiRacun);
            TempData["organizatori"] = organizatori.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.KorisnickiRacun.FirstName + ' ' + x.KorisnickiRacun.LastName
            }).ToList();
        }

        public IActionResult Uredi(int Id)
        {
            TempData["action"] = "Spasi";
            TempData["nazivTeksta"] = "Uredi obavijesti";
            var dogadjaj = new Dogadjaj();
            var prijava = new Prijava();
            Uplata uplata = new Uplata();
            try
            {
                uplata = uplataService.GetById(Id);
            }
            catch (Exception ex)
            {
                exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti";
                return RedirectToAction("Prikaz");
            }
            if(uplata.PrijavaId == null)
            {
                dogadjaj = dogadjajService.GetById(uplata.Dogadjaj.Id);
            }
            else
            {
                prijava = prijavaService.GetById(uplata.Prijava.Id);
            }

            UplataDodajVM model = new UplataDodajVM()
            {
                Id = uplata.Id,
                Datum = uplata.Datum,
                Iznos = uplata.Iznos,
            };

            if(prijava.Id == 0)
            {
                model.SvrhaId = "Dogadjaj";
                model.DogadjajId = dogadjaj.Id;
                model.OrganizatorId = dogadjaj.OrganizatorId;
                GetDogadjajInput();
            }
            else
            {
                model.SvrhaId = "Kurs";
                model.KursId = prijava.KursId;
                model.PolaznikId = prijava.PolaznikId;
                GetPrijavaInput();
            }
            model.NovaUplata = false;
            model.Svrhe = GetSvrhe();

            TempData["svrha"] = model.SvrhaId;
            return View("Uredi", model);
        }

        public IActionResult Spasi(UplataDodajVM model)
        {
            if (!ModelState.IsValid)
            {
                if (model.NovaUplata)
                {
                    return RedirectToAction("Dodaj");
                }
                else
                {
                    return RedirectToAction("Uredi", model.Id);
                }
            }
            else
            {
                if (model.SvrhaId == "Kurs")
                    model.UplataKursa = true;
                else
                    model.UplataKursa = false;

                var prijava = new Prijava();
                var dogadjaj = new Dogadjaj();
                try
                {
                    if (model.UplataKursa)
                    {
                        prijava = prijavaService.GetAll().AsQueryable()
                            .Where(x => x.PolaznikId == model.PolaznikId && x.KursId == model.KursId).FirstOrDefault();
                    }
                    else
                    {
                        dogadjaj = dogadjajService.GetAll().AsQueryable()
                            .Where(x => x.Id == model.DogadjajId && x.OrganizatorId == model.OrganizatorId).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                    TempData["exception"] = "Nemoguce dobaviti potrebne podatke.";
                    return RedirectToAction("Prikaz");
                }
                try
                {
                    if (model.NovaUplata)
                    {
                        Uplata uplata = new Uplata()
                        {
                            Datum = model.Datum,
                            Iznos = model.Iznos,
                        };
                        if (model.UplataKursa)
                        {
                            uplata.PrijavaId = prijava.Id;
                        }
                        else
                        {
                            uplata.DogadjajId = dogadjaj.Id;
                        }
                        uplataService.Add(uplata);
                        TempData["successAdd"] = "Uspjesno ste dodali uplatu.";
                    }
                    else
                    {
                        Uplata uplata = uplataService.GetById(model.Id);
                        uplata.Datum = model.Datum;
                        uplata.Iznos = model.Iznos;
                        if (model.UplataKursa)
                        {
                            uplata.PrijavaId = prijava.Id;
                        }
                        else
                        {
                            uplata.DogadjajId = dogadjaj.Id;
                        }
                        uplataService.Update(uplata);
                        TempData["successUpdate"] = "Uspjesno ste uredili uplatu.";
                    }
                }
                catch (Exception ex)
                {
                    exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                    TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                    return RedirectToAction("Prikaz");
                }
            }
            return RedirectToAction("Prikaz");
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Obrisi(int Id)
        {
            TempData["action"] = "Obrisi";
            TempData["nazivTeksta"] = "Potvrda";
            Uplata uplata;
            try
            {
                uplata = uplataService.GetById(Id);
            }
            catch (Exception ex)
            {
                exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti.";
                return RedirectToAction("Prikaz");
            }

            UplataPrikazVM model = new UplataPrikazVM()
            {
                Id = uplata.Id,
                Datum = uplata.Datum,
                Iznos = uplata.Iznos,
            };

            if(uplata.PrijavaId == 0 || uplata.PrijavaId == null)
            {
                model.DogadjajId = uplata.DogadjajId;
                model.NazivDogadjaja = uplata.Dogadjaj.Naziv;
                model.Organizator = uplata.Dogadjaj.Organizator.KorisnickiRacun.FirstName + ' ' + uplata.Dogadjaj.Organizator.KorisnickiRacun.LastName;
            }
            else
            {
                model.PrijavaId = uplata.PrijavaId;
                model.NazivKursa = uplata.Prijava.Kurs.Naziv;
                model.Polaznik = uplata.Prijava.Polaznik.KorisnickiRacun.FirstName + ' ' + uplata.Prijava.Polaznik.KorisnickiRacun.LastName;
            }

            return View("Obrisi", model);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Obrisi(UplataPrikazVM model)
        {
            try
            {
                Uplata uplata = uplataService.GetById(model.Id);
                uplataService.Delete(uplata);
                TempData["deleted"] = "Uspjesno ste obrisali uplatu.";
            }
            catch (Exception ex)
            {
                exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti.";
                return RedirectToAction("Prikaz");
            }

            return RedirectToAction("Prikaz");
        }
    }
}