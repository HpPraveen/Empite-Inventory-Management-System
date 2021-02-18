using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Pages.ManageUser
{
    public class ApplicationUserDetails : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
    }
}