using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAM.Core;
using TAM.Service.Classes;
using TAM.Service.Interfaces;
using TAM.Web.Helper;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{

    [Area("AdministratorModul")]
    public class PolazniciController : Controller
    {
        readonly ITipPolaznikaService TipPolaznikaService;       
        readonly IExceptionHandlerService ExceptionHandlerService;       

        public PolazniciController(ITipPolaznikaService tipPolaznikaService, IExceptionHandlerService exceptionHandlerService)
        {
            TipPolaznikaService = tipPolaznikaService;
            ExceptionHandlerService = exceptionHandlerService;
        }

        public IActionResult TipPolaznikaPrikaz(string pretrazivanje, int pageNumber = 1, 
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var podaci = TipPolaznikaService.GetAll().Select(x => new SelectListItem
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

            TempData["naslov"] = "Tip polaznika";
            TempData["urlUredi"] = "/AdministratorModul/Polaznici/TipPolaznikaUredi";
            TempData["urlDodaj"] = "/AdministratorModul/Polaznici/TipPolaznikaDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/Polaznici/TipPolaznikaObrisiView";
            ViewData["Title"] = "TipPolaznikaPrikaz";
            ViewData["Controller"] = "Polaznici";
            ViewData["Action"] = "TipPolaznikaPrikaz";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", 
                PomocneMetode.Paginacija<SelectListItem>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult TipPolaznikaUredi(int Id)
        {
            TipPolaznika tipPolaznika;
            try
            {
               tipPolaznika = TipPolaznikaService.GetById(Id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("TipPolaznikaPrikaz");
            }
           

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
            try
            {
                if (tipPolaznika.Value == "0")
                {
                    TipPolaznikaService.Add(new Core.TipPolaznika { Naziv = tipPolaznika.Text });
                    TempData["successAdd"] = "Uspješno ste dodali tip polaznika.";
                }
                else
                {
                    var uredi = TipPolaznikaService.GetById(Int32.Parse(tipPolaznika.Value));
                    uredi.Naziv = tipPolaznika.Text;
                    TipPolaznikaService.Update(uredi);
                    TempData["successUpdate"] = "Uspješno ste uredili tip polaznika.";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("TipPolaznikaPrikaz");
            }
           
            return RedirectToAction("TipPolaznikaPrikaz");
        }

        public IActionResult TipPolaznikaObrisiView(int Id)
        {
            TipPolaznika tipPolaznika;
            try
            {
                tipPolaznika = TipPolaznikaService.GetById(Id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("TipPolaznikaPrikaz");
            }
            TempData["action"] = "Obrisi";
            TempData["controller"] = "Polaznici";
            TempData["nazivTeksta"] = "Potvrda";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = tipPolaznika.Id.ToString(), Text = tipPolaznika.Naziv });
        }

        public IActionResult Obrisi(SelectListItem tipPolaznika)
        {
            try
            {
                TipPolaznikaService.Delete(TipPolaznikaService.GetById(Int32.Parse(tipPolaznika.Value)));
                TempData["deleted"] = "Obrisali ste tip polaznika.";
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("TipPolaznikaPrikaz");

            }
            

            return RedirectToAction("TipPolaznikaPrikaz");
        }
    }
}
