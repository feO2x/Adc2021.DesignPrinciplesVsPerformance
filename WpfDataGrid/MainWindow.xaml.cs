namespace WpfDataGrid;

public sealed partial class MainWindow
{
    public MainWindow() => InitializeComponent();

    public MainWindow(MainWindowViewModel viewModel) : this() => DataContext = viewModel;
}