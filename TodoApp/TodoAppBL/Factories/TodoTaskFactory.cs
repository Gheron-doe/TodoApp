using TodoAppBL.Models;

namespace TodoAppBL.Factories
{
    public static class TodoTaskFactory
    {
        public static TodoTask Create(string title, string description = "", string? assignedPersonId = null)
        {
            var now = DateTime.Now;
            return new TodoTask
            {
                Title = title,
                Description = description,
                AssignedPersonId = assignedPersonId,
                IsDone = false,
                CreatedAt = now,
                ModifiedAt = now
            };
        }
    }
}
