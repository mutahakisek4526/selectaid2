using System;
using Microsoft.Win32;

namespace SelectAid.Services;

public class StartupService
{
    private const string RunKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

    public void SetAutoStart(bool enabled, string appName, string exePath)
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKey, true);
        if (key == null)
        {
            throw new InvalidOperationException("Run registry key not found.");
        }

        if (enabled)
        {
            key.SetValue(appName, exePath);
        }
        else
        {
            key.DeleteValue(appName, false);
        }
    }

    public bool IsAutoStartEnabled(string appName)
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKey, false);
        if (key == null)
        {
            return false;
        }

        return key.GetValue(appName) != null;
    }
}
