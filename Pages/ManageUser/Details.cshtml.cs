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
    public class DetailsModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public DetailsModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
