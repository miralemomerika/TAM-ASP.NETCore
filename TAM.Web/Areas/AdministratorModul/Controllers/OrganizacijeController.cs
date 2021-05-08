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
        private IEmailSender _emailSender;
        private IExceptionHandlerService _exceptionHandlerService;

        public OrganizacijeController(IKursService kursService, IPredavacService predavacService,
            IOrganizacijaKursaService organizacijaKursaService, IPrijavaService prijavaService,
            IPohadjanjeService pohadjanjeService, IEmailSender emailSender,
            IExceptionHandlerService exceptionHandlerService)
        {
            _kursService = kursService;
            _predavacService = predavacService;
            _organizacijaKursaService = organizacijaKursaService;
            _prijavaService = prijavaService;
            _pohadjanjeService = pohadjanjeService;
            _emailSender = emailSender;
            _exceptionHandlerService = exceptionHandlerService;
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

            ViewData["Title"] = "Index";
            ViewData["Controller"] = "Organizacije";
            ViewData["Action"] = "Index";

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
                DatumPocetka = DateTime.Now.AddDays(5),
                DatumZavrsetka = DateTime.Now.AddDays(35)
            };
            model.Uredi = false;
            TempData["action"] = "Spasi";
            return View("Forma", model);
        }

        public IActionResult Uredi(int Id)
        {
            {
                TempData["action"] = "Spasi";
                TempData["nazivTeksta"] = "Uredi organizaciju";
                OrganizacijaKursa organizacija;
                OrganizacijaDodajVM model;
                try
                {
                    organizacija = _organizacijaKursaService.GetAll().AsQueryable()
                        .Include(x => x.Kurs).FirstOrDefault(x => x.Id == Id);
                    if (organizacija == null)
                        throw new NullReferenceException();
                    model = new OrganizacijaDodajVM
                    {
                        Id = organizacija.Id,
                        DatumPocetka = organizacija.DatumPocetka,
                        DatumZavrsetka = organizacija.DatumZavrsetka,
                        Predavaci = _predavacService.GetAll().AsQueryable().Include(x => x.KorisnickiRacun)
                        .Select(x => new SelectListItem
                        {
                            Text = x.KorisnickiRacun.FirstName + " " + x.KorisnickiRacun.LastName,
                            Value = x.Id,
                            Selected = (x.Id == organizacija.PredavacId)
                        }).ToList(),
                        Kurs = organizacija.Kurs,
                        Uredi = true,
                        PredavacId = organizacija.PredavacId
                    };
                }
                catch (Exception ex)
                {
                    _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                    TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                    return RedirectToAction("Prikaz");
                }
                return View("Forma", model);
            }
        }

        public IActionResult Spasi(OrganizacijaDodajVM model)
        {
            try
            {
                if (!model.Uredi)
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
                        .OrderByDescending(x => x.Datum).Take(kurs.Kapacitet)
                        .Include(x => x.Polaznik.KorisnickiRacun)
                        .Select(x => x.Polaznik).ToList();
                    foreach (var item in polaznici)
                    {
                        Pohadjanje pohadjanje = new Pohadjanje
                        {
                            OrganizacijaKursaId = organizacija.Id,
                            PolaznikId = item.Id,
                            Pohadja = true
                        };
                        _pohadjanjeService.Add(pohadjanje);
                    }
                    kurs.PotrebnoOrganizovati = false;
                    _kursService.Update(kurs);
                    string htmlMessage = @"Poštovani,<br/><br/>" +
                                        "Obaviještavamo vas da će se kurs: " +
                                        "<b>{0}</b><br/>" + "Održati dana: " +
                                        "<b>{1}.</b> sa početkom u <b>{2}.</b> sati.<br/>" +
                                        "Očekujemo vaš dolazak :)" +
                                        "<br/><br/><br/>" + "Lijep pozdrav!" + "<br/>" +
                                        "Kulturni centar TAM";
                    Task.Factory.StartNew(() => ObavijestiPolaznike(polaznici, organizacija, htmlMessage));
                }
                else
                {
                    var organizacija = _organizacijaKursaService.GetAll().AsQueryable()
                                    .Include(x => x.Kurs).FirstOrDefault(x => x.Id == model.Id);
                    if (organizacija == null)
                        throw new NullReferenceException();
                    organizacija.PredavacId = model.PredavacId;
                    if(!organizacija.DatumPocetka.Equals(model.DatumPocetka))
                    {
                        organizacija.DatumPocetka = model.DatumPocetka;
                        var upit = _pohadjanjeService.GetAll().AsQueryable();
                        var polaznici = upit.Include(x => x.Polaznik)
                            .ThenInclude(x => x.KorisnickiRacun)
                            .Where(x => x.OrganizacijaKursaId == model.Id).Select(x => x.Polaznik).ToList();
                        string htmlMessage = @"Poštovani,<br/><br/>" +
                                            "Obaviještavamo vas da je došlo do izmjene termina," +
                                            "te će se kurs: " +
                                            "<b>{0}</b><br/>" + "Održati dana: " +
                                            "<b>{1}.</b> sa početkom u <b>{2}.</b> sati.<br/>" +
                                            "Očekujemo vaš dolazak :)" +
                                            "<br/><br/><br/>" + "Lijep pozdrav!" + "<br/>" +
                                            "Kulturni centar TAM";
                        Task.Factory.StartNew(() => ObavijestiPolaznike(polaznici, organizacija, htmlMessage));
                    }
                    organizacija.DatumZavrsetka = model.DatumZavrsetka;
                    _organizacijaKursaService.Update(organizacija);
                }
                TempData["successAdd"] = "Uspješno ste organizovali kurs.";
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
            }
            return RedirectToAction("Index");
        }

        public void PromijeniAktivno(int Id)
        {
            var organizacija = _organizacijaKursaService.GetById(Id);
            organizacija.AktivnaRecenzija = !organizacija.AktivnaRecenzija;
            _organizacijaKursaService.Update(organizacija);
        }

        private async Task ObavijestiPolaznike(List<Polaznik> polaznici, 
            OrganizacijaKursa organizacijaKursa, string htmlMessage)
        {
            string subject = "Obaviještenje o početku održavanja kursa";
            htmlMessage = string.Format(htmlMessage, organizacijaKursa.Kurs.Naziv, 
                organizacijaKursa.DatumPocetka.ToString("dd.MM.yyyy"), 
                organizacijaKursa.DatumPocetka.ToString("HH.mm"));
            foreach (var item in polaznici)
            {
               await _emailSender.SendEmail(item.KorisnickiRacun.Email, subject, htmlMessage);
            }
        }
    }
}
