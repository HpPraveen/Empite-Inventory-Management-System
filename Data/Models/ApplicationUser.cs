using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Pages.ManageUser
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LasstName { get; set; }
    }
}