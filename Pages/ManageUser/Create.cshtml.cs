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
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;

namespace InventoryManagementSystem.Pages.ManageUser
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;

        public CreateModel(InventoryManagementSystem.Data.ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public IdentityRole IdentityRole { get; set; }

        public IActionResult OnGet()
        {
            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnGetSubmit(string name, string email, string password, string role)
        {
            try
            {
                var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                await _userManager.CreateAsync(user, password);

                var userRole = new IdentityUserRole<string>
                {
                    RoleId = role,
                    UserId = user.Id
                };
                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Successfully created new user " + email + "!";
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Save failed!";
                return new JsonResult(false);
            }
        }
    }
}