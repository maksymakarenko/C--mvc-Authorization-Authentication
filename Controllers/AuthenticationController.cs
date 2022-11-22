using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAuthenticationAuthorization.Models;
using TaskAuthenticationAuthorization.Models.Auth.Entities;
using TaskAuthenticationAuthorization.Models.Auth.ViewModels;

namespace TaskAuthenticationAuthorization.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly ShoppingContext _context;

        public AuthenticationController(ShoppingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user is null)
                {
                    var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "buyer");
                    var newCustomer = await _context.Customers.AddAsync(model.Customer);

                    _context.Users.Add(new User
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Role = defaultRole,
                        RoleId = defaultRole.Id,
                        Customer = newCustomer.Entity,
                    });

                    await _context.SaveChangesAsync();

                    await Authenticate(model.Email);
                    return RedirectToAction(nameof(Index), "Home");   
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Incorrect login and/or password");
                }
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user is null)
                {
                    ModelState.AddModelError(String.Empty, "Incorrect login and/or password");
                }
                else
                {
                    await Authenticate(model.Email);
                    return RedirectToAction(nameof(Index), "Home");
                }
            }

            return View(model);
        }

        [NonAction]
        private async Task Authenticate(string email)
        {
            var buyerType = new [] { "none", "regular", "golden", "wholesale" };
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user!.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user!.Role.Name),
                new Claim("buyerType", "regular"),
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index), "Products");
        }

        public IActionResult AccessDenied()
        {
            return View();
        } 
    }
}