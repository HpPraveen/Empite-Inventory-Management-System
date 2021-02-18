using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;

namespace InventoryManagementSystem.Pages.ManageUser
{
    public class IndexModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public IndexModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var userDetails = _context.Users.Where(u => u.EmailConfirmed == true).ToList();
            return Page();
        }

        public IActionResult OnGetFindUser(int merchantId)
        {
            var merchantEmail = _context.MerchantDetails.Where(i => i.MerchantId == merchantId).FirstOrDefault().MerchantEmail;

            return new JsonResult("ll");
        }
    }
}