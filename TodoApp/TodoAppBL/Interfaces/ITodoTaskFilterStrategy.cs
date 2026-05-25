using TodoAppBL.Models;

namespace TodoAppBL.Interfaces
{
    public interface ITodoTaskFilterStrategy
    {
        string DisplayName { get; }

        IEnumerable<TodoTask> Apply(IEnumerable<TodoTask> tasks);
    }
}
