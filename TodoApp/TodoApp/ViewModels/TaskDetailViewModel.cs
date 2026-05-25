using System.Collections.ObjectModel;
using System.Windows.Input;
using TodoApp.Services;
using TodoApp.ViewModels.Base;
using TodoAppBL.Factories;
using TodoAppBL.Messages;
using TodoAppBL.Models;
using TodoAppBL.Services;

namespace TodoApp.ViewModels
{
    public class TaskDetailViewModel : ViewModel, IQueryAttributable
    {
        private TodoTaskService TaskService { get; }
        private PersonService PersonService { get; }
        public NavigationService NavigationService { get; }

        public TodoTask Task { get; private set; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(PageTitle));
                ((Command)SaveCommand).ChangeCanExecute();
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
                _isDone = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Person> Persons { get; }

        private Person _assignedPerson;
        public Person AssignedPerson
        {
            get => _assignedPerson;
            set
            {
                _assignedPerson = value;
                NotifyPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        public string PageTitle => Task == null
            ? "Nieuwe taak"
            : (string.IsNullOrWhiteSpace(Title) ? "Taak" : Title);

        public ICommand SaveCommand { get; init; }
        public ICommand CancelCommand { get; init; }

        public TaskDetailViewModel(
            TodoTaskService taskService,
            PersonService personService,
            NavigationService navigationService,
            MessageService messageService)
        {
            TaskService = taskService;
            PersonService = personService;
            NavigationService = navigationService;

            Persons = new ObservableCollection<Person>(PersonService.GetAll());

            SaveCommand = new Command(async () => await OnSaveAsync(), CanSave);
            CancelCommand = new Command(async () => await NavigationService.GoBackAsync());

            messageService.Register<PersonCreatedMessage>(this, (sender, message) =>
            {
                Persons.Add(message.Person);
            });
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("taskId", out var taskId) && taskId is string id && !string.IsNullOrEmpty(id))
            {
                Task = TaskService.GetById(id);
                if (Task != null)
                {
                    Title = Task.Title;
                    Description = Task.Description;
                    IsDone = Task.IsDone;
                    AssignedPerson = Persons.FirstOrDefault(p => p.Id == Task.AssignedPersonId);
                }
            }
        }

        private bool CanSave() => !string.IsNullOrWhiteSpace(Title) && AssignedPerson != null;

        private async Task OnSaveAsync()
        {
            if (!CanSave()) return;

            if (Task == null)
            {
                var newTask = TodoTaskFactory.Create(Title, Description, AssignedPerson.Id);
                newTask.IsDone = IsDone;
                TaskService.Add(newTask);
            }
            else
            {
                Task.Title = Title;
                Task.Description = Description;
                Task.IsDone = IsDone;
                Task.AssignedPersonId = AssignedPerson.Id;
                TaskService.Update(Task);
            }

            await NavigationService.GoBackAsync();
        }
    }
}
