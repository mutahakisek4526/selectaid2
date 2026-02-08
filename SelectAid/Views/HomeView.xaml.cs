using System.Windows;
using System.Windows.Controls;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    private HomeViewModel ViewModel => (HomeViewModel)DataContext;

    private void OnAac(object sender, RoutedEventArgs e) => ViewModel.GoAac();
    private void OnPhrases(object sender, RoutedEventArgs e) => ViewModel.GoPhrases();
    private void OnLayouts(object sender, RoutedEventArgs e) => ViewModel.GoKeyboardLayouts();
    private void OnOverlay(object sender, RoutedEventArgs e) => ViewModel.GoOverlay();
    private void OnSettings(object sender, RoutedEventArgs e) => ViewModel.GoSettings();
    private void OnSupporter(object sender, RoutedEventArgs e) => ViewModel.GoSupporter();
    private void OnTraining(object sender, RoutedEventArgs e) => ViewModel.GoTraining();
    private void OnLogs(object sender, RoutedEventArgs e) => ViewModel.GoLogs();
    private void OnBackup(object sender, RoutedEventArgs e) => ViewModel.GoBackup();
}
