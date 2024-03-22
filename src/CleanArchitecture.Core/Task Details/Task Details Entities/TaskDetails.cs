using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;

namespace CleanArchitecture.Core.Task.Entities
{
    public sealed class TaskDetails : AggregateRoot
    {
        public string TaskTitle { get; set; }
        public string TaskDetail { get; set; }
        public string TaskAssignTo { get; set; }
        public string TaskStatus { get; set; }
        public DateTime TaskCreatedAt { get; set; } = DateTime.Now;
        public string TaskCreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

        private TaskDetails(string taskTitle, string taskDetail, string taskAssignTo, string taskStatus, DateTime taskCreatedAt, string taskCreatedBy, bool isDeleted)
        {
            TaskTitle = taskTitle;
            TaskDetail = taskDetail;
            TaskAssignTo = taskAssignTo;
            TaskStatus = taskStatus;
            TaskCreatedAt = taskCreatedAt;
            TaskCreatedBy = taskCreatedBy;
            IsDeleted = isDeleted;
        }

#pragma warning disable CS8618 // this is needed for the ORM for serializing Value Objects
        private TaskDetails()
#pragma warning restore CS8618
        {

        }

        public static TaskDetails Create(string taskTitle, string taskDetail, string taskAssignTo, string taskStatus, DateTime taskCreatedAt, string taskCreatedBy, bool isDeleted)
        {

            taskTitle = (taskTitle ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(taskTitle, nameof(TaskTitle));
            taskDetail = (taskDetail ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(taskDetail, nameof(TaskDetail));
            taskAssignTo = (taskAssignTo ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(taskAssignTo, nameof(TaskAssignTo));
            taskStatus = (taskStatus ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(taskStatus, nameof(TaskStatus));
            taskCreatedBy = (taskCreatedBy ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(taskCreatedBy, nameof(TaskCreatedBy));

            return new TaskDetails(taskTitle, taskDetail, taskAssignTo, taskStatus, taskCreatedAt, taskCreatedBy, isDeleted);
        }

        public void IsDeletedFlag(bool isDelete)
        {
            IsDeleted = isDelete;
        }
    }
}
