using System;
using System.Windows;
using SelectAid.Models;
using UITimer = System.Timers.Timer;

namespace SelectAid.Services;

public class InputTimingController
{
    private readonly UITimer _dwellTimer;
    private readonly object _sync = new();
    private int _elapsed;
    private int _dwellMs;

    public event Action<int>? DwellProgressChanged;
    public event Action? DwellCompleted;

    public InputTimingController()
    {
        _dwellTimer = new UITimer(50);
        _dwellTimer.AutoReset = true;
        _dwellTimer.Elapsed += (_, _) => OnTick();
    }

    public void Configure(DwellSettings dwell)
    {
        _dwellMs = dwell.DwellMs;
    }

    public void Start()
    {
        lock (_sync)
        {
            _elapsed = 0;
        }

        _dwellTimer.Start();
    }

    public void Stop()
    {
        _dwellTimer.Stop();
        PublishProgress(0);
    }

    private void OnTick()
    {
        lock (_sync)
        {
            _elapsed += (int)_dwellTimer.Interval;
        }

        var progress = _dwellMs == 0 ? 0 : Math.Min(100, _elapsed * 100 / _dwellMs);
        PublishProgress(progress);

        if (_dwellMs > 0 && _elapsed >= _dwellMs)
        {
            _dwellTimer.Stop();
            Application.Current?.Dispatcher.BeginInvoke(() => DwellCompleted?.Invoke());
        }
    }

    private void PublishProgress(int value)
    {
        Application.Current?.Dispatcher.BeginInvoke(() => DwellProgressChanged?.Invoke(value));
    }
}
