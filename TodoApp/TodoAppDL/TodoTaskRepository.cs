using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppBL.Models;

namespace TodoAppDL
{
    public class TodoTaskRepository
    {
        private List<TodoTask> _tasks;
        
        public List<TodoTask> GetAll()
        {
            return _tasks;
        }
        public TodoTask GetById(string taskId)
        {
            return _tasks.FirstOrDefault(x => x.Id == taskId);
        }
        public bool Exists(TodoTask task)
        {
            return _tasks.Contains(task);
        }
        public void Add(TodoTask task)
        {
            _tasks.Add(task);
        }
        public void Update(TodoTask task)
        {
            int index = _tasks.FindIndex(t => t.Id == task.Id);
            _tasks[index] = task;
        }
    }
}
