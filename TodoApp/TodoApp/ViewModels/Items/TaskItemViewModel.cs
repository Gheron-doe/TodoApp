using TodoApp.ViewModels.Base;
using TodoAppBL.Services;

namespace TodoApp.ViewModels.Items
{
    public class TaskItemViewModel : ViewModel
    {
        private TodoTaskService TaskService { get; }

        public string Id { get; internal set; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set
            {
                if (_isDone == value) return;

                _isDone = value;
                NotifyPropertyChanged();

                TaskService.ToggleDone(Id, value);
            }
        }

        public DateTime ModifiedAt { get; set; }

        public TaskItemViewModel(TodoTaskService taskService)
        {
            TaskService = taskService;
        }

        public void SetIsDoneSilent(bool value)
        {
            if (_isDone == value) return;
            _isDone = value;
            NotifyPropertyChanged(nameof(IsDone));
        }
    }
}
