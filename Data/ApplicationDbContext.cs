using System;
using System.Collections.Generic;
using System.Text;
using InventoryManagementSystem.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MerchantDetails> MerchantDetails { get; set; }

        public DbSet<InventoryManagementSystem.Data.Models.InventoryDetails> InventoryDetails { get; set; }
    }
}