using System.Media;
using System.Windows;
using SelectAid.Services;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
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
    }

    private void OnPc(object sender, RoutedEventArgs e)
    {
        ViewModel.Navigate(ViewModel.OverlayPanel);
    }
}
