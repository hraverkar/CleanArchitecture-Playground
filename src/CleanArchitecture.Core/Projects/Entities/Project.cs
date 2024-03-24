using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;

namespace CleanArchitecture.Core.Projects.Entities
{
    public sealed class Project : AggregateRoot
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Project() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Project(string projectName, string projectDescription, bool isDeleted, DateTime createdAt, DateTime updatedAt, string createdBy, string updatedBy)
        {
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            IsDeleted = isDeleted;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
        }

        public Project(string projectName, string projectDescription, bool isDeleted, DateTime createdAt, string createdBy)
        {
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            IsDeleted = isDeleted;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }

        public static Project Create(string projectName, string projectDescription, bool isDeleted, DateTime createdAt, string createdBy)
        {
            projectName = (projectName ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(projectName, nameof(ProjectName));
            projectDescription = (projectDescription ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(projectDescription, nameof(ProjectDescription));
            return new Project(projectName, projectDescription, isDeleted, createdAt, createdBy);
        }

        public void IsDeletedFlag(bool isDelete)
        {
            IsDeleted = isDelete;
        }

    }
}
