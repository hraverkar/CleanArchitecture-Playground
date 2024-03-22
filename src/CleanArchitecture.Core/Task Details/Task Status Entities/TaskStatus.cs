using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Task.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Task_Details.Task_Status_Entities
{
    public sealed class TaskStatus : AggregateRoot
    {
        public string StatusName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        private TaskStatus(string statusName, string createdBy, DateTime createdAt, bool isDeleted)
        {
            StatusName = statusName;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            IsDeleted = isDeleted;
        }

#pragma warning disable CS8618 // this is needed for the ORM for serializing Value Objects
        private TaskStatus()
#pragma warning restore CS8618
        {

        }

        public static TaskStatus Create(string statusName, string createdBy, DateTime createdAt, bool isDeleted)
        {

            statusName = (statusName ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(statusName, nameof(StatusName));
            createdBy = (createdBy ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(createdBy, nameof(CreatedBy));
            return new TaskStatus(statusName, createdBy, createdAt, isDeleted);
        }

        public void IsDeletedFlag(bool isDelete)
        {
            IsDeleted = isDelete;
        }
    }
}
