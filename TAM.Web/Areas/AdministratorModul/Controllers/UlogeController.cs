using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Core;
using TAM.ViewModels;

namespace TAM.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class UlogeController : Controller
    {
        private readonly UserManager<KorisnickiRacun> _userManager; 
        private readonly RoleManager<IdentityRole> _roleManager;

        public UlogeController(UserManager<KorisnickiRacun> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string pretrazivanje, int pageNumber = 1, 
            int pageSize = 5) 
        { 
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<KorisnikUlogaVM>();

            foreach (KorisnickiRacun user in users) 
            { 
                var thisViewModel = new KorisnikUlogaVM(); 
                thisViewModel.UserId = user.Id; 
                thisViewModel.Email = user.Email; 
                thisViewModel.FirstName = user.FirstName; 
                thisViewModel.LastName = user.LastName; 
                thisViewModel.Roles = await GetUserRoles(user); 
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
            upit = upit.Skip(ExcludeRecords).Take(pageSize);

            var rezultat = new PagedResult<KorisnikUlogaVM>
            {
                Data = upit.AsNoTracking().ToList(),
                TotalItems = BrojKategorija,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            ViewData["Title"] = "Index";
            ViewData["Controller"] = "Uloge";
            ViewData["Action"] = "Index";
            return View(rezultat); 
        }

        public async Task<IActionResult> Upravljanje(string userId) 
        { 
            ViewBag.userId = userId; 
            var user = await _userManager.FindByIdAsync(userId); 

            if (user == null) 
            { 
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found"; 
                return NotFound(); 
            }

            ViewBag.UserName = user.FirstName + " " + user.LastName;
            var model = new List<KorisnikUlogaUpravljanjeVM>(); 

            foreach (var role in _roleManager.Roles.ToList()) 
            { 
                var userRolesViewModel = new KorisnikUlogaUpravljanjeVM
                { 
                    RoleId = role.Id, 
                    RoleName = role.Name
                }; 

                if (await _userManager.IsInRoleAsync(user, role.Name)) 
                { 
                    userRolesViewModel.Selected = true; 
                } 
                else 
                { 
                    userRolesViewModel.Selected = false; 
                }
                model.Add(userRolesViewModel); 
            } 
            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> Upravljanje(List<KorisnikUlogaUpravljanjeVM> model,
            string userId) 
        { 
            var user = await _userManager.FindByIdAsync(userId); 
            if (user == null) 
            { 
                return View(); 
            } 
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded) 
            { 
                ModelState.AddModelError("", "Cannot remove user existing roles"); 
                return View(model); 
            } 

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected)
                .Select(y => y.RoleName)); 

            if (!result.Succeeded) 
            { 
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            } 

            return RedirectToAction("Index"); 
        }
        private async Task<List<string>> GetUserRoles(KorisnickiRacun user) 
        { 
            return new List<string>(await _userManager.GetRolesAsync(user)); 
        }
    }    
}
