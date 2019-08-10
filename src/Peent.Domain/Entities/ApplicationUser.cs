using Microsoft.AspNetCore.Identity;

namespace Peent.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
    }
}
