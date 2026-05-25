using TodoAppBL.Interfaces;
using TodoAppBL.Models;

namespace TodoAppBL.Strategies
{
    public class ShowAllFilterStrategy : ITodoTaskFilterStrategy
    {
        public string DisplayName => "Toon alles";

        public IEnumerable<TodoTask> Apply(IEnumerable<TodoTask> tasks) => tasks;
    }
}
