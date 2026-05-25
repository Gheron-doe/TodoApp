using System.Windows.Input;
using TodoApp.Services;
using TodoApp.ViewModels.Base;
using TodoAppBL.Factories;
using TodoAppBL.Models;
using TodoAppBL.Services;

namespace TodoApp.ViewModels
{
    public class PersonDetailViewModel : ViewModel, IQueryAttributable
    {
        private PersonService PersonService { get; }
        public NavigationService NavigationService { get; }

        public Person Person { get; private set; }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(PageTitle));
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _birthDate = DateTime.Today.AddYears(-18);
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                NotifyPropertyChanged();
            }
        }

        private string _photoUrl;
        public string PhotoUrl
        {
            get => _photoUrl;
            set
            {
                _photoUrl = value;
                NotifyPropertyChanged();
            }
        }

        public string PageTitle => Person == null
            ? "Nieuwe persoon"
            : (string.IsNullOrWhiteSpace(FirstName) ? "Persoon" : FirstName);

        public bool CanDelete => Person != null;

        public ICommand SaveCommand { get; init; }
        public ICommand DeleteCommand { get; init; }
        public ICommand CancelCommand { get; init; }

        public PersonDetailViewModel(
            PersonService personService,
            NavigationService navigationService)
        {
            PersonService = personService;
            NavigationService = navigationService;

            SaveCommand = new Command(async () => await OnSaveAsync(), CanSave);
            DeleteCommand = new Command(async () => await OnDeleteAsync());
            CancelCommand = new Command(async () => await NavigationService.GoBackAsync());
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("personId", out var raw) && raw is string personId && !string.IsNullOrEmpty(personId))
            {
                Person = PersonService.GetById(personId);
                if (Person != null)
                {
                    FirstName = Person.FirstName;
                    LastName = Person.LastName;
                    BirthDate = Person.BirthDate;
                    PhotoUrl = Person.PhotoUrl;
                }
            }

            NotifyPropertyChanged(nameof(CanDelete));
        }

        private bool CanSave() => !string.IsNullOrWhiteSpace(FirstName);

        private async Task OnSaveAsync()
        {
            if (!CanSave()) return;

            if (Person == null)
            {
                var newPerson = PersonFactory.Create(FirstName, LastName, BirthDate, PhotoUrl);
                PersonService.Add(newPerson);
            }
            else
            {
                Person.FirstName = FirstName;
                Person.LastName = LastName;
                Person.BirthDate = BirthDate;
                Person.PhotoUrl = PhotoUrl;
                PersonService.Update(Person);
            }

            await NavigationService.GoBackAsync();
        }

        private async Task OnDeleteAsync()
        {
            if (Person == null) return;

            PersonService.Delete(Person);
            await NavigationService.GoBackAsync();
        }
    }
}
