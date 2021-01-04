using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Classes;
using TAM.Service.Interfaces;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class UplateController : Controller
    {
        readonly ISvrhaUplateService SvrhaUplateService;

        public UplateController(ISvrhaUplateService svrhaUplateService)
        {
            SvrhaUplateService = svrhaUplateService;
        }

        public IActionResult SvrhaUplatePrikaz(string pretrazivanje, int pageNumber = 1, int pageSize = 3)
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
            podaci = podaci.Skip(ExcludeRecords).Take(pageSize);
            var rezultat = new PagedResult<SelectListItem>
            {
                Data = podaci.AsNoTracking().ToList(),
                TotalItems = BrojKategorija,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            TempData["naslov"] = "Svrha uplate";
            TempData["urlUredi"] = "/AdministratorModul/Uplate/SvrhaUplateUredi";
            TempData["urlDodaj"] = "/AdministratorModul/Uplate/SvrhaUplateDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/Uplate/SvrhaUplateObrisi";
            ViewData["Title"] = "SvrhaUplatePrikaz";
            ViewData["Controller"] = "Uplate";
            ViewData["Action"] = "SvrhaUplatePrikaz";
            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", rezultat);
        }

        public IActionResult SvrhaUplateUredi(int Id)
        {
            var svrha = SvrhaUplateService.GetById(Id);

            TempData["action"] = "SvrhaUplateSpasi";
            TempData["controller"] = "Uplate";
            TempData["nazivTeksta"] = "Svrha uplate";
            return View("/Areas/Shared/SelectListItemForma.cshtml", 
                new SelectListItem { Value=svrha.Id.ToString(), Text=svrha.Svrha });
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
            if (svrha.Value == "0")
            {
                SvrhaUplateService.Add(new Core.SvrhaUplate { Svrha = svrha.Text });
            }
            else
            {
                var uredi = SvrhaUplateService.GetById(Int32.Parse(svrha.Value));
                uredi.Svrha = svrha.Text;
                SvrhaUplateService.Update(uredi);
            }
            return RedirectToAction("SvrhaUplatePrikaz");
        }

        public IActionResult SvrhaUplateObrisi(int Id)
        {
            SvrhaUplateService.Delete(SvrhaUplateService.GetById(Id));
            return RedirectToAction("SvrhaUplatePrikaz");
        }


    }
}