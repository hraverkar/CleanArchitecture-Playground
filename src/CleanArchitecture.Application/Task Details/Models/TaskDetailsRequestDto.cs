namespace CleanArchitecture.Application.Task_Details.Models
{
    public sealed class TaskDetailsRequestDto
    {
        public string TaskTitle { get; set; }
        public string TaskDetail { get; set; }
        public string TaskAssignTo { get; set; }
        public Guid TaskStatusId { get; set; }
        public DateTime TaskCreatedAt { get; set; }
        public string TaskCreatedBy { get; set; }
        public Guid ProjectId { get; set; }
    }
}
