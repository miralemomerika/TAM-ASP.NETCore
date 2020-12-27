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
    public class PolazniciController : Controller
    {
        readonly ITipPolaznikaService TipPolaznikaService;

        public PolazniciController(ITipPolaznikaService tipPolaznikaService)
        {
            TipPolaznikaService = tipPolaznikaService;
        }

        public IActionResult TipPolaznikaPrikaz()
        {
            var podaci = TipPolaznikaService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            TempData["naslov"] = "Tip polaznika";
            TempData["urlUredi"] = "/AdministratorModul/Polaznici/TipPolaznikaUredi";
            TempData["urlDodaj"] = "/AdministratorModul/Polaznici/TipPolaznikaDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/Polaznici/TipPolaznikaObrisi";
   
            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", podaci);
        }

        public IActionResult TipPolaznikaUredi(int Id)
        {
            var tipPolaznika = TipPolaznikaService.GetById(Id);

            TempData["action"] = "TipPolaznikaSpasi";
            TempData["controller"] = "Polaznici";
            TempData["nazivTeksta"] = "Tip polaznika";
            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = tipPolaznika.Id.ToString(), Text = tipPolaznika.Naziv });
        }

        public IActionResult TipPolaznikaDodaj()
        {
            TempData["action"] = "TipPolaznikaSpasi";
            TempData["controller"] = "Polaznici";
            TempData["nazivTeksta"] = "Tip polaznika";
            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = "0" });
        }

        public IActionResult TipPolaznikaSpasi(SelectListItem tipPolaznika)
        {
            if (tipPolaznika.Value == "0")
            {
                TipPolaznikaService.Add(new Core.TipPolaznika { Naziv = tipPolaznika.Text });
            }
            else
            {
                var uredi = TipPolaznikaService.GetById(Int32.Parse(tipPolaznika.Value));
                uredi.Naziv = tipPolaznika.Text;
                TipPolaznikaService.Update(uredi);
            }
            return RedirectToAction("TipPolaznikaPrikaz");
        }

        public IActionResult TipPolaznikaObrisi(int Id)
        {
            TipPolaznikaService.Delete(TipPolaznikaService.GetById(Id));
            return RedirectToAction("TipPolaznikaPrikaz");
        }
    }
}
