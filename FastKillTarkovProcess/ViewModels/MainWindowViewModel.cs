using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FastKillTarkovProcess.Enums;
using FastKillTarkovProcess.Interfaces;
using FastKillTarkovProcess.Models;
using FastKillTarkovProcess.Services;
using SharpHook;
using SharpHook.Native;

namespace FastKillTarkovProcess.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private AppService _appService;

        private readonly KillTarkovProcessService _killTarkovProcessService;

        private IShortcut _shortcut;

        [ObservableProperty] private bool _isEnabledShortcut;

        [ObservableProperty] private string _shortcutName;

        private readonly string _shortcutJsonPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Shortcut.json");

        private readonly TaskPoolGlobalHook _taskPoolGlobalHook;

        private bool _isBindingShortcut;

        public MainWindowViewModel(AppService appService, KillTarkovProcessService killTarkovProcessService)
        {
            _appService = appService;
            _killTarkovProcessService = killTarkovProcessService;

            _isEnabledShortcut = true;

            _shortcut = File.Exists(_shortcutJsonPath)
                ? ShortcutModel.Deserialize(File.ReadAllText(_shortcutJsonPath))
                : ShortcutModel.None;

            _shortcutName = _shortcut.Name;

            _taskPoolGlobalHook = new TaskPoolGlobalHook();

            _taskPoolGlobalHook.KeyPressed += OnKeyPressed;
            _taskPoolGlobalHook.MousePressed += OnMousePressed;

            _taskPoolGlobalHook.RunAsync();
        }

        private void OnKeyPressed(object? sender, KeyboardHookEventArgs e)
        {
            if (_isBindingShortcut)
            {
                _shortcut = new ShortcutModel(e.Data.KeyCode);

                return;
            }

            if (IsEnabledShortcut && _shortcut.InputDevice == InputDevice.Keyboard &&
                e.Data.KeyCode == (KeyCode)_shortcut.Shortcut)
            {
                _killTarkovProcessService.KillTarkovProcess();
            }
        }

        private void OnMousePressed(object? sender, MouseHookEventArgs e)
        {
            if (_isBindingShortcut && e.Data.Button is not MouseButton.Button1 and MouseButton.Button2)
            {
                _shortcut = new ShortcutModel(e.Data.Button);

                return;
            }

            if (IsEnabledShortcut && _shortcut.InputDevice == InputDevice.Mouse &&
                e.Data.Button == (MouseButton)_shortcut.Shortcut)
            {
                _killTarkovProcessService.KillTarkovProcess();
            }
        }

        [RelayCommand]
        private async Task OnBindShortcut()
        {
            ShortcutName = "...";
            _shortcut = ShortcutModel.None;

            _isBindingShortcut = true;

            while (_shortcut.IsNone)
                await Task.Delay(50);

            _isBindingShortcut = false;

            ShortcutName = _shortcut.Name;

            await File.WriteAllTextAsync(_shortcutJsonPath, _shortcut.Serialize());
        }

        [RelayCommand]
        private void OnChangeEnabledShortcut(bool? parameter)
        {
            IsEnabledShortcut = (bool)parameter!;
        }

        [RelayCommand]
        private void OnOpenAuthorURL()
        {
            Process.Start("explorer", AppService.AppURL);
        }

        [RelayCommand]
        private void OnClosing()
        {
            _taskPoolGlobalHook.Dispose();
        }
    }
}