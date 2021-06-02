using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAM.Core;
using TAM.Service.Interfaces;
using TAM.ViewModels;
using TAM.Web.Helper;
using Microsoft.AspNetCore.Authorization;
using static TAM.ViewModels.NastavaVM;
using static TAM.ViewModels.OdrzanaNastavaPrikazVM;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Authorize(Roles = "Predavac")]
    public class NastavaController : Controller
    {
        readonly IOdrzanaNastavaService _odrzanaNastavaService;
        readonly IOrganizacijaKursaService _organizacijaKursaService;
        readonly IDolazakService _dolazakService;
        readonly IExceptionHandlerService _exceptionHandlerService;
        readonly IProstorijaService _prostorijaService;
        readonly IPohadjanjeService _pohadjanjeService;
        readonly UserManager<KorisnickiRacun> _userManager;
        public NastavaController(IOdrzanaNastavaService odrzanaNastavaService, IOrganizacijaKursaService organizacijaKursaService,
            IDolazakService dolazakService, IExceptionHandlerService exceptionHandlerService,
            UserManager<KorisnickiRacun> userManager, IProstorijaService prostorijaService,
            IPohadjanjeService pohadjanjeService)
        {
            _odrzanaNastavaService = odrzanaNastavaService;
            _organizacijaKursaService = organizacijaKursaService;
            _dolazakService = dolazakService;
            _exceptionHandlerService = exceptionHandlerService;
            _userManager = userManager;
            _prostorijaService = prostorijaService;
            _pohadjanjeService = pohadjanjeService;
        }
        [Authorize(Roles = "Predavac")]
        public async Task<IActionResult> Index(string pretrazivanje, int pageNumber = 1,
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var korisnickoIme = HttpContext.User.Identity.Name;
            var korisnik = await _userManager.FindByNameAsync(korisnickoIme);
            var id = korisnik.Id;
            var organizacijeKursa = _organizacijaKursaService.GetAll().AsQueryable()
                .Include(x=>x.Kurs).Include(x=>x.Predavac).ThenInclude(x=>x.KorisnickiRacun);
            var list = organizacijeKursa.Where(x => x.PredavacId == id).ToList();
            var zapisi = new NastavaVM
            {
                Zapisi = list.Select(x => new Zapis
                {
                    Id=x.Id,
                    DatumPocetka=x.DatumPocetka.ToString("dd.MM.yyyy."),
                    DatumZavrsetka=x.DatumZavrsetka.ToString("dd.MM.yyyy."),
                    Kurs=x.Kurs.Naziv
                }).ToList()
            };
            var podaci = zapisi.Zapisi.ToList().AsQueryable();
            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                podaci = podaci.Where(x => x.Kurs.ToLower().Contains(pretrazivanje.ToLower()));
            }
            ViewData["Title"] = "Index";
            ViewData["Controller"] = "Nastava";
            ViewData["Action"] = "Index";
            return View(PomocneMetode.Paginacija<NastavaVM.Zapis>(pretrazivanje, podaci, pageNumber, pageSize));
        }
        [Authorize(Roles = "Predavac")]
        public async Task<IActionResult> Detalji(int Id)
        {
            var korisnickoIme = HttpContext.User.Identity.Name;
            var korisnik = await _userManager.FindByNameAsync(korisnickoIme);
            var organizacijaKursa = _organizacijaKursaService.GetById(Id);
            var odrzanaNastava = _odrzanaNastavaService.GetAll().AsQueryable()
                .Include(x=>x.OrganizacijaKursa).ThenInclude(x=>x.Kurs)
                .Include(x=>x.Prostorija);
            var lista = odrzanaNastava.Where(x => x.OrganizacijaKursaId == Id).ToList();
            var prikaz = new OdrzanaNastavaPrikazVM
            {
                OrganizacijaId=Id,
                DatumPocetka=organizacijaKursa.DatumPocetka.ToString("dd.MM.yyyy."),
                DatumZavrsetka=organizacijaKursa.DatumZavrsetka.ToString("dd.MM.yyyy."),
                NastavaZapisi = lista.Select
                (x => new NastavaZapis
                {
                    DatumOdrzavanja = x.DatumIVrijemeOdrzavanja.ToString("dd.MM.yyyy. HH:mm"),
                    Id=x.Id,
                    Kurs=x.OrganizacijaKursa.Kurs.ToString(),
                    Prostorija=x.Prostorija.Naziv,
                    Zakljucen=x.Zakljucen
                }).ToList()
            };
            ViewData["Title"] = "Detalji";
            ViewData["Controller"] = "Nastava";
            ViewData["Action"] = "Detalji";
            return View(prikaz);
        }
        public IActionResult Dodaj(int Id)
        {
            var forma = new OdrzanaNastavaDodajVM();
            forma.OrganizacijaKursaId = Id;
            forma.Prostorije = _prostorijaService.GetAll().
                Select(x => new SelectListItem
                {
                    Text=x.Naziv,
                    Value=x.Id.ToString()
                }
                ).ToList();
            return PartialView("Forma", forma);
        }
        public async Task<IActionResult> Spasi(OdrzanaNastavaDodajVM odrzanaNastavaDodaj)
        {
            var korisnickoIme = HttpContext.User.Identity.Name;
            var korisnik = await _userManager.FindByNameAsync(korisnickoIme);
            OdrzanaNastava odrzanaNastava = new OdrzanaNastava()
            {
                OrganizacijaKursaId = odrzanaNastavaDodaj.OrganizacijaKursaId,
                DatumIVrijemeOdrzavanja = DateTime.Now,
                PredavacId = korisnik.Id,
                ProstorijaId = odrzanaNastavaDodaj.ProstorijaId,
                Zakljucen = false
            };
            _odrzanaNastavaService.Add(odrzanaNastava);
            var pohadjanje = _pohadjanjeService.GetAll().AsQueryable();
            var listaPohadjanja = pohadjanje.Where(x => x.OrganizacijaKursaId == odrzanaNastava.OrganizacijaKursaId).ToList();
            foreach (var item in listaPohadjanja)
            {
                Dolazak dolazak = new Dolazak
                {
                    OdrzanaNastavaId = odrzanaNastava.Id,
                    PohadjanjeId = item.Id,
                    Prisutan = false
                };
                _dolazakService.Add(dolazak);
            }
            return RedirectToAction("Detalji", new { Id = odrzanaNastava.OrganizacijaKursaId });
        }

        public IActionResult DetaljiOCasu(int Id)
        {
            var odrzanaNastava = _odrzanaNastavaService.GetById(Id);
            var dolasci = _dolazakService.GetAll().AsQueryable();
            var organizacija = _organizacijaKursaService.GetAll().AsQueryable().FirstOrDefault(x => x.Id == odrzanaNastava.OrganizacijaKursaId);
            var lista = dolasci.Where(x => x.OdrzanaNastavaId == odrzanaNastava.Id)
                .Include(x=>x.Pohadjanje).ThenInclude(x=>x.Polaznik).ThenInclude(x=>x.KorisnickiRacun)             
                .Where(x=>x.Pohadjanje.Aktivan).ToList();
            var detalji = new OdrzanaNastavaDetaljiVM
            {
                Id = Id,
                Zakljucen=odrzanaNastava.Zakljucen,
                DatumIVrijemeOdrzavanja=odrzanaNastava.DatumIVrijemeOdrzavanja.ToString("dd.MM.yyyy. HH:mm"),
                Ukupno = _odrzanaNastavaService.GetAll().AsQueryable().Where(y=>y.Zakljucen&&y.OrganizacijaKursaId==organizacija.Id).Count(),
                Dolasci = lista.Select
                (
                    x => new OdrzanaNastavaDetaljiVM.Polaznici
                    {
                        Id = x.Id,
                        ImePrezime = x.Pohadjanje.Polaznik.KorisnickiRacun.FirstName + " " + x.Pohadjanje.Polaznik.KorisnickiRacun.LastName,
                        IsPrisutan = x.Prisutan,
                        Prisutan=_dolazakService.GetAll().AsQueryable()
                        .Include(y=>y.OdrzanaNastava).Where(y=>y.OdrzanaNastava.Zakljucen&&y.OdrzanaNastava.OrganizacijaKursaId==organizacija.Id && y.Prisutan)
                        .Count(),
                        Odsutan= _dolazakService.GetAll().AsQueryable()
                        .Include(y => y.OdrzanaNastava).Where(y => y.OdrzanaNastava.Zakljucen&& y.OdrzanaNastava.OrganizacijaKursaId == organizacija.Id && y.Prisutan==false)
                        .Count()
                    }
                ).ToList()
            };
            return View(detalji);
        }
        public IActionResult Polaznici(int Id)
        {
            var pohadjanja = _pohadjanjeService.GetAll().AsQueryable();         
            var lista = pohadjanja.Where(x => x.OrganizacijaKursaId == Id)
                .Include(x => x.Polaznik).ThenInclude(x => x.KorisnickiRacun)
                .ToList();
            var detalji = new NastavaPolazniciVM
            {               
                Polaznici = lista.Select
                (
                    x => new NastavaPolazniciVM.PolazniciList
                    {
                        Id = x.Id,
                        ImePrezime = x.Polaznik.KorisnickiRacun.FirstName+" "+x.Polaznik.KorisnickiRacun.LastName,
                        Aktivan=x.Aktivan
                    }
                ).ToList()
            };
            return View(detalji);
        }
        public IActionResult Promijeni(int Id, int Cas)
        {
            var dolazak = _dolazakService.GetById(Id);
            dolazak.Prisutan = !dolazak.Prisutan;
            _dolazakService.Update(dolazak);
            return RedirectToAction("DetaljiOCasu", new { Id = Cas });
        }
        public IActionResult Zakljuci(int Id)
        {
            var odrzanaNastava = _odrzanaNastavaService.GetById(Id);
            odrzanaNastava.Zakljucen = true;
            _odrzanaNastavaService.Update(odrzanaNastava);
            return RedirectToAction("DetaljiOCasu", new { Id = Id });
        }
        public IActionResult Deaktiviraj(int Id, int Cas)
        {
            var dolazak = _dolazakService.GetAll().AsQueryable()
                .Include(x=>x.Pohadjanje).ThenInclude(x=>x.Polaznik)
                .Where(x=>x.Id==Id).First();

            var pohadjanje = _pohadjanjeService.GetAll().AsQueryable()
                .Where(x => x.PolaznikId == dolazak.Pohadjanje.PolaznikId && x.Id == dolazak.PohadjanjeId)
                .FirstOrDefault();
            pohadjanje.Aktivan = !pohadjanje.Aktivan;
            _pohadjanjeService.Update(pohadjanje);
            return RedirectToAction("DetaljiOCasu", new { Id = Cas });
        }
    }
}
