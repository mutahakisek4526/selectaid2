namespace SelectAid.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly MainViewModel _main;

    public HomeViewModel(MainViewModel main)
    {
        _main = main;
    }

    public void GoAac() => _main.Navigate(_main.Aac);
    public void GoPhrases() => _main.Navigate(_main.Phrases);
    public void GoKeyboardLayouts() => _main.Navigate(_main.KeyboardLayouts);
    public void GoOverlay() => _main.Navigate(_main.OverlayPanel);
    public void GoSettings() => _main.Navigate(_main.Settings);
    public void GoSupporter() => _main.Navigate(_main.Supporter);
    public void GoTraining() => _main.Navigate(_main.Training);
    public void GoLogs() => _main.Navigate(_main.Logs);
    public void GoBackup() => _main.Navigate(_main.BackupRestore);
}
