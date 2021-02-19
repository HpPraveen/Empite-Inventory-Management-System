using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;

namespace InventoryManagementSystem.Pages.ManageUser
{
    //[Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public IndexModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<ApplicationUser> ApplicationUser { get; set; }

        public async Task OnGetAsync()
        {
            ApplicationUser = await _context.Users.ToListAsync();
        }

        public IActionResult OnGetFindUser()
        {
            var userDetails = _context.Users.Where(u => u.EmailConfirmed == true).ToList();

            return new JsonResult(userDetails);
        }
    }
}