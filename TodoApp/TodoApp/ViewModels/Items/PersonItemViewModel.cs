using TodoApp.ViewModels.Base;

namespace TodoApp.ViewModels.Items
{
    public class PersonItemViewModel : ViewModel
    {
        public string Id { get; internal set; }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChanged();
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

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                NotifyPropertyChanged();
            }
        }
    }
}
