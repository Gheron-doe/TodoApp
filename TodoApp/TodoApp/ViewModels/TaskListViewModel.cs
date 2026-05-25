using System.Collections.ObjectModel;
using System.Windows.Input;
using TodoApp.Pages;
using TodoApp.Services;
using TodoApp.ViewModels.Base;
using TodoApp.ViewModels.Items;
using TodoAppBL.Interfaces;
using TodoAppBL.Messages;
using TodoAppBL.Models;
using TodoAppBL.Services;
using TodoAppBL.Strategies;

namespace TodoApp.ViewModels
{
    public class TaskListViewModel : ViewModel
    {
        private TodoTaskService TaskService { get; }
        public NavigationService NavigationService { get; }


        private readonly List<TaskItemViewModel> _allTasks = new();

        public ObservableCollection<FilterStrategyViewModel> Filters { get; }
        public ObservableCollection<TaskItemViewModel> VisibleTasks { get; }

        private FilterStrategyViewModel _selectedFilter;
        public FilterStrategyViewModel SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                NotifyPropertyChanged();

                foreach (var f in Filters)
                    f.IsSelected = (f == value);

                ApplyFilter();
            }
        }

        private TaskItemViewModel _selectedTask;
        public TaskItemViewModel SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                NotifyPropertyChanged();

                if (value != null)
                {
                    GoToTaskDetailAsync(value).ConfigureAwait(false);

                    _selectedTask = null;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand NewTaskCommand { get; init; }
        public ICommand GoToPersonsCommand { get; init; }
        public ICommand SelectFilterCommand { get; init; }

        public TaskListViewModel(
            TodoTaskService taskService,
            NavigationService navigationService,
            MessageService messageService,
            IEnumerable<ITodoTaskFilterStrategy> filterStrategies)
        {
            this.TaskService = taskService;
            NavigationService = navigationService;

            Filters = new ObservableCollection<FilterStrategyViewModel>(
                filterStrategies.Select(s => new FilterStrategyViewModel(s)));

            VisibleTasks = new ObservableCollection<TaskItemViewModel>();

            foreach (var task in this.TaskService.GetAll())
                _allTasks.Add(ConvertToViewModel(task));

            SelectedFilter = Filters.FirstOrDefault();

            NewTaskCommand = new Command(async () =>
                await NavigationService.GoToAsync(nameof(TaskDetailPage)));

            GoToPersonsCommand = new Command(async () =>
                await NavigationService.GoToAsync(nameof(PersonListPage)));

            SelectFilterCommand = new Command<FilterStrategyViewModel>(f => SelectedFilter = f);

            messageService.Register<TaskCreatedMessage>(this, (sender, message) =>
            {
                _allTasks.Add(ConvertToViewModel(message.Task));
                ApplyFilter();
            });

            messageService.Register<TaskUpdatedMessage>(this, (sender, message) =>
            {
                var taskvm = _allTasks.FirstOrDefault(t => t.Id == message.Task.Id);
                if (taskvm == null) return;

                taskvm.Title = message.Task.Title;
                taskvm.Description = message.Task.Description;
                taskvm.ModifiedAt = message.Task.ModifiedAt;
                taskvm.SetIsDoneSilent(message.Task.IsDone);

                ApplyFilter();
            });
        }

        private TaskItemViewModel ConvertToViewModel(TodoTask task)
        {
            var vm = new TaskItemViewModel(TaskService)
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                ModifiedAt = task.ModifiedAt
            };
            vm.SetIsDoneSilent(task.IsDone);
            return vm;
        }

        private void ApplyFilter()
        {
            var strategy = SelectedFilter?.Strategy ?? new ShowAllFilterStrategy();

            var domain = _allTasks.Select(vm => new TodoTask
            {
                Id = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                IsDone = vm.IsDone,
                ModifiedAt = vm.ModifiedAt
            });

            var filteredIds = strategy.Apply(domain).Select(t => t.Id).ToList();
            var lookup = _allTasks.ToDictionary(vm => vm.Id);

            VisibleTasks.Clear();
            foreach (var id in filteredIds)
                if (lookup.TryGetValue(id, out var vm))
                    VisibleTasks.Add(vm);
        }
        private async Task GoToTaskDetailAsync(TaskItemViewModel value)
        {
            await NavigationService.GoToTaskDetail(value.Id);
        }
    }
}
