using Avalonia.Controls;
using Avalonia.Input;
using FastKillTarkovProcess.ViewModels;

namespace FastKillTarkovProcess.Views
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow()
        {
            ViewModel = App.GetRequiredService<MainWindowViewModel>();
            DataContext = ViewModel;

            InitializeComponent();
        }

        private void AppInfoTextBlock_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            ViewModel.OpenAuthorURLCommand.Execute(null);
        }

        private void MainWindow_OnClosing(object? sender, WindowClosingEventArgs e)
        {
            ViewModel.ClosingCommand.Execute(null);
        }
    }
}