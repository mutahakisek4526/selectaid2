using System.Windows;
using System.Windows.Controls;
using SelectAid.Models;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class AacView : UserControl
{
    public AacView()
    {
        InitializeComponent();
    }

    private AacViewModel ViewModel => (AacViewModel)DataContext;

    private void OnKey(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is KeyboardKey key)
        {
            ViewModel.KeyPress(key);
        }
    }

    private void OnSpeak(object sender, RoutedEventArgs e) => ViewModel.Speak();
    private void OnBackspace(object sender, RoutedEventArgs e) => ViewModel.Backspace();
    private void OnClear(object sender, RoutedEventArgs e) => ViewModel.Clear();
    private void OnUndo(object sender, RoutedEventArgs e) => ViewModel.Undo();

    private void OnPrediction(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is string word)
        {
            ViewModel.ApplyPrediction(word);
        }
    }
}
