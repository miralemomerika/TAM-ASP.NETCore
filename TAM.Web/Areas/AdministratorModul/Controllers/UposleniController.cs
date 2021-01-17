using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAM.Core;
using TAM.ViewModels;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class UposleniController : Controller
    {

        private readonly SignInManager<KorisnickiRacun> _signInManager;
        private readonly UserManager<KorisnickiRacun> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IEmailSender _emailSender;

        public UposleniController(
            UserManager<KorisnickiRacun> userManager,
            SignInManager<KorisnickiRacun> signInManager,
            RoleManager<IdentityRole> roleManager
            //,IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            //_emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registracija()
        {
            var role = _roleManager.Roles.Select(x => x.Name).ToList();
            TempData["Role"] = role;

            return View(new UposleniRegistracijaVM());
        }
        public async Task<IActionResult> Registruj(UposleniRegistracijaVM Input)
        {
            if (ModelState.IsValid)
            {
                var user = new KorisnickiRacun
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var roleresult = await _userManager.AddToRoleAsync(user, Input.TipUposlenog);

                    if (!roleresult.Succeeded)
                    {
                        throw new Exception("Nije moguce dodati rolu");
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                       // $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Index");
        }
    }
}
