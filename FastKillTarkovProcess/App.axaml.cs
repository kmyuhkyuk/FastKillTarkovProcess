using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using FastKillTarkovProcess.Services;
using FastKillTarkovProcess.ViewModels;
using FastKillTarkovProcess.Views;
using Microsoft.Extensions.DependencyInjection;

namespace FastKillTarkovProcess
{
    public partial class App : Application
    {
        private static ServiceProvider? _serviceProvider;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var collection = new ServiceCollection();

            collection.AddSingleton<AppService>();
            collection.AddSingleton<KillTarkovProcessService>();

            collection.AddSingleton<MainWindow>();
            collection.AddSingleton<MainWindowViewModel>();

            _serviceProvider = collection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            }

            base.OnFrameworkInitializationCompleted();
        }

        public static T GetService<T>() where T : class
        {
            return _serviceProvider!.GetService<T>()!;
        }

        public static T GetRequiredService<T>() where T : class
        {
            return _serviceProvider!.GetRequiredService<T>();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}