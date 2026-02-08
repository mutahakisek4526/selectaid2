using System;
using System.Timers;
using SelectAid.Models;

namespace SelectAid.Scan;

public class ScanEngine
{
    private readonly Timer _timer = new();
    private int _index;
    private int _cycles;

    public event Action<int, int>? HighlightChanged;
    public event Action? AutoStopped;

    public int Level { get; private set; } = 1;
    public int Count { get; private set; }
    public bool IsRunning => _timer.Enabled;

    public void ConfigureLevel(int level, int itemCount, ScanSettings settings)
    {
        Level = level;
        Count = itemCount;
        _index = -1;
        _cycles = 0;
        _timer.Interval = level switch
        {
            1 => settings.L1SpeedMs,
            2 => settings.L2SpeedMs,
            _ => settings.L3SpeedMs
        };
    }

    public void Start()
    {
        if (Count <= 0)
        {
            return;
        }

        _timer.Elapsed -= OnTick;
        _timer.Elapsed += OnTick;
        _timer.AutoReset = true;
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }

    private void OnTick(object? sender, ElapsedEventArgs e)
    {
        _index++;
        if (_index >= Count)
        {
            _index = 0;
            _cycles++;
        }

        HighlightChanged?.Invoke(Level, _index);
    }
}
