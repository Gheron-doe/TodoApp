using TodoAppBL.Interfaces;
using TodoAppBL.Models;

namespace TodoAppBL.Strategies
{
    public class ShowAllByRecentFilterStrategy : ITodoTaskFilterStrategy
    {
        public string DisplayName => "Alles, meest recente eerst";

        public IEnumerable<TodoTask> Apply(IEnumerable<TodoTask> tasks)
            => tasks.OrderByDescending(t => t.ModifiedAt);
    }
}
