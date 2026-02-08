using System.Collections.ObjectModel;
using System.Linq;
using SelectAid.Models;
using SelectAid.Services;

namespace SelectAid.ViewModels;

public class SupporterViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    private readonly AppSettings _settings;
    private readonly StartupService _startupService;

    public SupporterViewModel(MainViewModel main, ObservableCollection<Profile> profiles, AppSettings settings, StartupService startupService)
    {
        _main = main;
        Profiles = profiles;
        _settings = settings;
        _startupService = startupService;
    }

    public ObservableCollection<Profile> Profiles { get; }

    public Profile CurrentProfile
    {
        get => _main.CurrentProfile;
        set => _main.CurrentProfile = value;
    }

    public bool AutoStartEnabled
    {
        get => _settings.AutoStartEnabled;
        set
        {
            _settings.AutoStartEnabled = value;
            _startupService.SetAutoStart(value, "SelectAid", System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty);
            _main.SaveAll();
        }
    }

    public string ThemeId
    {
        get => _main.CurrentProfile.ThemeId;
        set
        {
            _main.CurrentProfile.ThemeId = value;
            AppServices.Themes.ApplyTheme(value, _main.CurrentProfile.Ui.HighContrast);
            _main.SaveAll();
        }
    }

    public InputMode InputMode
    {
        get => _main.CurrentProfile.InputMode;
        set
        {
            _main.CurrentProfile.InputMode = value;
            _main.ApplyInputMode(value);
            _main.SaveAll();
        }
    }

    public SafetySettings Safety => _main.CurrentProfile.Safety;

    public void Shutdown()
    {
        if (Safety.AllowShutdown)
        {
            AppServices.PcControl.Shutdown();
        }
    }

    public void Restart()
    {
        if (Safety.AllowRestart)
        {
            AppServices.PcControl.Restart();
        }
    }

    public void Sleep()
    {
        if (Safety.AllowSleep)
        {
            AppServices.PcControl.Sleep();
        }
    }

    public void Logoff()
    {
        if (Safety.AllowLogoff)
        {
            AppServices.PcControl.Logoff();
        }
    }

    public void AddProfile(string name)
    {
        var profile = new Profile { Name = name };
        Profiles.Add(profile);
        _main.SaveAll();
    }

    public void RemoveProfile(Profile profile)
    {
        if (Profiles.Count <= 1)
        {
            return;
        }

        Profiles.Remove(profile);
        if (!_main.Profiles.Any(p => p.Id == _main.SettingsData.CurrentProfileId))
        {
            _main.CurrentProfile = Profiles.First();
        }

        _main.SaveAll();
    }
}
