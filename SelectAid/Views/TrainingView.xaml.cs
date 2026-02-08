using System.Windows;
using System.Windows.Controls;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class TrainingView : UserControl
{
    public TrainingView()
    {
        InitializeComponent();
    }

    private TrainingViewModel ViewModel => (TrainingViewModel)DataContext;

    private void OnEye(object sender, RoutedEventArgs e) => ViewModel.StartEyeTraining();
    private void OnGyro(object sender, RoutedEventArgs e) => ViewModel.StartGyroTraining();
    private void OnScan(object sender, RoutedEventArgs e) => ViewModel.StartScanTraining();
}
