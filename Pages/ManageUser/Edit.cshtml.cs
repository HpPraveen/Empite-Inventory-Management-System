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
    public class EditModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public EditModel(InventoryManagementSystem.Data.ApplicationDbContext context)
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
           ViewData["MerchantId"] = new SelectList(_context.MerchantDetails, "MerchantId", "MerchantId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(InventoryDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryDetailsExists(InventoryDetails.ItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InventoryDetailsExists(long id)
        {
            return _context.InventoryDetails.Any(e => e.ItemId == id);
        }
    }
}
