using Microsoft.Extensions.Logging;
using TodoApp.Pages;
using TodoApp.Services;
using TodoApp.ViewModels;
using TodoAppBL.Interfaces;
using TodoAppBL.Services;
using TodoAppBL.Strategies;
using TodoAppDL;

namespace TodoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //  DL:
            builder.Services.AddSingleton<DatabaseConnection>();
            builder.Services.AddSingleton<ITodoTaskRepository, LiteDBTodoTaskRepository>();
            builder.Services.AddSingleton<IPersonRepository, LiteDBPersonRepository>();

            // BL: services + messages
            builder.Services.AddSingleton<MessageService>();
            builder.Services.AddTransient<TodoTaskService>();
            builder.Services.AddTransient<PersonService>();

            //  Strategy
            builder.Services.AddTransient<ITodoTaskFilterStrategy, ShowAllFilterStrategy>();
            builder.Services.AddTransient<ITodoTaskFilterStrategy, ShowUnfinishedFilterStrategy>();
            builder.Services.AddTransient<ITodoTaskFilterStrategy, ShowAllByRecentFilterStrategy>();

            // UI:
            builder.Services.AddTransient<NavigationService>();

            builder.Services.AddTransient<TaskListViewModel>();
            builder.Services.AddTransient<TaskDetailViewModel>();
            builder.Services.AddTransient<PersonListViewModel>();
            builder.Services.AddTransient<PersonDetailViewModel>();

            builder.Services.AddTransient<TaskListPage>();
            builder.Services.AddTransient<TaskDetailPage>();
            builder.Services.AddTransient<PersonListPage>();
            builder.Services.AddTransient<PersonDetailPage>();

            // Routes
            Routing.RegisterRoute(nameof(TaskDetailPage), typeof(TaskDetailPage));
            Routing.RegisterRoute(nameof(PersonListPage), typeof(PersonListPage));
            Routing.RegisterRoute(nameof(PersonDetailPage), typeof(PersonDetailPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
