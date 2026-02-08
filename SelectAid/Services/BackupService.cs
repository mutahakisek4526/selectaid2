using System;
using System.IO;
using System.IO.Compression;

namespace SelectAid.Services;

public class BackupService
{
    private readonly string _dataRoot;

    public BackupService(string dataRoot)
    {
        _dataRoot = dataRoot;
    }

    public string CreateBackup()
    {
        var backupDir = Path.Combine(_dataRoot, "backups");
        Directory.CreateDirectory(backupDir);
        var path = Path.Combine(backupDir, $"backup-{DateTime.Now:yyyyMMdd-HHmmss}.zip");
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        ZipFile.CreateFromDirectory(_dataRoot, path, CompressionLevel.Optimal, true);
        return path;
    }

    public void RestoreBackup(string zipPath)
    {
        if (!File.Exists(zipPath))
        {
            throw new FileNotFoundException("Backup not found", zipPath);
        }

        var tempDir = Path.Combine(_dataRoot, "_restore");
        if (Directory.Exists(tempDir))
        {
            Directory.Delete(tempDir, true);
        }

        ZipFile.ExtractToDirectory(zipPath, tempDir, true);
        foreach (var file in Directory.GetFiles(tempDir))
        {
            var target = Path.Combine(_dataRoot, Path.GetFileName(file));
            File.Copy(file, target, true);
        }

        Directory.Delete(tempDir, true);
    }
}
