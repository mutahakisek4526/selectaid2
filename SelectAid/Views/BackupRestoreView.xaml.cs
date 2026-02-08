using System.Windows;
using System.Windows.Controls;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class BackupRestoreView : UserControl
{
    private bool _confirmPending;

    public BackupRestoreView()
    {
        InitializeComponent();
    }

    private BackupRestoreViewModel ViewModel => (BackupRestoreViewModel)DataContext;

    private void OnCreate(object sender, RoutedEventArgs e) => ViewModel.CreateBackup();

    private void OnRestoreClick(object sender, RoutedEventArgs e)
    {
        _confirmPending = true;
        ViewModel.Status = "復元は長押しで確定します";
    }

    private void OnRestoreLongPress(object sender, System.EventArgs e)
    {
        if (!_confirmPending)
        {
            return;
        }

        _confirmPending = false;
        if (BackupList.SelectedItem is string path)
        {
            ViewModel.Restore(path);
        }
        else
        {
            ViewModel.Status = "復元するバックアップを選択してください";
        }
    }
}
