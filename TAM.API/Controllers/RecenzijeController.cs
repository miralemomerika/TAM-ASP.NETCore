using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.API.Dto;
using TAM.Core;
using TAM.Service.Interfaces;
using TAM.Web.Helper;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecenzijeController : ControllerBase
    {
        private IRecenzijeService _recenzijeService;
        private IExceptionHandlerService _exceptionHandlerService;

        public RecenzijeController(IRecenzijeService recenzijeService, IExceptionHandlerService exceptionHandlerService)
        {
            _recenzijeService = recenzijeService;
            _exceptionHandlerService = exceptionHandlerService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Recenzije()
        {
            var aktivne = _recenzijeService.GetAllAktivne();
            var rez = aktivne.Select(x => new AktivneRecenzijeDto
            {
                OrganizacijaKursId = x.Id,
                Predavac = $"{x.Predavac.KorisnickiRacun.FirstName} {x.Predavac.KorisnickiRacun.LastName}",
                Kurs = $"{x.Kurs.Naziv}"
            }).ToList();
            return Ok(rez);
        }

        [HttpGet("brojaktivnih")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public int? BrojAktivnih()
        {
            var broj = _recenzijeService.GetAllAktivne().Count();
            if (broj > 0)
                return broj;
            else
                return null;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Recenzija([FromBody] Recenzija recenzija)
        {
            try
            {
                _recenzijeService.Add(recenzija);
                return Ok();
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest();
            }
        }
    }
}
