using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Core;
using TAM.Service.Interfaces;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class ProstorijeController : Controller
    {
        readonly IProstorijaService ProstorijaService;
        public ProstorijeController(IProstorijaService prostorijaService)
        {
            ProstorijaService = prostorijaService;
        }

        public IActionResult Prikaz(string pretrazivanje, int pageNumber = 1, int pageSize = 3)
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
            podaci = podaci.Skip(ExcludeRecords).Take(pageSize);
            var rezultat = new PagedResult<Prostorija>
            {
                Data = podaci.AsNoTracking().ToList(),
                TotalItems = BrojKategorija,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            ViewData["Title"] = "Prikaz";
            ViewData["Controller"] = "Prostorije";
            ViewData["Action"] = "Prikaz";
            return View(rezultat);
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

            return View("Forma", ProstorijaService.GetById(Id));
        }

        public IActionResult ObrisiView(int Id)
        {
            TempData["action"] = "Obrisi";
            TempData["nazivTeksta"] = "Potvrda";

            return View("Forma", ProstorijaService.GetById(Id));
        }

        public IActionResult Obrisi(Prostorija prostorija)
        {
            ProstorijaService.Delete(prostorija);
            TempData["deleted"] = "Obrisali ste prostoriju.";

            return RedirectToAction("Prikaz");
        }

        public IActionResult Spasi(Prostorija prostorija)
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
            return RedirectToAction("Prikaz");
        }
    }
}
