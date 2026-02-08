using System.Windows;
using System.Windows.Controls;
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
}
