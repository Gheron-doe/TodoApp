using TodoAppBL.Models;

namespace TodoAppBL.Interfaces
{
    public interface ITodoTaskRepository
    {
        List<TodoTask> GetAll();
        TodoTask? GetById(string id);
        bool Exists(TodoTask task);
        void Add(TodoTask task);
        void Update(TodoTask task);
    }
}
