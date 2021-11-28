using System;
using System.Windows;
using System.Windows.Threading;
using LightInject;
using Serilog;
using Synnotech.DatabaseAbstractions;

namespace WpfDataGrid;

public sealed partial class App
{
    public App() =>
        DispatcherUnhandledException += OnDispatcherUnhandledException;

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        var logger = Container.GetInstance<ILogger>();
        logger.Fatal(e.Exception, "An unexpected error occurred");
        MessageBox.Show("An unexpected error occurred. The app will now shut down. Please restart the app and look at the log files if this error happens again.",
                        "Unexpected Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
        Shutdown(-1);
    }

    private ServiceContainer Container { get; } = new (new ContainerOptions
    {
        EnablePropertyInjection = false,
        EnableVariance = false
    });

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Container.RegisterInstance(Logging.CreateLogger())
                 .RegisterInstance(new Random())
                 .RegisterSingleton<ISessionFactory<IGetContactsSession>>(c => InMemoryContactsSessionFactory.CreateDefault(c.GetInstance<Random>()))
                 .RegisterSingleton<MainWindow>()
                 .RegisterSingleton<MainWindowViewModel>();

        MainWindow = Container.GetInstance<MainWindow>();
        MainWindow.Show();
        var mainWindowViewModel = Container.GetInstance<MainWindowViewModel>();
        mainWindowViewModel.GetContacts();
    }
}