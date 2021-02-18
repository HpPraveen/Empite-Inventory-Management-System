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
    //[Authorize]
    public class IndexModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public IndexModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<InventoryDetails> InventoryDetails { get; set; }

        public async Task OnGetAsync()
        {
            InventoryDetails = await _context.InventoryDetails
                .Include(i => i.MerchantDetails).ToListAsync();
        }
    }
}