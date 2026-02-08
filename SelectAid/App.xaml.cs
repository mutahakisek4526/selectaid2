using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SelectAid.Services;

namespace SelectAid;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DispatcherUnhandledException += (_, args) =>
        {
            AppServices.Logger.Log("ERROR", "UI thread exception", args.Exception);
            args.Handled = true;
        };
        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            AppServices.Logger.Log("ERROR", "Task exception", args.Exception);
            args.SetObserved();
        };

        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
        {
            var settings = AppServices.Profiles.LoadSettings();
            settings.SafeModeRequested = true;
            AppServices.Profiles.SaveSettings(settings);
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppServices.Metrics.Flush();
        base.OnExit(e);
    }
}
