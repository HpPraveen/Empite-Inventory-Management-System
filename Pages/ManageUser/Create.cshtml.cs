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
        public ApplicationUser ApplicationUsers { get; set; }

        public IActionResult OnGetSubmit(string username, string email, string password)
        {
            var user = new ApplicationUser
            {
                FirstName = username,
                LasstName = username,
            };
            bool isSaved = false;
            _context.Users.Add(user);
            _context.SaveChangesAsync();

            return new JsonResult(isSaved);
        }
    }
}