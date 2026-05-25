using TodoApp.ViewModels.Base;
using TodoAppBL.Interfaces;
using TodoAppBL.Strategies;

namespace TodoApp.ViewModels.Items
{
    public class FilterStrategyViewModel : ViewModel
    {
        public ITodoTaskFilterStrategy Strategy { get; init; }

        public string DisplayName => Strategy.DisplayName;

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyPropertyChanged();
            }
        }

        public FilterStrategyViewModel(ITodoTaskFilterStrategy strategy)
        {
            Strategy = strategy;
        }
    }
}
