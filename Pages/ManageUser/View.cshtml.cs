using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Pages.ManageUser
{  //[Authorize(Roles = "Admin")]
    public class ViewModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public ViewModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<IdentityUser> DisabledApplicationUser { get; set; }

        [BindProperty]
        public IList<IdentityUser> EnabledApplicationUser { get; set; }

        public async Task OnGetAsync()
        {
            EnabledApplicationUser = await _context.Users.Where(i => i.EmailConfirmed == true).ToListAsync();
            DisabledApplicationUser = await _context.Users.Where(i => i.EmailConfirmed == false).ToListAsync();
        }

        public IActionResult OnGetDisableUser(string userId)
        {
            try
            {
                var userDetails = _context.Users.Where(u => u.Id == userId).ToList();

                foreach (var user in userDetails)
                {
                    user.EmailConfirmed = false;
                    _context.Users.Attach(user).State = EntityState.Modified;
                    _context.SaveChangesAsync();
                }
                return new JsonResult(true);
            }
            catch (Exception)
            {
                return new JsonResult(false);
            }
        }

        public IActionResult OnGetEnableUser(string userId)
        {
            try
            {
                var userDetails = _context.Users.Where(u => u.Id == userId).ToList();

                foreach (var user in userDetails)
                {
                    user.EmailConfirmed = true;
                    _context.Users.Attach(user).State = EntityState.Modified;
                    _context.SaveChangesAsync();
                }
                return new JsonResult(true);
            }
            catch (Exception)
            {
                return new JsonResult(false);
            }
        }
    }
}