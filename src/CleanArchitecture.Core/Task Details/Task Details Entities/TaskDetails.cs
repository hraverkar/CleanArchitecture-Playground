using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Projects.Entities;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;

namespace CleanArchitecture.Core.Task.Entities
{
    public sealed class TaskDetails : AggregateRoot
    {
        public string TaskTitle { get; set; }
        public string TaskDetail { get; set; }
        public string TaskAssignTo { get; set; }
        public Guid TaskStatusId { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public DateTime TaskCreatedAt { get; set; } = DateTime.Now;
        public string TaskCreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

        private TaskDetails(string taskTitle, string taskDetail, string taskAssignTo, Guid taskStatusId, DateTime taskCreatedAt, string taskCreatedBy, bool isDeleted, Guid projectId)
        {
            TaskTitle = taskTitle;
            TaskDetail = taskDetail;
            TaskAssignTo = taskAssignTo;
            TaskStatusId = taskStatusId;
            TaskCreatedAt = taskCreatedAt;
            TaskCreatedBy = taskCreatedBy;
            IsDeleted = isDeleted;
            ProjectId = projectId;
        }

#pragma warning disable CS8618 // this is needed for the ORM for serializing Value Objects
        private TaskDetails()
#pragma warning restore CS8618
        {

        }

        public static TaskDetails Create(string taskTitle, string taskDetail, string taskAssignTo, Guid taskStatusId, DateTime taskCreatedAt, string taskCreatedBy, bool isDeleted, Guid projectId)
        {

            taskTitle = (taskTitle ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(taskTitle, nameof(TaskTitle));
            taskDetail = (taskDetail ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(taskDetail, nameof(TaskDetail));
            taskAssignTo = (taskAssignTo ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(taskAssignTo, nameof(TaskAssignTo));
            taskCreatedBy = (taskCreatedBy ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(taskCreatedBy, nameof(TaskCreatedBy));

            return new TaskDetails(taskTitle, taskDetail, taskAssignTo, taskStatusId, taskCreatedAt, taskCreatedBy, isDeleted, projectId);
        }

        public void IsDeletedFlag(bool isDelete)
        {
            IsDeleted = isDelete;
        }
    }
}
