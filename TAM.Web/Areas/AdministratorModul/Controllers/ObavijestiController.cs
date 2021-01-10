using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Interfaces;
using TAM.Service.Classes;
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore;
using TAM.Web.Helper;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class ObavijestiController : Controller
    {

        readonly IKategorijaObavijestiService KategorijaObavijestiService;
        readonly IExceptionHandlerService ExceptionHandlerService;
        public ObavijestiController(IKategorijaObavijestiService kategorijaObavijestiService, IExceptionHandlerService exceptionHandlerService)
        {
            KategorijaObavijestiService = kategorijaObavijestiService;
            ExceptionHandlerService = exceptionHandlerService;
        }
     
        public IActionResult KategorijaObavijestiPrikaz(string pretrazivanje, int pageNumber = 1,
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var podaci = KategorijaObavijestiService.GetAll().Select(x => new SelectListItem
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

            TempData["naslov"] = "Kategorija obavijesti";
            TempData["urlUredi"] = "/AdministratorModul/Obavijesti/KategorijaObavijestiUredi";
            TempData["urlDodaj"] = "/AdministratorModul/Obavijesti/KategorijaObavijestiDodaj";
            TempData["urlObrisi"] = "/AdministratorModul/Obavijesti/KategorijaObavijestiObrisiView";
            ViewData["Title"] = "KategorijaObavijestiPrikaz";
            ViewData["Controller"] = "Obavijesti";
            ViewData["Action"] = "KategorijaObavijestiPrikaz";

            return View("/Areas/Shared/SelectListItemPrikaz.cshtml", 
                PomocneMetode.Paginacija<SelectListItem>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult KategorijaObavijestiUredi(int id)
        {
            Core.KategorijaObavijesti kategorijaObavijesti;
            try
            {
                kategorijaObavijesti = KategorijaObavijestiService.GetById(id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaObavijestiPrikaz");
            }

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
            try
            {
                if (kategorijaObavijesti.Value == "0")
                {
                    KategorijaObavijestiService.Add(new Core.KategorijaObavijesti { Naziv = kategorijaObavijesti.Text });
                    TempData["successAdd"] = "Uspješno ste dodali kategoriju obavijesti.";
                }
                else
                {
                    var uredi = KategorijaObavijestiService.GetById(Int32.Parse(kategorijaObavijesti.Value));
                    uredi.Naziv = kategorijaObavijesti.Text;
                    KategorijaObavijestiService.Update(uredi);
                    TempData["successUpdate"] = "Uspješno ste uredili kategoriju obavijesti.";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaObavijestiPrikaz");
            }

            return RedirectToAction("KategorijaObavijestiPrikaz");
        }

        public IActionResult KategorijaObavijestiObrisiView(int id)
        {
            Core.KategorijaObavijesti kategorijaObavijesti;

            try
            {
                kategorijaObavijesti=KategorijaObavijestiService.GetById(id);

            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaObavijestiPrikaz");
            }
            

            TempData["action"] = "Obrisi";
            TempData["controller"] = "Obavijesti";
            TempData["nazivTeksta"] = "Potvrda";

            return View("/Areas/Shared/SelectListItemForma.cshtml",
                new SelectListItem { Value = kategorijaObavijesti.Id.ToString(), Text = kategorijaObavijesti.Naziv });
        }

        public IActionResult Obrisi(SelectListItem kategorijaObavijesti)
        {
            try
            {
                KategorijaObavijestiService.Delete(KategorijaObavijestiService.GetById(Int32.Parse(kategorijaObavijesti.Value)));
                TempData["deleted"] = "Obrisali ste kategoriju obavijesti.";
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("KategorijaObavijestiPrikaz");
            }

            return RedirectToAction("KategorijaObavijestiPrikaz");
        }
    }
}
