﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNet.Identity;

namespace InventoryManagementSystem.Pages.ManageUser
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly InventoryManagementSystem.Data.ApplicationDbContext _context;

        public CreateModel(InventoryManagementSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApplicationUserDetails ApplicationUsers { get; set; }

        public IActionResult OnGetSubmit(string name, string email, string password)
        {
            //if (_context.Roles.Any(x => x.Name == "Admin"))
            //{
            //    _context.Roles.Add(new IdentityRole("Viewer"));
            //    _context.SaveChanges();
            //}

            var user = new ApplicationUserDetails
            {
                Name = name,
                UserName = email,
                NormalizedUserName = email,
                Email = email,
                NormalizedEmail = email,
                EmailConfirmed = true,
                PasswordHash = password,
            };

            var userStore = new UserStore<IdentityUser>(_context);
            var userRole = new IdentityUserRole<string>();
            userRole.RoleId = "1";
            userRole.UserId = user.Id;

            userStore.CreateAsync(user);
            _context.UserRoles.Add(userRole);
            _context.SaveChangesAsync();

            bool isSaved = false;

            return new JsonResult(isSaved);
        }
    }
}