using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
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
    private string _errorMessage = "";
    private string _scanStatus = "L1";
    private string _scanTarget = "";
    private bool _isPaused;
    private int _dwellProgress;
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
        AppServices.Themes.ApplyTheme(_currentProfile.ThemeId, _currentProfile.Ui.HighContrast);
        AppServices.Speech.ApplySettings(_currentProfile.Speech);
        AppServices.InputCoordinator.Configure(_currentProfile);
        AppServices.TimingController.Configure(_currentProfile.Dwell);
        AppServices.ScanEngine.Configure(_currentProfile.Scan);
        ApplyInputMode(_currentProfile.InputMode);

        AppServices.InputRouter.ActionReceived += OnInputAction;
        AppServices.ScanEngine.HighlightChanged += state => Application.Current?.Dispatcher.BeginInvoke(() => UpdateScanState(state));
        AppServices.ScanEngine.AutoStopped += () => Application.Current?.Dispatcher.BeginInvoke(() => StatusMessage = "Scan Auto Stop");
        AppServices.TimingController.DwellProgressChanged += progress => DwellProgress = progress;

        Home = new HomeViewModel(this);
        Aac = new AacViewModel(this, _layouts, _history, _dictionary);
        Phrases = new PhrasesViewModel(this, _phrases);
        KeyboardLayouts = new KeyboardLayoutsViewModel(this, _layouts);
        OverlayPanel = new OverlayPanelViewModel(this);
        Settings = new SettingsViewModel(this);
        Supporter = new SupporterViewModel(this, _profiles, _settings, _startupService);
        Training = new TrainingViewModel(this);
        Logs = new LogsViewModel(this);
        BackupRestore = new BackupRestoreViewModel(this, _backupService);

        if (_settings.SafeModeRequested)
        {
            _settings.SafeModeRequested = false;
            SaveAll();
            Navigate(Supporter);
        }
        else
        {
            Navigate(Home);
        }
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
            AppServices.Themes.ApplyTheme(value.ThemeId, value.Ui.HighContrast);
            AppServices.Speech.ApplySettings(value.Speech);
            AppServices.InputCoordinator.Configure(value);
            AppServices.TimingController.Configure(value.Dwell);
            AppServices.ScanEngine.Configure(value.Scan);
            ApplyInputMode(value.InputMode);
            SaveAll();
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public string ScanStatus
    {
        get => _scanStatus;
        set => SetProperty(ref _scanStatus, value);
    }

    public string ScanTarget
    {
        get => _scanTarget;
        set => SetProperty(ref _scanTarget, value);
    }

    public bool IsPaused
    {
        get => _isPaused;
        set => SetProperty(ref _isPaused, value);
    }

    public int DwellProgress
    {
        get => _dwellProgress;
        set => SetProperty(ref _dwellProgress, value);
    }

    public string InputModeLabel => CurrentProfile.InputMode switch
    {
        InputMode.EyeOnly => "Eye",
        InputMode.EyeSwitch => "Eye+Switch",
        InputMode.EyeGyro => "Eye+Gyro",
        InputMode.GyroOnly => "Gyro",
        InputMode.SwitchScan => "Scan",
        _ => "Eye"
    };

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

    public void ApplyInputMode(InputMode mode)
    {
        if (mode == InputMode.SwitchScan)
        {
            AppServices.ScanEngine.Start();
        }
        else
        {
            AppServices.ScanEngine.Stop();
        }
        OnPropertyChanged(nameof(InputModeLabel));
    }

    private void OnInputAction(Input.InputAction action)
    {
        Application.Current?.Dispatcher.BeginInvoke(() =>
        {
            switch (action)
            {
                case Input.InputAction.PauseToggle:
                    IsPaused = AppServices.InputRouter.IsPaused;
                    StatusMessage = IsPaused ? "Pause" : "Running";
                    AppServices.Metrics.Increment("pause_toggle");
                    break;
                case Input.InputAction.EmergencyStop:
                    Navigate(Home);
                    AppServices.Metrics.Increment("emergency_stop");
                    break;
                case Input.InputAction.Confirm:
                    if (CurrentProfile.InputMode == InputMode.SwitchScan)
                    {
                        AppServices.ScanEngine.Confirm();
                    }
                    AppServices.Metrics.Increment("confirm");
                    break;
                case Input.InputAction.Cancel:
                    if (CurrentProfile.InputMode == InputMode.SwitchScan)
                    {
                        AppServices.ScanEngine.Cancel();
                    }
                    AppServices.Metrics.Increment("cancel");
                    break;
            }
        });
    }

    private void UpdateScanState(Scan.ScanState state)
    {
        ScanStatus = $"L{state.Level}";
        ScanTarget = state.Label;
    }
}
