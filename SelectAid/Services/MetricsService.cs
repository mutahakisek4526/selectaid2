using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SelectAid.Services;

public class MetricsService
{
    private readonly string _path;
    private readonly Dictionary<string, int> _counters = new();

    public MetricsService(string dataRoot)
    {
        Directory.CreateDirectory(dataRoot);
        _path = Path.Combine(dataRoot, "metrics.csv");
    }

    public void Increment(string key)
    {
        if (_counters.ContainsKey(key))
        {
            _counters[key]++;
        }
        else
        {
            _counters[key] = 1;
        }
    }

    public void Flush()
    {
        using var writer = new StreamWriter(_path, true);
        foreach (var entry in _counters)
        {
            writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)},{entry.Key},{entry.Value}");
        }
        _counters.Clear();
    }
}
