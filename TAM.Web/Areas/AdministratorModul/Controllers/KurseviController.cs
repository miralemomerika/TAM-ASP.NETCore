﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAM.Core;
using TAM.Service.Classes;
using TAM.Service.Interfaces;
using TAM.ViewModels;
using TAM.Web.Helper;
using static TAM.ViewModels.KursPrikazVM;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class KurseviController : Controller
    {
        readonly IKursService KursService;
        readonly IKategorijaKursaService KategorijaKursaService;
        readonly IExceptionHandlerService ExceptionHandlerService;
        public KurseviController(IKursService kursService, IExceptionHandlerService exceptionHandlerService,
            IKategorijaKursaService kategorijaKursaService)
        {
            KursService = kursService;
            ExceptionHandlerService = exceptionHandlerService;
            KategorijaKursaService = kategorijaKursaService;
        }
        public IActionResult Prikaz(string pretrazivanje, int pageNumber = 1,
            int pageSize = 5)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;              
            var temp = KursService.GetAll().AsQueryable();
            var kursPrikaz = new KursPrikazVM
            {
                Zapisi = temp.Include(i=>i.KategorijaKursa).Select
                (
                    i=> new KursPrikazVM.Zapis
                    {
                        BrojCasova=i.BrojCasova,
                        Cijena=i.Cijena,
                        Id=i.Id,
                        KategorijaKursa=i.KategorijaKursa.Naziv,
                        Naziv=i.Naziv
                    }
                ).ToList()
            };
            var podaci = kursPrikaz.Zapisi.ToList().AsQueryable();
            var BrojKategorija = podaci.Count();
            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                podaci = podaci.Where(x => x.Naziv.Contains(pretrazivanje));
                BrojKategorija = podaci.Count();
            }
            ViewData["Title"] = "Prikaz";
            ViewData["Controller"] = "Kursevi";
            ViewData["Action"] = "Prikaz";
            return View(PomocneMetode.Paginacija<Zapis>(pretrazivanje, podaci, pageNumber, pageSize));
        }

        public IActionResult Dodaj()
        {
            TempData["action"] = "Spasi";
            TempData["nazivTeksta"] = "Dodaj kurs";
            KursDodajVM kursDodajVM = new KursDodajVM();
            List<SelectListItem> kategorije = KategorijaKursaService.GetAll()
                .Select
                (
                    x => new SelectListItem 
                    { 
                        Text=x.Naziv,
                        Value=x.Id.ToString()
                    }
                ).ToList();
            kursDodajVM.KategorijaKursa = kategorije;
            kursDodajVM.Dodaj = true;
            return View("Forma", kursDodajVM);
        }

        public IActionResult Uredi(int Id)
        {
            TempData["action"] = "Spasi";
            TempData["nazivTeksta"] = "Uredi kurs";
            Kurs kurs;
            try
            {
                kurs = KursService.GetById(Id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            KursDodajVM kursDodajVM = new KursDodajVM()
            {
                BrojCasova = kurs.BrojCasova,
                Cijena = kurs.Cijena,
                Naziv = kurs.Naziv,
                KategorijaKursaId = kurs.KategorijaKursaId,
                Id = kurs.Id,
                Dodaj = false
            };
            List<SelectListItem> kategorije = KategorijaKursaService.GetAll()
                .Select
                (
                    x => new SelectListItem
                    {
                        Text = x.Naziv,
                        Value = x.Id.ToString()
                    }
                ).ToList();
            kursDodajVM.KategorijaKursa = kategorije;           
            return View("Forma", kursDodajVM);
        }
        public IActionResult ObrisiView(int Id)
        {
            TempData["action"] = "Obrisi";
            TempData["nazivTeksta"] = "Potvrda";
            Kurs kurs;
            try
            {
                kurs = KursService.GetById(Id);
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            KursDodajVM kursDodajVM = new KursDodajVM()
            {
                BrojCasova = kurs.BrojCasova,
                Cijena = kurs.Cijena,
                Naziv = kurs.Naziv,
                KategorijaKursaId = kurs.KategorijaKursaId,
                Id = kurs.Id,
                Dodaj=false
            };
            List<SelectListItem> kategorije = KategorijaKursaService.GetAll()
                .Select
                (
                    x => new SelectListItem
                    {
                        Text = x.Naziv,
                        Value = x.Id.ToString()
                    }
                ).ToList();
            kursDodajVM.KategorijaKursa = kategorije;
            return View("Forma", kursDodajVM);
        }

        public IActionResult Obrisi(KursDodajVM kursDodajVM)
        {
           
            try
            {
                Kurs kurs = KursService.GetById(kursDodajVM.Id);                
                KursService.Delete(kurs);
                TempData["deleted"] = "Uspješno ste obrisali kurs.";
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            return RedirectToAction("Prikaz");
        }
        public IActionResult Spasi(KursDodajVM kursDodajVM)
        {            
            try
            {
                if (kursDodajVM.Dodaj == true)
                {
                    Kurs kurs = new Kurs
                    {
                        BrojCasova = kursDodajVM.BrojCasova,
                        Naziv = kursDodajVM.Naziv,
                        Cijena = kursDodajVM.Cijena,
                        KategorijaKursaId = kursDodajVM.KategorijaKursaId
                    };
                    KursService.Add(kurs);
                    TempData["successAdd"] = "Uspješno ste dodali kurs.";
                }
                else
                {
                    Kurs kurs = KursService.GetById(kursDodajVM.Id);
                    kurs.Cijena = kursDodajVM.Cijena;
                    kurs.BrojCasova = kursDodajVM.BrojCasova;
                    kurs.Naziv = kursDodajVM.Naziv;
                    kurs.KategorijaKursaId = kursDodajVM.KategorijaKursaId;
                    KursService.Update(kurs);
                    TempData["successUpdate"] = "Uspješno ste uredili kurs.";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                TempData["exception"] = "Operaciju nije moguce izvrsiti!";
                return RedirectToAction("Prikaz");
            }
            return RedirectToAction("Prikaz");
        }
    }
}