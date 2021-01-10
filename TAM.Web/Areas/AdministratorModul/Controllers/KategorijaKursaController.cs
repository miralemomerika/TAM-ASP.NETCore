using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TAM.Service.Interfaces;
using TAM.Service.Classes;
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore;
using TAM.Web.Helper;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class KategorijaKursaController : Controller
    {

        readonly IKategorijaKursaService KategorijaKursaService;
        readonly IExceptionHandlerService ExceptionHandlerService;
        public KategorijaKursaController (IKategorijaKursaService kategorijaKursaService, IExceptionHandlerService exceptionHandlerService)
        {
            KategorijaKursaService = kategorijaKursaService;
            ExceptionHandlerService = exceptionHandlerService;
        }
       
        public IActionResult KategorijaKursaPrikaz(string pretrazivanje, int pageNumber = 1, 
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var podaci = KategorijaKursaService.GetAll().Select(x => new SelectListItem
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

            TempData["naslov"] = "Kategorija kursa";
            TempData["urlUredi"] = "/AdministratorModul/KategorijaKursa/KategorijaKursaUredi";
            TempData["urlDodaj"] = "/AdministratorModul/KategorijaKursa/KategorijaKursaDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/KategorijaKursa/KategorijaKursaObrisiView";
            ViewData["Title"] = "KategorijaKursaPrikaz";
            ViewData["Controller"] = "KategorijaKursa";
            ViewData["Action"] = "KategorijaKursaPrikaz";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml",
                 PomocneMetode.Paginacija<SelectListItem>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult KategorijaKursaUredi(int id)
        {
            Core.KategorijaKursa kategorijaKursa;
            try
            {
                kategorijaKursa = KategorijaKursaService.GetById(id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaKursaPrikaz");
            }

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
            try
            {
                if (kategorijaKursa.Value == "0")
                {
                    KategorijaKursaService.Add(new Core.KategorijaKursa { Naziv = kategorijaKursa.Text });
                    TempData["successAdd"] = "Uspješno ste dodali kategoriju.";
                }
                else
                {
                    var uredi = KategorijaKursaService.GetById(Int32.Parse(kategorijaKursa.Value));
                    uredi.Naziv = kategorijaKursa.Text;
                    KategorijaKursaService.Update(uredi);
                    TempData["successUpdate"] = "Uspješno ste uredili kategoriju.";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaKursaPrikaz");
            }

            return RedirectToAction("KategorijaKursaPrikaz");
        }

        public IActionResult KategorijaKursaObrisiView(int id)
        {
            Core.KategorijaKursa kategorijaKursa;
            try
            {
                kategorijaKursa = KategorijaKursaService.GetById(id);

            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaKursaPrikaz");
            }
            TempData["action"] = "Obrisi";
            TempData["controller"] = "KategorijaKursa";
            TempData["nazivTeksta"] = "Potvrda";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = kategorijaKursa.Id.ToString(), Text = kategorijaKursa.Naziv });
        }

        public IActionResult Obrisi(SelectListItem kategorijaKursa)
        {
            try
            {
                KategorijaKursaService.Delete(KategorijaKursaService.GetById(Int32.Parse(kategorijaKursa.Value)));
                TempData["deleted"] = "Obrisali ste kategoriju.";

            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaKursaPrikaz");
            }
            return RedirectToAction("KategorijaKursaPrikaz");
        }
    }
}
