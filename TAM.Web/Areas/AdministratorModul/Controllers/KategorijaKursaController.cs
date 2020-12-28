using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TAM.Service.Interfaces;
using TAM.Service.Classes;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class KategorijaKursaController : Controller
    {

        readonly IKategorijaKursaService KategorijaKursaService;
       
        public KategorijaKursaController (IKategorijaKursaService kategorijaKursaService)
        {
            KategorijaKursaService = kategorijaKursaService;
        }

        public IActionResult KategorijaKursaPrikaz()
        {
            var podaci = KategorijaKursaService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            TempData["naslov"] = "Kategorija kursa";
            TempData["urlUredi"] = "/AdministratorModul/KategorijaKursa/KategorijaKursaUredi";
            TempData["urlDodaj"] = "/AdministratorModul/KategorijaKursa/KategorijaKursaDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/KategorijaKursa/KategorijaKursaObrisi";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", podaci);

        }

        public IActionResult KategorijaKursaUredi(int id)
        {
            var kategorijaKursa = KategorijaKursaService.GetById(id);

            TempData["action"] = "KategorijaKursaSpasi";
            TempData["controller"] = "KategorijaKursa";
            TempData["nazivTeksta"] = "Kategorija kursa";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = kategorijaKursa.Id.ToString(), Text = kategorijaKursa.Naziv });
        }

        public IActionResult KategorijaKursaDodaj()
        {
            TempData["action"] = "KategorijaKursaSpasi";
            TempData["controller"] = "KategorijaKursa";
            TempData["nazivTeksta"] = "Kategorija kursa";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = "0" });
        }

        public IActionResult KategorijaKursaSpasi(SelectListItem kategorijaKursa)
        {
            if(kategorijaKursa.Value == "0")
            {
                KategorijaKursaService.Add(new Core.KategorijaKursa { Naziv = kategorijaKursa.Text });
            }
            else
            {
                var uredi = KategorijaKursaService.GetById(Int32.Parse(kategorijaKursa.Value));
                uredi.Naziv = kategorijaKursa.Text;
                KategorijaKursaService.Update(uredi);
            }

            return RedirectToAction("KategorijaKursaPrikaz");
        }

        public IActionResult KategorijaKursaObrisi(int id)
        {
            KategorijaKursaService.Delete(KategorijaKursaService.GetById(id));

            return RedirectToAction("KategorijaKursaPrikaz");
        }
    }
}
