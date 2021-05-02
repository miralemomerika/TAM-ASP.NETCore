using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.API.Dto;
using TAM.Service.Interfaces;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecenzijeController : ControllerBase
    {
        private IRecenzijeService _recenzijeService;

        public RecenzijeController(IRecenzijeService recenzijeService)
        {
            _recenzijeService = recenzijeService;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(_recenzijeService.GetAll());
        //}

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
    }
}
