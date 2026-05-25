using TodoApp.Pages;

namespace TodoApp.Services
{
    public class NavigationService 
    {
        public async Task GoBackAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }
        public async Task GoToAsync(string routeName, IDictionary<string, object> parameters = null)
        {
            if (parameters is null)
                await Shell.Current.GoToAsync(routeName);
            else
                await Shell.Current.GoToAsync(routeName, parameters);
        }
 
        public async Task GoToTaskDetail(string taskId)
        {
            await GoToAsync(nameof(TaskDetailPage), new Dictionary<string, object>
            {
                { "taskId", taskId }
            });
        }
        public async Task GoToPersonDetail(string personId)
        {
            await GoToAsync(nameof(PersonDetailPage), new Dictionary<string, object>
            {
                { "personId", personId }
            });
        }
    }
}
