using System.Collections.ObjectModel;
using System.IO;
using SelectAid.Services;

namespace SelectAid.ViewModels;

public class BackupRestoreViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    private readonly BackupService _backupService;
    private string _status = string.Empty;

    public BackupRestoreViewModel(MainViewModel main, BackupService backupService)
    {
        _main = main;
        _backupService = backupService;
        Refresh();
    }

    public ObservableCollection<string> Backups { get; } = new();

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public void Refresh()
    {
        Backups.Clear();
        var dir = Path.Combine(AppServices.DataRoot, "backups");
        if (!Directory.Exists(dir))
        {
            return;
        }

        foreach (var file in Directory.GetFiles(dir, "*.zip"))
        {
            Backups.Add(file);
        }
    }

    public void CreateBackup()
    {
        var path = _backupService.CreateBackup();
        Status = $"バックアップ作成: {path}";
        Refresh();
    }

    public void Restore(string path)
    {
        _backupService.RestoreBackup(path);
        Status = $"復元完了: {path}";
        _main.SaveAll();
    }
}
