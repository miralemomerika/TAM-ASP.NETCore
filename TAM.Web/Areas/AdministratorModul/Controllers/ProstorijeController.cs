using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Core;
using TAM.Service.Interfaces;
using TAM.Web.Helper;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    [Authorize(Roles = "Portir")]
    public class ProstorijeController : Controller
    {
        readonly IProstorijaService ProstorijaService;
        readonly IExceptionHandlerService ExceptionHandlerService;
        public ProstorijeController(IProstorijaService prostorijaService, IExceptionHandlerService exceptionHandlerService)
        {
            ProstorijaService = prostorijaService;
            ExceptionHandlerService = exceptionHandlerService;
        }

        public IActionResult Prikaz(string pretrazivanje, int pageNumber = 1, 
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var podaci = ProstorijaService.GetAll().ToList().AsQueryable();
            var BrojKategorija = podaci.Count();
            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                podaci = podaci.Where(x => x.Naziv.Contains(pretrazivanje));
                BrojKategorija = podaci.Count();
            }

            ViewData["Title"] = "Prikaz";
            ViewData["Controller"] = "Prostorije";
            ViewData["Action"] = "Prikaz";       

            return View(PomocneMetode.Paginacija<Prostorija>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult Dodaj()
        {
            TempData["action"] = "Spasi";
            TempData["nazivTeksta"] = "Dodaj prostoriju";

            return View("Forma", new Prostorija { Id = 0 });
        }

        public IActionResult Uredi(int Id)
        {
            TempData["action"] = "Spasi";
            TempData["nazivTeksta"] = "Uredi prostoriju";
            Prostorija prostorija;
            try
            {
                prostorija = ProstorijaService.GetById(Id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            return View("Forma", prostorija);
        }

        public IActionResult ObrisiView(int Id)
        {
            TempData["action"] = "Obrisi";
            TempData["nazivTeksta"] = "Potvrda";
            Prostorija prostorija;
            try
            {
                prostorija = ProstorijaService.GetById(Id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            return View("Forma", prostorija);
        }

        public IActionResult Obrisi(Prostorija prostorija)
        {
            try
            {
                var obrisi = ProstorijaService.GetById(prostorija.Id);
                ProstorijaService.Delete(obrisi);
                TempData["deleted"] = "Uspješno ste obrisali prostoriju.";

            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            return RedirectToAction("Prikaz");
        }

        public IActionResult Spasi(Prostorija prostorija)
        {
            try
            {
                if (prostorija.Id == 0)
                {
                    ProstorijaService.Add(prostorija);
                    TempData["successAdd"] = "Uspješno ste dodali prostoriju.";
                }
                else
                {
                    ProstorijaService.Update(prostorija);
                    TempData["successUpdate"] = "Uspješno ste uredili prostoriju.";
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
    }
}
