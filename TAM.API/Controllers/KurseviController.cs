using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Interfaces;
using TAM.Web.Helper;

namespace TAM.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KurseviController : ControllerBase
    {
        private IKursService _service;
        private IOrganizacijaKursaService _organizacijaKursaService;
        private IExceptionHandlerService _exceptionHandlerService;

        public KurseviController(IKursService service, IOrganizacijaKursaService organizacijaKursaService,
            IExceptionHandlerService exceptionHandlerService)
        {
            _service = service;
            _organizacijaKursaService = organizacijaKursaService;
            _exceptionHandlerService = exceptionHandlerService;
        }

        [HttpGet]
        public IActionResult Kursevi()
        {
            try
            {
                List<string> includes = new List<string>();
                includes.Add("KategorijaKursa");
                var kursevi = _service.GetAll().AsQueryable();
                kursevi = kursevi.Include(x => x.KategorijaKursa);
                return Ok(kursevi);
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Kursevi nisu pronadjeni");
            }
        }

        [HttpGet("{Najpopularniji}")]
        public IActionResult Najpopularniji()
        {
            try
            {
                var q = _organizacijaKursaService.GetAll().AsQueryable();
                var lista = q.GroupBy(x => x.KursId).Select(x => new NajpoplarnijiIds
                {
                    Id = x.Key,
                    BrojPojavljivanja = q.Where(y => y.KursId == x.Key).Count()
                });
                lista = lista.OrderByDescending(x => x.BrojPojavljivanja);
                var Ids = lista.Take(3).Select(x => x.Id);
                var kursevi = _service.GetAll().AsQueryable();
                kursevi = kursevi.Where(x => Ids.Contains(x.Id)).Include(x => x.KategorijaKursa);
                return Ok(kursevi);
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Kursevi nisu pronadjeni");
            }
            
        }
    }

    public class NajpoplarnijiIds
    {
        public int Id { get; set; }
        public int BrojPojavljivanja { get; set; }
    }
}
