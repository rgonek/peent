using EnsureThat;
using Microsoft.AspNetCore.Identity;

namespace Peent.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; private set; }

        private ApplicationUser() { }

        public ApplicationUser(string firstName)
        {
            Ensure.That(firstName, nameof(firstName)).IsNotNullOrWhiteSpace();

            FirstName = firstName;
        }
    }
}
