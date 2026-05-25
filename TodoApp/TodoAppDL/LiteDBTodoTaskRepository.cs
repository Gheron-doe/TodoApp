using LiteDB;
using TodoAppBL.Interfaces;
using TodoAppBL.Models;

namespace TodoAppDL
{
    public class LiteDBTodoTaskRepository : ITodoTaskRepository
    {
        private readonly DatabaseConnection _db;

        public LiteDBTodoTaskRepository(DatabaseConnection db)
        {
            _db = db;
        }

        private ILiteCollection<TodoTask> GetCollection()
        {
            return _db.GetCollection<TodoTask>();
        }
        public List<TodoTask> GetAll()
        {
            return GetCollection().FindAll().ToList();
        }

        public TodoTask? GetById(string taskId)
        {
            return GetCollection().FindById(taskId);
        }
        public bool Exists(TodoTask task)
        {
            return GetCollection().Exists(x => x.Id == task.Id);
        }
        public void Add(TodoTask task)
        {
            GetCollection().Insert(task);
        }

        public void Update(TodoTask task)
        {
            GetCollection().Update(task);
        }

    }
}
