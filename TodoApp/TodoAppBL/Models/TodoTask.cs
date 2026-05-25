namespace TodoAppBL.Models
{
    public class TodoTask
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public bool IsDone { get; set; }

        public string? AssignedPersonId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
