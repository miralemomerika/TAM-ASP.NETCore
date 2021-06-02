using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAM.API.Dto;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;
using TAM.Web.Helper;
using System.IO;
using System.Net.Http.Headers;

namespace TAM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IspitiController: ControllerBase
    {
        readonly IIspitService _ispitService;
        readonly KorisnickiRacun _korisnickiRacun;
        readonly ApplicationDbContext _context;
        readonly IExceptionHandlerService _exceptionHandler;
        readonly IOrganizacijaKursaService _organizacijaKursaService;

        public IspitiController(IIspitService ispitService,
                                IHttpContextAccessor httpContextAccessor,
                                IExceptionHandlerService exceptionHandler,
                                IOrganizacijaKursaService organizacijaKursaService,
                                ApplicationDbContext context)
        {
            _ispitService = ispitService;
            _exceptionHandler = exceptionHandler;
            _organizacijaKursaService = organizacijaKursaService;
            _context = context;
            if(httpContextAccessor.HttpContext.User.Identity.Name != null)
            {
                _korisnickiRacun = _context.Users.First(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getispiti")]
        public async Task<IActionResult> GetIspiti()
        {
            try
            {
                var ispiti = _ispitService.GetAll();
                var model = ispiti.Where(x => x.VrijemePocetka > DateTime.Now)
                    .Select(x => new IspitGetDto
                {
                    Id = x.Id,
                    Naslov = x.Naslov,
                    Opis = x.Opis,
                    VrijemePocetka = x.VrijemePocetka,
                    VrijemeZavrsetka = x.VrijemeZavrsetka,
                    UrlDokumenta = x.UrlDokumenta,
                    OrganizacijaKursa = x.OrganizacijaKursa.Kurs.Naziv + ", " 
                    + x.OrganizacijaKursa.DatumPocetka.ToString("dd.MM.yyyy, hh:MM") + " | " + x.OrganizacijaKursa.DatumZavrsetka.ToString("dd.MM.yyyy, hh:MM"),
                    OrganizacijaKursaId = x.OrganizacijaKursaId
                }).ToList();

                if (model == null)
                    return BadRequest("Ispiti nisu pronadjeni.");

                return Ok(model);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Ispiti nisu pronadjeni.");
            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getnaispiti")]
        public async Task<IActionResult> GetNeaktivniIspiti()
        {
            try
            {
                var ispiti = _ispitService.GetAll();
                var model = ispiti.Where(x => x.VrijemePocetka < DateTime.Now)
                    .Select(x => new IspitGetDto
                {
                    Id = x.Id,
                    Naslov = x.Naslov,
                    Opis = x.Opis,
                    VrijemePocetka = x.VrijemePocetka,
                    VrijemeZavrsetka = x.VrijemeZavrsetka,
                    UrlDokumenta = x.UrlDokumenta,
                    OrganizacijaKursa = x.OrganizacijaKursa.Kurs.Naziv + ", "
                    + x.OrganizacijaKursa.DatumPocetka.ToString("dd.MM.yyyy, hh:MM") + " | " + x.OrganizacijaKursa.DatumZavrsetka.ToString("dd.MM.yyyy, hh:MM"),
                    OrganizacijaKursaId = x.OrganizacijaKursaId
                }).ToList();

                if (model == null)
                    return BadRequest("Neaktivni ispiti nisu pronadjeni.");

                return Ok(model);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Neaktivni ispiti nisu pronadjeni.");
            }

        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("createispit")]
        public async Task<IActionResult> CreateIspit(IspitDodajDto model)
        {
            if(model == null || !ModelState.IsValid)
            {
                return BadRequest("Operaciju nije moguce izvrsiti.");
            }

            try
            {
                Ispit ispit = new Ispit
                {
                    Naslov = model.Naslov,
                    Opis = model.Opis,
                    UrlDokumenta = model.UrlDokumenta,
                    VrijemePocetka = model.VrijemePocetka,
                    VrijemeZavrsetka = model.VrijemeZavrsetka,
                    OrganizacijaKursaId = model.OrganizacijaKursaId
                };
                _ispitService.Add(ispit);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Operaciju je nemoguce izvrsiti.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                if(Id == null)
                {
                    return BadRequest("Ispit nije moguce obrisati.");
                }

                var ispit = _ispitService.GetById(Id);

                if (ispit == null)
                    return BadRequest("Ispit nije moguce obrisati.");

                _ispitService.Delete(ispit);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Ispit nije moguce obrisati.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(IspitDodajDto model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                    return BadRequest("Ispit nije moguce modifikovati.");

                Ispit ispit = _ispitService.GetById(model.Id);
                ispit.Naslov = model.Naslov;
                ispit.Opis = model.Opis;
                ispit.VrijemePocetka = model.VrijemePocetka;
                ispit.VrijemeZavrsetka = model.VrijemeZavrsetka;
                ispit.UrlDokumenta = model.UrlDokumenta;
                ispit.OrganizacijaKursaId = model.OrganizacijaKursaId;

                _ispitService.Update(ispit);
                return StatusCode(201);

            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Ispit nije moguce modifikovati.");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getorganizacije")]
        public async Task<IActionResult> GetOrganizacije()
        {
            try
            {
                var organizacije = _organizacijaKursaService.GetAll().AsQueryable();
                List<SelectListItem> lista = new List<SelectListItem>();
                foreach(var x in organizacije)
                {
                    lista.Add(new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Kurs.Naziv + ", "
                    + x.DatumPocetka.ToString("dd.MM.yyyy") + " | " + x.DatumZavrsetka.ToString("dd.MM.yyyy")
                    });
                }

                if (organizacije == null)
                    return BadRequest("Organizacije kurseva nije moguce dobaviti.");

                return Ok(lista);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest();
            }
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formControl = await Request.ReadFormAsync();
                var file = formControl.Files.First();
                var folderName = "wwwroot/upload/";
                bool exists = Directory.Exists(folderName);
                if (!exists)
                    Directory.CreateDirectory(folderName);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if(file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using(var stream = new FileStream(fullPath, FileMode.Create))
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
