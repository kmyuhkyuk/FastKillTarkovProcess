using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using FastKillTarkovProcess.ViewModels;

namespace FastKillTarkovProcess.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext!;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnabledShortcutButton_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
        {
            ViewModel.ChangeEnabledShortcutCommandCommand.Execute(((CheckBox)e.Source!).IsChecked);
        }

        private void AppInfoTextBlock_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            ViewModel.OpenAuthorURLCommandCommand.Execute(null);
        }

        private void MainWindow_OnClosing(object? sender, WindowClosingEventArgs e)
        {
            ViewModel.ClosingCommand.Execute(null);
        }
    }
}