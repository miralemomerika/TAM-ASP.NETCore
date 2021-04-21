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

        public KurseviController(IKursService service)
        {
            _service = service;
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
    }
}
