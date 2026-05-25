using System.Collections.ObjectModel;
using System.Windows.Input;
using TodoApp.Pages;
using TodoApp.Services;
using TodoApp.ViewModels.Base;
using TodoApp.ViewModels.Items;
using TodoAppBL.Messages;
using TodoAppBL.Models;
using TodoAppBL.Services;

namespace TodoApp.ViewModels
{
    public class PersonListViewModel : ViewModel
    {
        public NavigationService NavigationService { get; }

        public ObservableCollection<PersonItemViewModel> Persons { get; }

        private PersonItemViewModel _selectedPerson;
        public PersonItemViewModel SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                NotifyPropertyChanged();

                if (value != null)
                {
                    GoToPersonDetailAsync(value).ConfigureAwait(false);

                    _selectedPerson = null;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand NewPersonCommand { get; init; }

        public PersonListViewModel(
            PersonService personService,
            NavigationService navigationService,
            MessageService messageService)
        {
            NavigationService = navigationService;

            Persons = new ObservableCollection<PersonItemViewModel>(
                personService.GetAll().Select(ConvertToViewModel));

            NewPersonCommand = new Command(async () =>
                await NavigationService.GoToAsync(nameof(PersonDetailPage)));

            messageService.Register<PersonCreatedMessage>(this, (sender, message) =>
            {
                Persons.Add(ConvertToViewModel(message.Person));
            });

            messageService.Register<PersonUpdatedMessage>(this, (sender, message) =>
            {
                var personvm = Persons.FirstOrDefault(p => p.Id == message.Person.Id);
                if (personvm == null) return;

                personvm.FirstName = message.Person.FirstName;
                personvm.LastName = message.Person.LastName;
                personvm.Age = message.Person.Age;
            });

            messageService.Register<PersonDeletedMessage>(this, (sender, message) =>
            {
                var personvm = Persons.FirstOrDefault(p => p.Id == message.PersonId);
                if (personvm != null) Persons.Remove(personvm);
            });
        }

        private PersonItemViewModel ConvertToViewModel(Person person)
        {
            return new PersonItemViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age
            };
        }
        private async Task GoToPersonDetailAsync(PersonItemViewModel value)
        {
            await NavigationService.GoToPersonDetail(value.Id);
        }
    }
}
