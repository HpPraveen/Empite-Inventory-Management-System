using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNet.Identity;
using System.Threading;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace InventoryManagementSystem.Pages.ManageUser
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public CreateModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IdentityRole IdentityRole { get; set; }

        public IActionResult OnGet()
        {
            if (!_context.Roles.Any(x => x.Name == "Admin"))
            {
                _context.Roles.Add(new IdentityRole("Admin"));
                _context.SaveChanges();
            }
            if (!_context.Roles.Any(x => x.Name == "Manager"))
            {
                _context.Roles.Add(new IdentityRole("Manager"));
                _context.SaveChanges();
            }
            if (!_context.Roles.Any(x => x.Name == "User"))
            {
                _context.Roles.Add(new IdentityRole("User"));
                _context.SaveChanges();
            }
            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Name");
            return Page();
        }

        public IActionResult OnGetSubmit(string name, string email, string password, string role)
        {
            try
            {
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
                var hashPassword = hashed;

                var user = new ApplicationUser
                {
                    Name = name,
                    UserName = email,
                    NormalizedUserName = email,
                    Email = email,
                    NormalizedEmail = email,
                    EmailConfirmed = true,
                    PasswordHash = password,
                };

                var userStore = new UserStore<ApplicationUser>(_context);
                userStore.CreateAsync(user);

                var userRole = new IdentityUserRole<string>
                {
                    RoleId = role,
                    UserId = user.Id
                };
                _context.UserRoles.AddAsync(userRole);
                _context.SaveChangesAsync();

                return new JsonResult(true);
            }
            catch (Exception)
            {
                return new JsonResult(false);
            }
        }
    }
}