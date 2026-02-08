using System.Windows;
using System.Windows.Controls;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class LogsView : UserControl
{
    public LogsView()
    {
        InitializeComponent();
    }

    private LogsViewModel ViewModel => (LogsViewModel)DataContext;

    private void OnRefresh(object sender, RoutedEventArgs e) => ViewModel.Refresh();
}
