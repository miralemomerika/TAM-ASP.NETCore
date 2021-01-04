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
            return View("Forma", new Prostorija { Id = 0 });
        }

        public IActionResult Uredi(int Id)
        {
            return View("Forma", ProstorijaService.GetById(Id));
        }

        public IActionResult Obrisi(int Id)
        {
            ProstorijaService.Delete(ProstorijaService.GetById(Id));
            return RedirectToAction("Prikaz");
        }

        public IActionResult Spasi(Prostorija prostorija)
        {
            if(prostorija.Id == 0)
            {
                ProstorijaService.Add(prostorija);
            }
            else
            {
                ProstorijaService.Update(prostorija);
            }
            return RedirectToAction("Prikaz");
        }
    }
}
