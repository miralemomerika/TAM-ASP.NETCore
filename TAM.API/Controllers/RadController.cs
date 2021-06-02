using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;
using TAM.Web.Helper;
using TAM.API.Dto;
using System.IO;
using System.Net.Http.Headers;

namespace TAM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RadController : ControllerBase
    {
        readonly IRadService _radService;
        readonly IPohadjanjeService _pohadjanjeService;
        readonly IIspitService _ispitService;
        readonly IExceptionHandlerService _exceptionHandler;
        readonly ApplicationDbContext _context;
        readonly KorisnickiRacun _korisnickiRacun;

        public RadController(IRadService radService,
                             IPohadjanjeService pohadjanjeService,
                             IIspitService ispitService,
                             IExceptionHandlerService exceptionHandler,
                             IHttpContextAccessor httpContextAccessor,
                             ApplicationDbContext context)
        {
            _radService = radService;
            _pohadjanjeService = pohadjanjeService;
            _ispitService = ispitService;
            _exceptionHandler = exceptionHandler;
            _context = context;
            if(httpContextAccessor.HttpContext.User.Identity.Name != null)
            {
                _korisnickiRacun = _context.Users.First(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getispitipolaznika")]
        public async Task<IActionResult> GetAktivniIspitiPolaznika()
        {
            try
            {
                var pohadjanja = _pohadjanjeService.GetAll().AsQueryable();
                //sva pohadjanja/kursevi logiranog polaznika
                var kursevi = pohadjanja.Where(x => x.PolaznikId == _korisnickiRacun.Id && x.Pohadja == true)
                    .Select(x => x.OrganizacijaKursaId)
                    .ToList();
                //var kursevi = pohadjanja.Where(x => x.PolaznikId == "f7e3373e-4971-4803-8169-b1c38be4062f" && x.Pohadja == true)
                //    .Select(x => x.OrganizacijaKursaId)
                //    .ToList();

                var ispiti = _ispitService.GetAll().AsQueryable();
                //ispiti za trazenog polaznika
                var ispitiPolaznika = ispiti.Where(x => kursevi.Any(y => y == x.OrganizacijaKursaId)).ToList();

                var model = ispitiPolaznika.Where(x => x.VrijemePocetka < DateTime.Now && x.VrijemeZavrsetka > DateTime.Now)
                    .Select(x => new IspitRadDto
                {
                    IspitId = x.Id,
                    Naslov = x.Naslov,
                    Opis = x.Opis,
                    VrijemePocetka = x.VrijemePocetka,
                    VrijemeZavrsetka = x.VrijemeZavrsetka,
                    UrlIspita = x.UrlDokumenta,
                    OrganizacijaKursa = x.OrganizacijaKursa.Kurs.Naziv,
                    OrganizacijaKursaId = x.OrganizacijaKursaId
                }).ToList();

                if (model == null)
                    return BadRequest("Ispiti polaznika nisu pronadjeni.");

                return Ok(model);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Problem sa dobavljanjem ispita polaznika.");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getzavrseniispiti")]
        public async Task<IActionResult> GetZavrseniIspiti()
        {
            try
            {
                var radoviPolaznika = _radService.GetAll().AsQueryable();

                var model = radoviPolaznika.Where(x => x.Ispit.VrijemeZavrsetka < DateTime.Now)
                    .Select(x => new IspitRadDto
                {
                    RadId = x.Id,
                    VrijemePostavljanjaRada = x.DatumPostavljanja,
                    UrlRada = x.UrlDokumenta,
                    IspitId = x.Ispit.Id,
                    Naslov = x.Ispit.Naslov,
                    Opis = x.Ispit.Opis,
                    VrijemePocetka = x.Ispit.VrijemePocetka,
                    VrijemeZavrsetka = x.Ispit.VrijemeZavrsetka,
                    UrlIspita = x.Ispit.UrlDokumenta,
                    OrganizacijaKursa = x.Ispit.OrganizacijaKursa.Kurs.Naziv,
                    OrganizacijaKursaId = x.Ispit.OrganizacijaKursaId
                }).ToList();

                if (model == null)
                    return BadRequest("Ispiti polaznika nisu pronadjeni.");

                return Ok(model);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Problem sa dobavljanjem ispita polaznika.");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("dodajrad")]
        public async Task<IActionResult> DodajRad(RadDodaj model)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest("Rad nije moguce dodati.");

            var radovi = _radService.GetAll().AsQueryable();
            var postoji = radovi.Where(x => x.IspitId == model.IspitId && x.PolaznikId == _korisnickiRacun.Id).FirstOrDefault();

            if (postoji != null)
                return BadRequest("Rad nije moguce dodati vise puta.");

            try
            {
                Rad rad = new Rad
                {
                    DatumPostavljanja = DateTime.Now,
                    UrlDokumenta = model.UrlDokumenta,
                    IspitId = model.IspitId,
                    PolaznikId = _korisnickiRacun.Id
                };
                _radService.Add(rad);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Rad nije moguce dodati.");
            }
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formControl = await Request.ReadFormAsync();
                var file = formControl.Files.First();
                var folderName = "wwwroot/upload/radovi/";
                bool exists = Directory.Exists(folderName);
                if (!exists)
                    Directory.CreateDirectory(folderName);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
