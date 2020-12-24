using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Classes;
using TAM.Service.Interfaces;
using TAM.ViewModels;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class UplateController : Controller
    {
        ISvrhaUplateService SvrhaUplateService;

        public UplateController(ISvrhaUplateService svrhaUplateService)
        {
            SvrhaUplateService = svrhaUplateService;
        }

        public IActionResult SvrhaUplatePrikaz()
        {
            var podaci = SvrhaUplateService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Svrha
            }).ToList();

            TempData["naslov"] = "Svrha uplate";
            TempData["urlUredi"] = "/neki/url";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", podaci);
        }
    }
}