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


namespace TAM.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<KorisnickiRacun> userManager;
        private readonly SignInManager<KorisnickiRacun> signInManager;
        private readonly Service.Interfaces.IEmailSender emailSender;
        private readonly JwtHandler jwtHandler;

        public AccountController(UserManager<KorisnickiRacun> _userManager,
                                  SignInManager<KorisnickiRacun> _signInManager,
                                  Service.Interfaces.IEmailSender _emailSender,
                                  JwtHandler _jwtHandler)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            emailSender = _emailSender;
            jwtHandler = _jwtHandler;
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

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var err = result.Errors.Select(e => e.Description);

                return BadRequest(new OdgovorRegistracijaDto { Errors = err });
            }

            if (result.Succeeded)
            {
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

    }
}
