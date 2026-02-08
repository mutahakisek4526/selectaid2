using System.Windows;
using System.Windows.Controls;
using SelectAid.Models;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class PhrasesView : UserControl
{
    public PhrasesView()
    {
        InitializeComponent();
    }

    private PhrasesViewModel ViewModel => (PhrasesViewModel)DataContext;

    private void OnAdd(object sender, RoutedEventArgs e) => ViewModel.AddPhrase();

    private void OnSpeak(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is PhraseItem item)
        {
            ViewModel.Speak(item);
        }
    }

    private void OnInsert(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is PhraseItem item)
        {
            ViewModel.Insert(item);
        }
    }
}
