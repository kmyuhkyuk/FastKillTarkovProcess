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

            Closing += (sender, args) => ViewModel.OnClosing();
        }

        private void EnabledShortcutButton_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
        {
            ViewModel.ChangeEnabledShortcutCommandCommand.Execute(((CheckBox)e.Source!).IsChecked);
        }

        private void AppInfoTextBlock_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            ViewModel.OpenAuthorURLCommandCommand.Execute(null);
        }
    }
}