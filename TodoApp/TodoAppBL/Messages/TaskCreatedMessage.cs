using TodoAppBL.Models;

namespace TodoAppBL.Messages
{
    public class TaskCreatedMessage
    {
        public TodoTask Task { get; }

        public TaskCreatedMessage(TodoTask task)
        {
            Task = task;
        }
    }
}
