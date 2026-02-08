using System;
using System.Globalization;
using System.IO;

namespace SelectAid.Services;

public class LoggingService
{
    private readonly string _logFile;
    private readonly object _sync = new();

    public LoggingService(string dataRoot)
    {
        Directory.CreateDirectory(dataRoot);
        _logFile = Path.Combine(dataRoot, "log.txt");
    }

    public void Log(string level, string message, Exception? ex = null)
    {
        var line = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)} [{level}] {message}";
        if (ex != null)
        {
            line += Environment.NewLine + ex;
        }

        lock (_sync)
        {
            File.AppendAllText(_logFile, line + Environment.NewLine);
        }
    }
}
