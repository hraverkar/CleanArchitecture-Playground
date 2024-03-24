using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Locations.ValueObjects;

namespace CleanArchitecture.Core.Locations.Entities
{
    public sealed class Author : AggregateRoot
    {
        private Author(string name, string email, string description)
        {
            Name = name;
            Email = email;
            Description = description;
        }

#pragma warning disable CS8618 // this is needed for the ORM for serializing Value Objects
        private Author()
#pragma warning restore CS8618
        {

        }

        public static Author Create(string name, string email, string description)
        {
            return new Author(name, email, description);
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Description { get; private set; }

        public void MarkAsDeleted(bool isDelete)
        {
            this.IsDeleted = isDelete;
        }
    }
}
