using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TAM.API.Controllers;
using TAM.API.Dto;
using TAM.Core;
using NETCore.MailKit.Core;
using System.Text.Encodings.Web;
using TAM.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using TAM.ViewModels;
using TAM.Repository;

namespace TAM.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<KorisnickiRacun> userManager;
        private readonly SignInManager<KorisnickiRacun> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly Service.Interfaces.IEmailSender emailSender;
        private readonly JwtHandler jwtHandler;
        readonly ITipPolaznikaService TipPolaznikaService;


        public AccountController(UserManager<KorisnickiRacun> _userManager,
                                  SignInManager<KorisnickiRacun> _signInManager,
                                  Service.Interfaces.IEmailSender _emailSender,
                                  JwtHandler _jwtHandler,
                                  RoleManager<IdentityRole> _roleManager,
                                  ITipPolaznikaService _TipPolaznikaService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            emailSender = _emailSender;
            jwtHandler = _jwtHandler;
            roleManager = _roleManager;
            TipPolaznikaService = _TipPolaznikaService;
        }

        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roleList = roleManager.Roles
                .Where(x => x.Name == "Organizator" || x.Name == "Polaznik")
                .Select(x => new RolesVM
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            if (roleList == null)
                return NotFound();

            return Ok(roleList);
        }

        [HttpGet("GetStudentType")]
        public async Task<IActionResult> GetStudentType()
        {
            var roleList = TipPolaznikaService.GetAll();

            if (roleList == null)
                return NotFound();

            return Ok(roleList);
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] KorisnikRegistracijaDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            KorisnickiRacun user = new KorisnickiRacun()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            var role = roleManager.FindByIdAsync(dto.Role).Result;
            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var err = result.Errors.Select(e => e.Description);

                return BadRequest(new OdgovorRegistracijaDto { Errors = err });
            }

            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, role.Name);
                var roleError = roleResult.Errors.Select(e => e.Description);

                if (!roleResult.Succeeded)
                    return BadRequest(new OdgovorRegistracijaDto { Errors = roleError });



                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var param = new Dictionary<string, string>
                {
                    {"token", token },
                    {"email", user.Email }
                };

                var callback = QueryHelpers.AddQueryString(dto.ClientURI, param);

                await emailSender.SendEmail(user.Email, "Potvrda maila",
                        $"Poštovani, <br/><br/> Molimo vas da za kompletiranje vase registracije kliknite <a href='{HtmlEncoder.Default.Encode(callback)}'>ovdje</a>." +
                        $"<br/><br/> Lijep pozdrav!" +
                        $"<br/> Kulturni centar TAM.");
            }
            

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] KorisnikLoginDto korisnikLoginDto)
        {
            var user = await userManager.FindByNameAsync(korisnikLoginDto.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, korisnikLoginDto.Password))
                return Unauthorized(new OdgovorLoginDto { ErrorMessage = "Sorry we couldn\'t log you in. Try different email or password" });

            if (!await userManager.IsEmailConfirmedAsync(user))
                return Unauthorized(new OdgovorLoginDto { ErrorMessage = "Email is not confirmed" });

            var signinCredentials = jwtHandler.GetSigningCredentials();
            var claims = jwtHandler.GetClaims(user);
            var tokenOptions = jwtHandler.GenerateTokenOptions(signinCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new OdgovorLoginDto { IsAuthSuccessful = true, Token = token });
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            var res = new List<int>();

            res.Add(1);
            res.Add(2);
            res.Add(3);
            res.Add(4);
            res.Add(5);

            return Ok(res);
        }

        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");

            var confirmResult = await userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");

            return Ok();
        }

    }
}
