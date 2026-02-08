using SelectAid.Models;

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
        _main.SaveAll();
    }
}
