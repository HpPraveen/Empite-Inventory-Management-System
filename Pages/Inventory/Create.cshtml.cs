using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Pages.Inventory
{
    //[Authorize(Roles = "Admin, Manager")]
    public class CreateModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public CreateModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["MerchantsName"] = new SelectList(_context.MerchantDetails, "MerchantId", "MerchantName");
            ViewData["MerchantsEmail"] = new SelectList(_context.MerchantDetails, "MerchantId", "MerchantEmail");
            return Page();
        }

        [BindProperty]
        public InventoryDetails InventoryDetails { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InventoryDetails.Add(InventoryDetails);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}