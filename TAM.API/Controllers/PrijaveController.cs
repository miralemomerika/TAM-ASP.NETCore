using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Service.Interfaces;

namespace TAM.API.Controllers
{
    [Route("api/prijave")]
    [ApiController]
    public class PrijaveController : Controller
    {
        private IPrijavaService _prijavaService;

        public PrijaveController(IPrijavaService prijavaService)
        {
            _prijavaService = prijavaService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Polaznik")]
        public async Task<IActionResult> Prijave([FromBody] int id)
        {
            return Ok(await _prijavaService.Add(id));
        }
    }
}
