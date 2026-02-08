using System.Windows;
using System.Windows.Controls;
using SelectAid.Models;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class KeyboardLayoutsView : UserControl
{
    public KeyboardLayoutsView()
    {
        InitializeComponent();
    }

    private KeyboardLayoutsViewModel ViewModel => (KeyboardLayoutsViewModel)DataContext;

    private void OnToggle(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is KeyboardLayout layout)
        {
            ViewModel.ToggleEnabled(layout);
        }
    }
}
