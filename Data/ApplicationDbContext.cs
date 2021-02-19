using System;
using System.Collections.Generic;
using System.Text;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Pages.ManageUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MerchantDetails> MerchantDetails { get; set; }

        public DbSet<InventoryDetails> InventoryDetails { get; set; }
    }
}