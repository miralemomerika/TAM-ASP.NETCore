using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAM.Service.Interfaces;
using TAM.Web.Helper;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class DogadjajiController : Controller
    {
        readonly ITipDogadjajaService TipDogadjajaService;

        public DogadjajiController(ITipDogadjajaService tipDogadjajaService)
        {
            TipDogadjajaService = tipDogadjajaService;
        }

        
        public IActionResult TipDogadjajaPrikaz(string pretrazivanje, int pageNumber = 1, 
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var podaci = TipDogadjajaService.GetAll().Select(x => new SelectListItem
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

            TempData["naslov"] = "Tip dogadjaja";
            TempData["urlUredi"] = "/AdministratorModul/Dogadjaji/TipDogadjajaUredi";
            TempData["urlDodaj"] = "/AdministratorModul/Dogadjaji/TipDogadjajaDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/Dogadjaji/TipDogadjajaObrisiView";
            ViewData["Title"] = "TipDogadjajaPrikaz";
            ViewData["Controller"] = "Dogadjaji";
            ViewData["Action"] = "TipDogadjajaPrikaz";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml",
                 PomocneMetode.Paginacija<SelectListItem>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult TipDogadjajaUredi(int Id)
        {
            var TipDogadjaja = TipDogadjajaService.GetById(Id);

            TempData["action"] = "TipDogadjajaSpasi";
            TempData["controller"] = "Dogadjaji";
            TempData["nazivTeksta"] = "Tip dogadjaja";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = TipDogadjaja.Id.ToString(), Text = TipDogadjaja.Naziv });
        }

        public IActionResult TipDogadjajaDodaj()
        {
            TempData["action"] = "TipDogadjajaSpasi";
            TempData["controller"] = "Dogadjaji";
            TempData["nazivTeksta"] = "Tip dogadjaja";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = "0" });
        }

        public IActionResult TipDogadjajaSpasi(SelectListItem TipDogadjaja)
        {
            if (TipDogadjaja.Value == "0")
            {
                TipDogadjajaService.Add(new Core.TipDogadjaja { Naziv = TipDogadjaja.Text });
                TempData["successAdd"] = "Uspješno ste dodali tip događaj.";
            }
            else
            {
                var uredi = TipDogadjajaService.GetById(Int32.Parse(TipDogadjaja.Value));
                uredi.Naziv = TipDogadjaja.Text;
                TipDogadjajaService.Update(uredi);
                TempData["successUpdate"] = "Uspješno ste uredili tip događaja.";
            }
            return RedirectToAction("TipDogadjajaPrikaz");
        }

        public IActionResult TipDogadjajaObrisiView(int Id)
        {
            var TipDogadjaja = TipDogadjajaService.GetById(Id);

            TempData["action"] = "Obrisi";
            TempData["controller"] = "Dogadjaji";
            TempData["nazivTeksta"] = "Potvrda";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = TipDogadjaja.Id.ToString(), Text = TipDogadjaja.Naziv });
        }

        public IActionResult Obrisi(SelectListItem TipDogadjaja)
        {
            TipDogadjajaService.Delete(TipDogadjajaService.GetById(Int32.Parse(TipDogadjaja.Value)));
            TempData["deleted"] = "Uspješno ste obrisali tip događaja.";

            return RedirectToAction("TipDogadjajaPrikaz");
        }
    }
}
