using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Pages.Inventory
{
    //[Authorize(Roles = "Admin, Manager")]
    public class DeleteModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public DeleteModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InventoryDetails InventoryDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InventoryDetails = await _context.InventoryDetails
                .Include(i => i.MerchantDetails).FirstOrDefaultAsync(m => m.ItemId == id);

            if (InventoryDetails == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InventoryDetails = await _context.InventoryDetails.FindAsync(id);

            if (InventoryDetails != null)
            {
                _context.InventoryDetails.Remove(InventoryDetails);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}