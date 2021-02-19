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

            Task.Delay(100000000);

            var userStore = new UserStore<ApplicationUser>(_context);
            userStore.CreateAsync(user);

            var userRole = new IdentityUserRole<string>
            {
                RoleId = role,
                UserId = user.Id
            };
            _context.UserRoles.AddAsync(userRole);
            _context.SaveChangesAsync();

            bool isSaved = false;

            return new JsonResult(isSaved);
        }
    }
}