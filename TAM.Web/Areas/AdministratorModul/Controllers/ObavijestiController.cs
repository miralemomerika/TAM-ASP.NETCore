using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Interfaces;
using TAM.Service.Classes;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class ObavijestiController : Controller
    {

        readonly IKategorijaObavijestiService KategorijaObavijestiService;

        public ObavijestiController(IKategorijaObavijestiService kategorijaObavijestiService)
        {
            KategorijaObavijestiService = kategorijaObavijestiService;
        }

        public IActionResult KategorijaObavijestiPrikaz()
        {
            var podaci = KategorijaObavijestiService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            TempData["naslov"] = "Kategorija obavijesti";
            TempData["urlUredi"] = "/AdministratorModul/Obavijesti/KategorijaObavijestiUredi";
            TempData["urlDodaj"] = "/AdministratorModul/Obavijesti/KategorijaObavijestiDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/Obavijesti/KategorijaObavijestiObrisi";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", podaci);

        }

        public IActionResult KategorijaObavijestiUredi(int id)
        {
            var kategorijaObavijesti = KategorijaObavijestiService.GetById(id);

            TempData["action"] = "KategorijaObavijestiSpasi";
            TempData["controller"] = "Obavijesti";
            TempData["nazivTeksta"] = "Kategorija obavijesti";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = kategorijaObavijesti.Id.ToString(), Text = kategorijaObavijesti.Naziv });
        }

        public IActionResult KategorijaObavijestiDodaj()
        {
            TempData["action"] = "KategorijaObavijestiSpasi";
            TempData["controller"] = "Obavijesti";
            TempData["nazivTeksta"] = "Kategorija obavijesti";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = "0" });
        }

        public IActionResult KategorijaObavijestiSpasi(SelectListItem kategorijaObavijesti)
        {
            if (kategorijaObavijesti.Value == "0")
            {
                KategorijaObavijestiService.Add(new Core.KategorijaObavijesti { Naziv = kategorijaObavijesti.Text });
            }
            else
            {
                var uredi = KategorijaObavijestiService.GetById(Int32.Parse(kategorijaObavijesti.Value));
                uredi.Naziv = kategorijaObavijesti.Text;
                KategorijaObavijestiService.Update(uredi);
            }

            return RedirectToAction("KategorijaObavijestiPrikaz");
        }

        public IActionResult KategorijaObavijestiObrisi(int id)
        {
            KategorijaObavijestiService.Delete(KategorijaObavijestiService.GetById(id));

            return RedirectToAction("KategorijaObavijestiPrikaz");
        }
    }
}
