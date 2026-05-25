using TodoAppBL.Interfaces;
using TodoAppBL.Messages;
using TodoAppBL.Models;

namespace TodoAppBL.Services
{
    public class TodoTaskService(ITodoTaskRepository todoTaskRepository, MessageService messageService)
    {
        public MessageService MessageService { get; } = messageService;
        private ITodoTaskRepository _repository { get; } = todoTaskRepository;

        public List<TodoTask> GetAll()
        {
            return _repository.GetAll();
        }

        public TodoTask? GetById(string id)
        {
            return _repository.GetById(id);
        }

        public void Add(TodoTask task)
        {
            ArgumentNullException.ThrowIfNull(task);
            var now = DateTime.Now;

            if (_repository.Exists(task)) return;

            task.CreatedAt = now;
            task.ModifiedAt = now;
            _repository.Add(task);
            MessageService.Send(new TaskCreatedMessage(task));
        }

        public void Update(TodoTask task)
        {
            ArgumentNullException.ThrowIfNull(task);

            task.ModifiedAt = DateTime.Now;

            _repository.Update(task);

            MessageService.Send(new TaskUpdatedMessage(task));
        }

        public void ToggleDone(string taskId, bool isDone)
        {
            var task = _repository.GetById(taskId);
            if (task == null) return;

            task.IsDone = isDone;
            task.ModifiedAt = DateTime.Now;

            _repository.Update(task);
            MessageService.Send(new TaskUpdatedMessage(task));
        }
    }
}
