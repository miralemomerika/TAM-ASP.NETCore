using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TAM.Service.Interfaces;

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

        public IActionResult TipDogadjajaPrikaz()
        {
            var podaci = TipDogadjajaService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            TempData["naslov"] = "Tip dogadjaja";
            TempData["urlUredi"] = "/AdministratorModul/Dogadjaji/TipDogadjajaUredi";
            TempData["urlDodaj"] = "/AdministratorModul/Dogadjaji/TipDogadjajaDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/Dogadjaji/TipDogadjajaObrisi";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", podaci);
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
            }
            else
            {
                var uredi = TipDogadjajaService.GetById(Int32.Parse(TipDogadjaja.Value));
                uredi.Naziv = TipDogadjaja.Text;
                TipDogadjajaService.Update(uredi);
            }
            return RedirectToAction("TipDogadjajaPrikaz");
        }

        public IActionResult TipDogadjajaObrisi(int Id)
        {
            TipDogadjajaService.Delete(TipDogadjajaService.GetById(Id));
            return RedirectToAction("TipDogadjajaPrikaz");
        }
    }
}
