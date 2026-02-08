using System.Windows;
using System.Windows.Controls;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class BackupRestoreView : UserControl
{
    public BackupRestoreView()
    {
        InitializeComponent();
    }

    private BackupRestoreViewModel ViewModel => (BackupRestoreViewModel)DataContext;

    private void OnCreate(object sender, RoutedEventArgs e) => ViewModel.CreateBackup();

    private void OnRestore(object sender, RoutedEventArgs e)
    {
        if (BackupList.SelectedItem is string path)
        {
            ViewModel.Restore(path);
        }
    }
}
