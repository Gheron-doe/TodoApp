using TodoAppBL.Models;

namespace TodoAppBL.Messages
{
    public class TaskUpdatedMessage
    {
        public TodoTask Task { get; }

        public TaskUpdatedMessage(TodoTask task)
        {
            Task = task;
        }
    }
}
