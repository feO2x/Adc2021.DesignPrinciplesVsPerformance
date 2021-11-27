using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using LightInject;

namespace SyncVsAsync.WpfClient;

public sealed partial class App
{
    private ServiceContainer Container { get; } = new ();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        Container.Register<MainWindow>()
                 .Register<MainWindowViewModel>()
                 .Register<WebApiPerformanceManager>()
                 .RegisterInstance(httpClient);

        MainWindow = Container.GetInstance<MainWindow>();
        MainWindow!.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Container.Dispose();
        base.OnExit(e);
    }

    protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
    {
        Container.Dispose();
        base.OnSessionEnding(e);
    }
}