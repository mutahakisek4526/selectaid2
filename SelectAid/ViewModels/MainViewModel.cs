using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using SelectAid.Models;
using SelectAid.Services;

namespace SelectAid.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly AppSettings _settings;
    private readonly ObservableCollection<Profile> _profiles;
    private readonly ObservableCollection<KeyboardLayout> _layouts;
    private readonly PhraseCatalog _phrases;
    private readonly HistoryLog _history;
    private readonly UserDictionary _dictionary;
    private readonly BackupService _backupService;
    private readonly StartupService _startupService = new();

    private ViewModelBase? _currentPage;
    private string _statusMessage = "";
    private Profile _currentProfile;

    public MainViewModel()
    {
        _settings = AppServices.Profiles.LoadSettings();
        _profiles = new ObservableCollection<Profile>(AppServices.Profiles.LoadProfiles());
        _layouts = new ObservableCollection<KeyboardLayout>(AppServices.Profiles.LoadKeyboardLayouts());
        _phrases = AppServices.Profiles.LoadPhrases();
        _history = AppServices.Profiles.LoadHistory();
        _dictionary = AppServices.Profiles.LoadUserDictionary();
        _backupService = new BackupService(AppServices.DataRoot);

        _currentProfile = AppServices.Profiles.GetCurrentProfile(_settings, _profiles.ToList());
        AppServices.Themes.ApplyTheme(_currentProfile.ThemeId);
        AppServices.Speech.ApplySettings(_currentProfile.Speech);

        Home = new HomeViewModel(this);
        Aac = new AacViewModel(this, _layouts, _phrases, _history, _dictionary);
        Phrases = new PhrasesViewModel(this, _phrases);
        KeyboardLayouts = new KeyboardLayoutsViewModel(this, _layouts);
        OverlayPanel = new OverlayPanelViewModel(this);
        Settings = new SettingsViewModel(this);
        Supporter = new SupporterViewModel(this, _profiles, _settings, _startupService);
        Training = new TrainingViewModel(this);
        Logs = new LogsViewModel(this);
        BackupRestore = new BackupRestoreViewModel(this, _backupService);

        Navigate(Home);
    }

    public HomeViewModel Home { get; }
    public AacViewModel Aac { get; }
    public PhrasesViewModel Phrases { get; }
    public KeyboardLayoutsViewModel KeyboardLayouts { get; }
    public OverlayPanelViewModel OverlayPanel { get; }
    public SettingsViewModel Settings { get; }
    public SupporterViewModel Supporter { get; }
    public TrainingViewModel Training { get; }
    public LogsViewModel Logs { get; }
    public BackupRestoreViewModel BackupRestore { get; }

    public ViewModelBase? CurrentPage
    {
        get => _currentPage;
        set => SetProperty(ref _currentPage, value);
    }

    public Profile CurrentProfile
    {
        get => _currentProfile;
        set
        {
            SetProperty(ref _currentProfile, value);
            _settings.CurrentProfileId = value.Id;
            AppServices.Themes.ApplyTheme(value.ThemeId);
            AppServices.Speech.ApplySettings(value.Speech);
            SaveAll();
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public string InputModeLabel => CurrentProfile.InputMode.ToString();

    public void Navigate(ViewModelBase page)
    {
        CurrentPage = page;
        StatusMessage = $"{page.GetType().Name.Replace("ViewModel", string.Empty)}";
    }

    public void SaveAll()
    {
        AppServices.Profiles.SaveSettings(_settings);
        AppServices.Profiles.SaveProfiles(_profiles.ToList());
        AppServices.Profiles.SaveKeyboardLayouts(_layouts.ToList());
        AppServices.Profiles.SavePhrases(_phrases);
        AppServices.Profiles.SaveHistory(_history);
        AppServices.Profiles.SaveUserDictionary(_dictionary);
    }

    public void Log(string message, Exception? ex = null)
    {
        AppServices.Logger.Log("INFO", message, ex);
    }

    public string GetLogText()
    {
        var logFile = Path.Combine(AppServices.DataRoot, "log.txt");
        return File.Exists(logFile) ? File.ReadAllText(logFile) : string.Empty;
    }

    public AppSettings SettingsData => _settings;
    public ObservableCollection<Profile> Profiles => _profiles;
}
