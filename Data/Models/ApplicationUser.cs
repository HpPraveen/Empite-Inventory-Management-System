using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Pages.ManageUser
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
    }
}