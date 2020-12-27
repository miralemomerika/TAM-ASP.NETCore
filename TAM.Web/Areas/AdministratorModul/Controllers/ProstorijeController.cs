using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Prikaz()
        {
            var podaci = ProstorijaService.GetAll().ToList();

            return View(podaci);
        }
    }
}
