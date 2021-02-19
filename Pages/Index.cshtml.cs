using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace InventoryManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, InventoryManagementSystem.Data.ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
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

            if (!_userManager.Users.Any(x => x.Email == "admin@empite.com"))
            {
                var adminRoleId = _context.Roles.Where(r => r.Name == "Admin").FirstOrDefault().Id.ToString();
                var user = new IdentityUser { UserName = "admin@empite.com", Email = "admin@empite.com", EmailConfirmed = true };
                await _userManager.CreateAsync(user, "admin@123");

                var userRole = new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = user.Id
                };
                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();
            }
            return Page();
        }
    }
}