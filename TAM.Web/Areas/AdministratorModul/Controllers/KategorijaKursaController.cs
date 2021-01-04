using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TAM.Service.Interfaces;
using TAM.Service.Classes;
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class KategorijaKursaController : Controller
    {

        readonly IKategorijaKursaService KategorijaKursaService;
       
        public KategorijaKursaController (IKategorijaKursaService kategorijaKursaService)
        {
            KategorijaKursaService = kategorijaKursaService;
        }
       
        public IActionResult KategorijaKursaPrikaz(string pretrazivanje, int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var podaci = KategorijaKursaService.GetAll().Select(x => new SelectListItem
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
            podaci = podaci.Skip(ExcludeRecords).Take(pageSize);
            var rezultat = new PagedResult<SelectListItem>
            {
                Data = podaci.AsNoTracking().ToList(),
                TotalItems = BrojKategorija,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            TempData["naslov"] = "Kategorija kursa";
            TempData["urlUredi"] = "/AdministratorModul/KategorijaKursa/KategorijaKursaUredi";
            TempData["urlDodaj"] = "/AdministratorModul/KategorijaKursa/KategorijaKursaDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/KategorijaKursa/KategorijaKursaObrisi";
            ViewData["Title"] = "KategorijaKursaPrikaz";
            ViewData["Controller"] = "KategorijaKursa";
            ViewData["Action"] = "KategorijaKursaPrikaz";
            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", rezultat);
        }

        public IActionResult KategorijaKursaUredi(int id)
        {
            var kategorijaKursa = KategorijaKursaService.GetById(id);

            TempData["action"] = "KategorijaKursaSpasi";
            TempData["controller"] = "KategorijaKursa";
            TempData["nazivTeksta"] = "Kategorija kursa";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = kategorijaKursa.Id.ToString(), Text = kategorijaKursa.Naziv });
        }

        public IActionResult KategorijaKursaDodaj()
        {
            TempData["action"] = "KategorijaKursaSpasi";
            TempData["controller"] = "KategorijaKursa";
            TempData["nazivTeksta"] = "Kategorija kursa";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = "0" });
        }

        public IActionResult KategorijaKursaSpasi(SelectListItem kategorijaKursa)
        {
            if(kategorijaKursa.Value == "0")
            {
                KategorijaKursaService.Add(new Core.KategorijaKursa { Naziv = kategorijaKursa.Text });
            }
            else
            {
                var uredi = KategorijaKursaService.GetById(Int32.Parse(kategorijaKursa.Value));
                uredi.Naziv = kategorijaKursa.Text;
                KategorijaKursaService.Update(uredi);
            }

            return RedirectToAction("KategorijaKursaPrikaz");
        }

        public IActionResult KategorijaKursaObrisi(int id)
        {
            KategorijaKursaService.Delete(KategorijaKursaService.GetById(id));

            return RedirectToAction("KategorijaKursaPrikaz");
        }
    }
}
