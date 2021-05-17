using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Interfaces;

namespace TAM.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KurseviController : ControllerBase
    {
        private IKursService _service;
        private IOrganizacijaKursaService _organizacijaKursaService;

        public KurseviController(IKursService service, IOrganizacijaKursaService organizacijaKursaService)
        {
            _service = service;
            _organizacijaKursaService = organizacijaKursaService;
        }

        [HttpGet]
        public IActionResult Kursevi()
        {
            List<string> includes = new List<string>();
            includes.Add("KategorijaKursa");
            var kursevi = _service.GetAll().AsQueryable();
            kursevi = kursevi.Include(x => x.KategorijaKursa);
            return Ok(kursevi);
        }

        [HttpGet("{Najpopularniji}")]
        public IActionResult Najpopularniji()
        {
            var q = _organizacijaKursaService.GetAll().AsQueryable();
            var lista = q.GroupBy(x => x.KursId).Select(x => new NajpoplarnijiIds{
                Id = x.Key,
                BrojPojavljivanja = q.Where(y => y.KursId == x.Key).Count()
            });
            lista = lista.OrderByDescending(x => x.BrojPojavljivanja);
            var Ids = lista.Take(3).Select(x => x.Id);
            var kursevi = _service.GetAll().AsQueryable();
            kursevi = kursevi.Where(x => Ids.Contains(x.Id)).Include(x => x.KategorijaKursa);
            return Ok(kursevi);
        }
    }

    public class NajpoplarnijiIds
    {
        public int Id { get; set; }
        public int BrojPojavljivanja { get; set; }
    }
}
