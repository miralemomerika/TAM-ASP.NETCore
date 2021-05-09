using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Interfaces;

namespace TAM.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatistikaController : ControllerBase
    {
        private IStatistikaService _statistikaService;

        public StatistikaController(IStatistikaService statistikaService)
        {
            _statistikaService = statistikaService;
        }

        [HttpGet]
        public IActionResult Statistika()
        {
            try
            {
                return Ok(_statistikaService.GetStatistika());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
