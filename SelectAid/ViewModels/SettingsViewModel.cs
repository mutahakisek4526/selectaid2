using SelectAid.Models;
using SelectAid.Services;

namespace SelectAid.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly MainViewModel _main;

    public SettingsViewModel(MainViewModel main)
    {
        _main = main;
    }

    public Profile Profile => _main.CurrentProfile;

    public void ToggleHighContrast()
    {
        Profile.Ui.HighContrast = !Profile.Ui.HighContrast;
        AppServices.Themes.ApplyTheme(Profile.ThemeId, Profile.Ui.HighContrast);
        _main.SaveAll();
    }
}
