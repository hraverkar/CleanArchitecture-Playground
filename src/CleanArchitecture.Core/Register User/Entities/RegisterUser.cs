using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;

namespace CleanArchitecture.Core.Weather.Entities
{
    public sealed class RegisterUser : AggregateRoot
    {
        private RegisterUser(string email, string password, string userName)
        {
            Email = email;
            Password = password;
            UserName = userName;
        }

#pragma warning disable CS8618 // this is needed for the ORM for serializing Value Objects
        private RegisterUser()
#pragma warning restore CS8618
        {

        }

        public static RegisterUser Create(string email, string password, string userName)
        {
            // validation should go here before the aggregate is created
            // an aggregate should never be in an invalid state
            // the temperature is validated in the Temperature ValueObject and is always valid
            email = (email ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(email, nameof(Email));
            password = (password ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(password, nameof(Password));
            userName = (userName ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(userName, nameof(UserName));
            return new RegisterUser(email, password, userName);

        }

        public string Email { get; private set; }
        public string Password { get; private set; }
        public string UserName { get; private set; }
        public bool IsDeleted { get; private set; } = false;
    }
}
