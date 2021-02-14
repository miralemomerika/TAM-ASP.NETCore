using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Interfaces;
using TAM.Service.Classes;
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore;
using TAM.Web.Helper;
using TAM.ViewModels;
using static TAM.ViewModels.ObavijestPrikazVM;
using TAM.Core;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class ObavijestiController : Controller
    {

        readonly IKategorijaObavijestiService KategorijaObavijestiService;
        readonly IObavijestService ObavijestService;
        readonly IExceptionHandlerService ExceptionHandlerService;
        public ObavijestiController(IKategorijaObavijestiService kategorijaObavijestiService, 
            IExceptionHandlerService exceptionHandlerService, IObavijestService obavijestService)
        {
            KategorijaObavijestiService = kategorijaObavijestiService;
            ExceptionHandlerService = exceptionHandlerService;
            ObavijestService = obavijestService;
        }
        //dio za obavijesti
        public IActionResult Prikaz(string pretrazivanje, int pageNumber=1, int pageSize=5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var temp = ObavijestService.GetAll().AsQueryable();
            var obavijesti = new ObavijestPrikazVM
            {
                Zapisi = temp.Include(i => i.KategorijaObavijesti)
                .Include(i => i.KorisnickiRacun)
                .Select
                (
                    x => new ObavijestPrikazVM.Zapis
                    {
                        Autor = x.KorisnickiRacun.FirstName + " " + x.KorisnickiRacun.LastName,
                        Datum = x.DatumIVrijeme.ToString("dd.MM.yyyy HH:mm"),
                        Naslov=x.Naslov,
                        Sadrzaj=(x.Sadrzaj.Length>30 ? x.Sadrzaj.Substring(0,30)+"...":x.Sadrzaj),
                        Id=x.Id
                    }
                ).ToList()
            };
            var podaci = obavijesti.Zapisi.ToList().AsQueryable();
            var brojObavijesti = podaci.Count();
            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                podaci = podaci.Where(x => x.Naslov.ToLower().Contains(pretrazivanje.ToLower())||x.Sadrzaj.ToLower().Contains(pretrazivanje.ToLower()));
                brojObavijesti = podaci.Count();
            }
            ViewData["Title"] = "Prikaz";
            ViewData["Controller"] = "Obavijesti";
            ViewData["Action"] = "Prikaz";
            return View(PomocneMetode.Paginacija<Zapis>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult Dodaj()
        {
            TempData["action"] = "Spasi";           
            TempData["nazivTeksta"] = "Dodaj obavijest";
            ObavijestDodajVM obavijestDodajVM = new ObavijestDodajVM();
            List<SelectListItem> kategorije = KategorijaObavijestiService.GetAll()
                .Select
                (
                    x=> new SelectListItem
                    {
                        Text=x.Naziv,
                        Value=x.Id.ToString()
                    }

                ).ToList();         
            obavijestDodajVM.KategorijaObavijesti = kategorije;
            obavijestDodajVM.Dodaj = true;
            return View("Forma", obavijestDodajVM);          
        }

        public IActionResult Uredi(int ObavijestId)
        {
            TempData["action"] = "Spasi";
            TempData["nazivTeksta"] = "Uredi obavijest";
            Obavijest obavijest;
            try
            {
                obavijest = ObavijestService.GetById(ObavijestId);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            ObavijestDodajVM obavijestDodajVM = new ObavijestDodajVM()
            {
                Id = obavijest.Id,
                DatumIVrijeme = obavijest.DatumIVrijeme,
                Dodaj = false,
                Naslov = obavijest.Naslov,
                Sadrzaj = obavijest.Sadrzaj
            };
            List<SelectListItem> kategorije = KategorijaObavijestiService.GetAll()
                .Select
                (
                    x => new SelectListItem
                    {
                        Text = x.Naziv,
                        Value = x.Id.ToString()
                    }

                ).ToList();
            obavijestDodajVM.KategorijaObavijesti = kategorije;
            return View("Forma", obavijestDodajVM);
        }


        public IActionResult Spasi(ObavijestDodajVM obavijestDodajVM)
        {
            try
            {
                if (obavijestDodajVM.Dodaj == true)
                {
                    Obavijest obavijest = new Obavijest()
                    {
                        DatumIVrijeme=DateTime.Now,
                        KategorijaObavijestiId=obavijestDodajVM.KategorijaObavijestiId,
                        Naslov=obavijestDodajVM.Naslov,
                        Sadrzaj= obavijestDodajVM.Sadrzaj,
                        KorisnickiRacunId= TempData["korisnikId"].ToString()
                    };
                    ObavijestService.Add(obavijest);
                    TempData["successAdd"] = "Uspješno ste dodali obavijest.";
                }
                else
                {
                    Obavijest obavijest = ObavijestService.GetById(obavijestDodajVM.Id);
                    obavijest.Naslov = obavijestDodajVM.Naslov;
                    obavijest.Sadrzaj = obavijestDodajVM.Sadrzaj;
                    obavijest.KategorijaObavijestiId = obavijestDodajVM.KategorijaObavijestiId;
                    ObavijestService.Update(obavijest);
                    TempData["successUpdate"] = "Uspješno ste uredili obavijest.";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            return RedirectToAction("Prikaz");
        }

        public IActionResult ObrisiView(int ObavijestId)
        {
            TempData["action"] = "Obrisi";
            TempData["nazivTeksta"] = "Potvrda";
            Obavijest obavijest;
            try
            {
                obavijest = ObavijestService.GetById(ObavijestId);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            ObavijestDodajVM obavijestDodajVM = new ObavijestDodajVM()
            {
                Id = obavijest.Id,
                DatumIVrijeme = obavijest.DatumIVrijeme,
                Dodaj = false,
                Naslov = obavijest.Naslov,
                Sadrzaj = obavijest.Sadrzaj
            };
            List<SelectListItem> kategorije = KategorijaObavijestiService.GetAll()
                .Select
                (
                    x => new SelectListItem
                    {
                        Text = x.Naziv,
                        Value = x.Id.ToString()
                    }

                ).ToList();
            obavijestDodajVM.KategorijaObavijesti = kategorije;
            return View("Forma", obavijestDodajVM);
        }

        public IActionResult Obrisi(ObavijestDodajVM obavijestDodajVM)
        {

            try
            {
                Obavijest obavijest = ObavijestService.GetById(obavijestDodajVM.Id);
                ObavijestService.Delete(obavijest);
                TempData["deleted"] = "Uspješno ste obrisali kurs.";
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            return RedirectToAction("Prikaz");
        }

        //kategorija obavijesti

        public IActionResult KategorijaObavijestiPrikaz(string pretrazivanje, int pageNumber = 1,
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var podaci = KategorijaObavijestiService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList().AsQueryable();
            var BrojKategorija = podaci.Count();
            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                podaci = podaci.Where(x => x.Text.Contains(pretrazivanje));
                BrojKategorija = podaci.Count();
            }

            TempData["naslov"] = "Kategorija obavijesti";
            TempData["urlUredi"] = "/AdministratorModul/Obavijesti/KategorijaObavijestiUredi";
            TempData["urlDodaj"] = "/AdministratorModul/Obavijesti/KategorijaObavijestiDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/Obavijesti/KategorijaObavijestiObrisiView";
            ViewData["Title"] = "KategorijaObavijestiPrikaz";
            ViewData["Controller"] = "Obavijesti";
            ViewData["Action"] = "KategorijaObavijestiPrikaz";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", 
                PomocneMetode.Paginacija<SelectListItem>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult KategorijaObavijestiUredi(int id)
        {
            Core.KategorijaObavijesti kategorijaObavijesti;
            try
            {
                kategorijaObavijesti = KategorijaObavijestiService.GetById(id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaObavijestiPrikaz");
            }

            TempData["action"] = "KategorijaObavijestiSpasi";
            TempData["controller"] = "Obavijesti";
            TempData["nazivTeksta"] = "Kategorija obavijesti";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = kategorijaObavijesti.Id.ToString(), Text = kategorijaObavijesti.Naziv });
        }

        public IActionResult KategorijaObavijestiDodaj()
        {
            TempData["action"] = "KategorijaObavijestiSpasi";
            TempData["controller"] = "Obavijesti";
            TempData["nazivTeksta"] = "Kategorija obavijesti";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = "0" });
        }

        public IActionResult KategorijaObavijestiSpasi(SelectListItem kategorijaObavijesti)
        {
            try
            {
                if (kategorijaObavijesti.Value == "0")
                {
                    KategorijaObavijestiService.Add(new Core.KategorijaObavijesti { Naziv = kategorijaObavijesti.Text });
                    TempData["successAdd"] = "Uspješno ste dodali kategoriju obavijesti.";
                }
                else
                {
                    var uredi = KategorijaObavijestiService.GetById(Int32.Parse(kategorijaObavijesti.Value));
                    uredi.Naziv = kategorijaObavijesti.Text;
                    KategorijaObavijestiService.Update(uredi);
                    TempData["successUpdate"] = "Uspješno ste uredili kategoriju obavijesti.";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaObavijestiPrikaz");
            }

            return RedirectToAction("KategorijaObavijestiPrikaz");
        }

        public IActionResult KategorijaObavijestiObrisiView(int id)
        {
            Core.KategorijaObavijesti kategorijaObavijesti;

            try
            {
                kategorijaObavijesti=KategorijaObavijestiService.GetById(id);

            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaObavijestiPrikaz");
            }
            

            TempData["action"] = "KategorijaObavijestiObrisi";
            TempData["controller"] = "Obavijesti";
            TempData["nazivTeksta"] = "Potvrda";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = kategorijaObavijesti.Id.ToString(), Text = kategorijaObavijesti.Naziv });
        }

        public IActionResult KategorijaObavijestiObrisi(SelectListItem kategorijaObavijesti)
        {
            try
            {
                KategorijaObavijestiService.Delete(KategorijaObavijestiService.GetById(Int32.Parse(kategorijaObavijesti.Value)));
                TempData["deleted"] = "Obrisali ste kategoriju obavijesti.";
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaObavijestiPrikaz");
            }

            return RedirectToAction("KategorijaObavijestiPrikaz");
        }
    }
}
