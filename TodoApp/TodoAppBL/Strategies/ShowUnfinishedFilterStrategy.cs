using TodoAppBL.Interfaces;
using TodoAppBL.Models;

namespace TodoAppBL.Strategies
{
    public class ShowUnfinishedFilterStrategy : ITodoTaskFilterStrategy
    {
        public string DisplayName => "Toon niet afgewerkt";

        public IEnumerable<TodoTask> Apply(IEnumerable<TodoTask> tasks)
            => tasks.Where(t => !t.IsDone);
    }
}
