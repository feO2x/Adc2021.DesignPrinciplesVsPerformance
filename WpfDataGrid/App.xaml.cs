using System.Windows;
using LightInject;

namespace WpfDataGrid;

public sealed partial class App
{
    private ServiceContainer Container { get; } = new (new ContainerOptions
    {
        EnablePropertyInjection = false,
        EnableVariance = false
    });

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Container.RegisterSingleton<MainWindow>()
                 .RegisterSingleton<MainWindowViewModel>();

        MainWindow = Container.GetInstance<MainWindow>();
        MainWindow.Show();
    }
}