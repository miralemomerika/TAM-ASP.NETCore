using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAM.API.Dto;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;
using TAM.Web.Helper;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogadjajController : ControllerBase
    {
        readonly IDogadjajService _dogadjajService;
        readonly ITipDogadjajaService _tipDogadjajaService;
        readonly ApplicationDbContext _context;
        readonly IExceptionHandlerService _exceptionHandlerService;
        private KorisnickiRacun _korisnickiRacun;

        public DogadjajController(IDogadjajService dogadjajService, ApplicationDbContext context,
            IExceptionHandlerService exceptionHandlerService, IHttpContextAccessor httpContextAccessor,
            ITipDogadjajaService tipDogadjajaService)
        {
            _tipDogadjajaService = tipDogadjajaService;
            _dogadjajService = dogadjajService;
            _context = context;
            _exceptionHandlerService = exceptionHandlerService;
            if (httpContextAccessor.HttpContext.User.Identity.Name != null)
            {
                _korisnickiRacun = _context.Users.First(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("o")]
        public IActionResult GetOrganizator()
        {
            try
            {
                var dogadjaji = _dogadjajService.GetAll();
                var dogadjajiget = new List<DogadjajGetDto>();
                foreach (var x in dogadjaji)
                {
                    dogadjajiget.Add(new DogadjajGetDto
                    {
                        DatumIVrijemeOdrzavanja=x.DatumIVrijemeOdrzavanja.ToString("dd.MM.yyyy. HH:mm"),
                        ImeOrganizatora=x.Organizator.KorisnickiRacun.FirstName+" "+x.Organizator.KorisnickiRacun.LastName,
                        Naziv=x.Naziv,
                        Id=x.Id,
                        Odobren=x.Odobren,
                        TipDogadjaja=x.TipDogadjaja.Naziv ,
                        Opis=x.Opis
                    });
                }

                //_context.Organizator.First(y => y.Id == x.OrganizatorId).ToString();
                return Ok(dogadjajiget);
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Dogadjaji nisu pronadjeni");
            }
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var dogadjaji = _dogadjajService.GetAll();
                dogadjaji = dogadjaji.OrderBy(x=>x.DatumIVrijemeOdrzavanja).Where(x => x.DatumIVrijemeOdrzavanja > DateTime.Now && x.Odobren);
                var dogadjajiget = new List<DogadjajGetDto>();
                foreach (var x in dogadjaji)
                {
                    dogadjajiget.Add(new DogadjajGetDto
                    {
                        DatumIVrijemeOdrzavanja = x.DatumIVrijemeOdrzavanja.ToString("dd.MM.yyyy. HH:mm"),
                        ImeOrganizatora = x.Organizator.KorisnickiRacun.FirstName + " " + x.Organizator.KorisnickiRacun.LastName,
                        Naziv = x.Naziv,
                        Id = x.Id,
                        Odobren = x.Odobren,
                        TipDogadjaja = x.TipDogadjaja.Naziv,
                        Opis = x.Opis
                    });
                }
                return Ok(dogadjajiget);
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Dogadjaji nisu pronadjeni");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}", Name = "GetDogadjajById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var temp = _dogadjajService.GetById(id);
                DogadjajGetDto dogadjaj = new DogadjajGetDto
                {
                    Id = temp.Id,
                    Naziv = temp.Naziv,
                    DatumIVrijemeOdrzavanja = temp.DatumIVrijemeOdrzavanja.ToString("dd.MM.yyyy. HH:mm"),
                    ImeOrganizatora = _context.Organizator.First(y => y.Id == temp.OrganizatorId).ToString(),
                    Odobren = temp.Odobren,
                    TipDogadjaja = temp.TipDogadjaja.Naziv,
                    Opis = temp.Opis
                };
                return Ok(dogadjaj);
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Dogadjaj nije pronadjen");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]       
        public IActionResult Create(API.Dto.DogadjajRequestDto dogadjaj)
        {
            try
            {

                Core.Dogadjaj temp = new Core.Dogadjaj
                {
                    DatumIVrijemeOdrzavanja = dogadjaj.DatumIVrijemeOdrzavanja,
                    TipDogadjajaId = dogadjaj.TipDogadjajaId,
                    Naziv = dogadjaj.Naziv,
                    Odobren = false,
                    OrganizatorId = _korisnickiRacun.Id,
                    Opis=dogadjaj.Opis
                };
                _dogadjajService.Add(temp);
                return Ok();
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Dogadjaj nije dodan");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]        
        public IActionResult Delete(int id)
        {
            try
            {
                var dogadjaj = _context.Dogadjaj.First(x => x.Id==id);
                _dogadjajService.Delete(dogadjaj);
                return Ok();
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Dogadjaj nije obrisan");
            }
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update([FromBody] DogadjajRequestDto dogadjaj)
        {
            try
            {
                Core.Dogadjaj temp = _dogadjajService.GetById(dogadjaj.Id);
                temp.Naziv = dogadjaj.Naziv;
                temp.TipDogadjajaId = dogadjaj.TipDogadjajaId;
                temp.DatumIVrijemeOdrzavanja = dogadjaj.DatumIVrijemeOdrzavanja;
                temp.Opis = dogadjaj.Opis;
                _dogadjajService.Update(temp);
                return Ok();
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest("Dogadjaj nije modifikovan");
            }
        }
        [HttpGet("tip")]
        public IActionResult GetTip()
        {
            try
            {
                return Ok(_tipDogadjajaService.GetAll());
            }
            catch (Exception ex)
            {
                _exceptionHandlerService.Add(PomocneMetode.GenerisiException(ex));
                return BadRequest();
            }
        }
    }
}
