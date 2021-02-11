using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAM.Core;
using TAM.Service.Interfaces;
using TAM.ViewModels;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class UposleniController : Controller
    {

        private readonly SignInManager<KorisnickiRacun> _signInManager;
        private readonly UserManager<KorisnickiRacun> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IPortirService _portirService;

        public UposleniController(
            UserManager<KorisnickiRacun> userManager,
            SignInManager<KorisnickiRacun> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            IPortirService portirService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _portirService = portirService;
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

                    await PosaljiLozinkuMailomAsync(Input.Email, Input.Password);
                    if(Input.TipUposlenog == "Portir")
                    {
                        Portir portir = new Portir
                        {
                            KorisnickiRacun = user
                        };
                        _portirService.Add(portir);
                    }
                    else if (Input.TipUposlenog == "Predavac")
                    {
                        
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Index");
        }

        private async Task PosaljiLozinkuMailomAsync(string email, string lozinka)
        {
            string subject = "Lozinka za korisnicki racun";
            string htmlMessage = @"Poštovani,<br/><br/>" + "Lozinka za vas korisnički račun je: <b>{0}</b><br/>" + "Molimo Vas da nakon prijave promijenite svoju lozinku." + "<br/><br/>" + "Lijep pozdrav!" + "<br/>" + "Kulturni centar TAM";
            htmlMessage = string.Format(htmlMessage, lozinka);
            await _emailSender.SendEmail(email, subject, htmlMessage);
        }
    }
}