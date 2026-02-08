using System.Windows;
using System.Windows.Controls;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class OverlayPanelView : UserControl
{
    public OverlayPanelView()
    {
        InitializeComponent();
    }

    private OverlayPanelViewModel ViewModel => (OverlayPanelViewModel)DataContext;

    private void OnOverlay(object sender, RoutedEventArgs e) => ViewModel.ToggleOverlay();
    private void OnGrid(object sender, RoutedEventArgs e) => ViewModel.ShowGrid();
    private void OnHome(object sender, RoutedEventArgs e) => ViewModel.BackHome();
}
