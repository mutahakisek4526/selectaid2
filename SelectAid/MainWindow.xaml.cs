using System.Media;
using System.Windows;
using SelectAid.Models;
using SelectAid.Services;
using SelectAid.ViewModels;
using UITimer = System.Timers.Timer;

namespace SelectAid;

public partial class MainWindow : Window
{
    private readonly UITimer _buzzerTimer;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        _buzzerTimer = new UITimer(300);
        _buzzerTimer.AutoReset = false;
        _buzzerTimer.Elapsed += (_, _) => Dispatcher.BeginInvoke(() => Background = (System.Windows.Media.Brush)FindResource("AppBackground"));
    }

    private MainViewModel ViewModel => (MainViewModel)DataContext;

    private void OnHome(object sender, RoutedEventArgs e)
    {
        ViewModel.Navigate(ViewModel.Home);
    }

    private void OnBack(object sender, RoutedEventArgs e)
    {
        ViewModel.Navigate(ViewModel.Home);
    }

    private void OnUndo(object sender, RoutedEventArgs e)
    {
        if (ViewModel.CurrentPage is AacViewModel aac)
        {
            aac.Undo();
        }
    }

    private void OnPause(object sender, RoutedEventArgs e)
    {
        AppServices.InputRouter.TogglePause();
        ViewModel.StatusMessage = AppServices.InputRouter.IsPaused ? "Pause" : "Running";
    }

    private void OnBuzzer(object sender, RoutedEventArgs e)
    {
        SystemSounds.Beep.Play();
        ViewModel.StatusMessage = "Buzzer";
        Background = (System.Windows.Media.Brush)FindResource("Accent");
        _buzzerTimer.Stop();
        _buzzerTimer.Start();
    }

    private void OnPc(object sender, RoutedEventArgs e)
    {
        ViewModel.Navigate(ViewModel.OverlayPanel);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        AppServices.InputCoordinator.Attach(this);
        if (ViewModel.CurrentProfile.InputMode == InputMode.SwitchScan)
        {
            AppServices.ScanEngine.Start();
        }
    }
}
