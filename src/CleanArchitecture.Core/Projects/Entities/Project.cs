using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;

namespace CleanArchitecture.Core.Projects.Entities
{
    public sealed class Project : AggregateRoot
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Project() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Project(string projectName, string projectDescription, DateTime createdAt, string createdBy)
        { 
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }

        public static Project Create(string projectName, string projectDescription, DateTime createdAt, string createdBy)
        {
            projectName = (projectName ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(projectName, nameof(ProjectName));
            projectDescription = (projectDescription ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(projectDescription, nameof(ProjectDescription));
            return new Project(projectName, projectDescription, createdAt, createdBy);
        }

        public void Update(string projectName, string projectDescription, DateTime updatedAt, string updatedBy)
        {
            projectName = (projectName ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(projectName, nameof(projectName));
            projectDescription = (projectDescription ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(projectDescription, nameof(projectDescription));

            // Update properties
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
