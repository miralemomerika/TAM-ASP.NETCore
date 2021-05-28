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
using TAM.Service.Classes;
using TAM.Service.Interfaces;
using TAM.Web.Helper;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    [Authorize(Roles = "Portir")]
    public class UplateController : Controller
    {
        readonly ISvrhaUplateService SvrhaUplateService;
        readonly IExceptionHandlerService ExceptionHandlerService;

        public UplateController(ISvrhaUplateService svrhaUplateService, IExceptionHandlerService exceptionHandlerService)
        {
            SvrhaUplateService = svrhaUplateService;
            ExceptionHandlerService = exceptionHandlerService;
        }

        public IActionResult SvrhaUplatePrikaz(string pretrazivanje, int pageNumber = 1, 
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var podaci = SvrhaUplateService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Svrha
            }).ToList().AsQueryable();
            var BrojKategorija = podaci.Count();
            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                podaci = podaci.Where(x => x.Text.Contains(pretrazivanje));
                BrojKategorija = podaci.Count();
            }

            TempData["naslov"] = "Svrha uplate";
            TempData["urlUredi"] = "/AdministratorModul/Uplate/SvrhaUplateUredi";
            TempData["urlDodaj"] = "/AdministratorModul/Uplate/SvrhaUplateDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/Uplate/SvrhaUplateObrisiView";
            ViewData["Title"] = "SvrhaUplatePrikaz";
            ViewData["Controller"] = "Uplate";
            ViewData["Action"] = "SvrhaUplatePrikaz";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", 
                PomocneMetode.Paginacija<SelectListItem>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult SvrhaUplateUredi(int Id)
        {
            SvrhaUplate svrha;
            try
            {
                svrha = SvrhaUplateService.GetById(Id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("SvrhaUplatePrikaz");
            }
            

            TempData["action"] = "SvrhaUplateSpasi";
            TempData["controller"] = "Uplate";
            TempData["nazivTeksta"] = "Svrha uplate";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = svrha.Id.ToString(), Text = svrha.Svrha });
        }

        public IActionResult SvrhaUplateDodaj()
        {
            TempData["action"] = "SvrhaUplateSpasi";
            TempData["controller"] = "Uplate";
            TempData["nazivTeksta"] = "Svrha uplate";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = "0" });
        }

        public IActionResult SvrhaUplateSpasi(SelectListItem svrha)
        {
            try
            {
                if (svrha.Value == "0")
                {
                    SvrhaUplateService.Add(new Core.SvrhaUplate { Svrha = svrha.Text });
                    TempData["successAdd"] = "Uspješno ste dodali kategoriju.";
                }
                else
                {
                    var uredi = SvrhaUplateService.GetById(Int32.Parse(svrha.Value));
                    uredi.Svrha = svrha.Text;
                    SvrhaUplateService.Update(uredi);
                    TempData["successUpdate"] = "Uspješno ste uredili kategoriju.";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("SvrhaUplatePrikaz");
            }
            return RedirectToAction("SvrhaUplatePrikaz");
        }

        public IActionResult SvrhaUplateObrisiView(int Id)
        {
            SvrhaUplate svrha;
            try
            {
                svrha = SvrhaUplateService.GetById(Id);

            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("SvrhaUplatePrikaz");
            }
            TempData["action"] = "Obrisi";
            TempData["controller"] = "Uplate";
            TempData["nazivTeksta"] = "Potvrda";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = svrha.Id.ToString(), Text = svrha.Svrha });
        }

        public IActionResult Obrisi(SelectListItem svrha)
        {
            try
            {
                SvrhaUplateService.Delete(SvrhaUplateService.GetById(Int32.Parse(svrha.Value)));
                TempData["deleted"] = "Uspješno ste obrisali svrhu uplate.";
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("SvrhaUplatePrikaz");
            }

            return RedirectToAction("SvrhaUplatePrikaz");
        }
    }
}