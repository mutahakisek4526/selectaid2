namespace SelectAid.ViewModels;

public class LogsViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    private string _logText = string.Empty;

    public LogsViewModel(MainViewModel main)
    {
        _main = main;
        Refresh();
    }

    public string LogText
    {
        get => _logText;
        set => SetProperty(ref _logText, value);
    }

    public void Refresh()
    {
        LogText = _main.GetLogText();
    }
}
