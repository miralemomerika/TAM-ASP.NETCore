using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Prikaz()
        {
            return View(ProstorijaService.GetAll().ToList());
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
