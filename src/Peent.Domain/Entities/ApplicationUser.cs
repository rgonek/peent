using EnsureThat;
using Microsoft.AspNetCore.Identity;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        [PersonalData]
        public string FirstName { get; private set; }

        private ApplicationUser() { }

        public ApplicationUser(string firstName)
        {
            Ensure.That(firstName, nameof(firstName)).IsNotNullOrWhiteSpace();

            FirstName = firstName;
        }

        public void SetFirstName(string firstName)
        {
            Ensure.That(firstName, nameof(firstName)).IsNotNullOrWhiteSpace();

            FirstName = firstName;
        }
    }
}
