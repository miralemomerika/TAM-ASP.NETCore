using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAM.Core;
using TAM.Service.Interfaces;
using TAM.ViewModels;
using TAM.Web.Helper;

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
        private readonly IPredavacService _predavacService;

        public UposleniController(
            UserManager<KorisnickiRacun> userManager,
            SignInManager<KorisnickiRacun> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            IPortirService portirService,
            IPredavacService predavacService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _portirService = portirService;
            _predavacService = predavacService;
        }

        public async Task<IActionResult> Index(string pretrazivanje, int pageNumber = 1,
            int pageSize = 5)
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<KorisnikUlogaVM>();

            foreach (KorisnickiRacun user in users)
            {
                bool flag = false;
                var thisViewModel = new KorisnikUlogaVM();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                foreach (var item in thisViewModel.Roles)
                {
                    if (item == "Predavac")
                    {
                        flag = true;
                        try
                        {
                            var predavac = _predavacService.GetById(thisViewModel.UserId);
                            thisViewModel.CVUrl = predavac.CVUrl;
                            thisViewModel.Titula = predavac.Titula;
                        }
                        catch (Exception)
                        {

                        }
                    }
                    if(item == "Portir")
                    {
                        flag = true;
                    }
                }
                if(flag)
                    userRolesViewModel.Add(thisViewModel);
            }

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            ViewBag.CurrentFilter = pretrazivanje;
            var BrojKategorija = userRolesViewModel.Count();
            var upit = userRolesViewModel.AsQueryable();

            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                upit = upit.Where(x => x.FirstName.Contains(pretrazivanje));
                BrojKategorija = userRolesViewModel.Count();
            }

            ViewData["Title"] = "Index";
            ViewData["Controller"] = "Uposleni";
            ViewData["Action"] = "Index";
            return View(PomocneMetode.Paginacija<KorisnikUlogaVM>(pretrazivanje, upit, pageNumber, pageSize));
        }

        public IActionResult Registracija()
        {
            var model = new UposleniRegistracijaVM
            {
                TipUposlenog = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> Registruj(UposleniRegistracijaVM Input)
        {
            var rola = _roleManager.FindByIdAsync(Input.TipUposlenogId).Result;
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
                    var roleresult = await _userManager.AddToRoleAsync(user, rola.Name);

                    if (!roleresult.Succeeded)
                    {
                        throw new Exception("Nije moguce dodati rolu");
                    }

                    //await PosaljiLozinkuMailomAsync(Input.Email, Input.Password);
                    if(rola.Name == "Portir")
                    {
                        Portir portir = new Portir
                        {
                            KorisnickiRacun = user
                        };
                        _portirService.Add(portir);
                        TempData["successAdd"] = "Portir uspjesno registrovan.";
                    }
                    else if (rola.Name == "Predavac")
                    {
                        Predavac predavac = new Predavac
                        {
                            KorisnickiRacun = user,
                            Titula = Input.Titula
                        };
                        if (Input.CV != null)
                        {
                            string ekstenzija = Path.GetExtension(Input.CV.FileName);
                            string contentType = Input.CV.ContentType;

                            var fileName = $"{Guid.NewGuid()}{ekstenzija}";
                            string folder = "wwwroot/upload/";
                            bool exist = System.IO.Directory.Exists(folder);
                            if (!exist)
                                System.IO.Directory.CreateDirectory(folder);

                            Input.CV.CopyTo(new FileStream(folder + fileName, FileMode.Create));
                            predavac.CVUrl = fileName;
                        }
                        _predavacService.Add(predavac);
                        TempData["successAdd"] = "Predavac uspjesno registrovan.";
                    }
                    else
                    {
                        TempData["successAdd"] = "Administrator uspjesno registrovan.";
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Redirect("Index");
        }

        public IActionResult PredavacInput()
        {
            TempData["titule"] = new List<SelectListItem>
            {
                new SelectListItem{Value = "mr.sc", Text = "mr.sc"},
                new SelectListItem{Value = "dr.sc", Text = "dr.sc"}
            };
            return View();
        }

        private async Task PosaljiLozinkuMailomAsync(string email, string lozinka)
        {
            string subject = "Lozinka za korisnicki racun";
            string htmlMessage = @"Poštovani,<br/><br/>" + "Lozinka za vas korisnički račun je: <b>{0}</b><br/>" + "Molimo Vas da nakon prijave promijenite svoju lozinku." + "<br/><br/>" + "Lijep pozdrav!" + "<br/>" + "Kulturni centar TAM";
            htmlMessage = string.Format(htmlMessage, lozinka);
            await _emailSender.SendEmail(email, subject, htmlMessage);
        }

        private async Task<List<string>> GetUserRoles(KorisnickiRacun user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}