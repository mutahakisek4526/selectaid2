using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SelectAid.Models;
using UITimer = System.Timers.Timer;

namespace SelectAid.Scan;

public class ScanEngine
{
    private readonly UITimer _timer;
    private readonly object _sync = new();
    private List<ScanArea> _areas = new();
    private int _areaIndex;
    private int _groupIndex;
    private int _targetIndex;
    private int _cycles;
    private bool _hold;
    private ScanSettings _settings = new();

    public ScanEngine()
    {
        _timer = new UITimer();
        _timer.AutoReset = true;
        _timer.Elapsed += (_, _) => Step();
    }

    public event Action<ScanState>? HighlightChanged;
    public event Action? AutoStopped;

    public int Level { get; private set; } = 1;
    public bool IsRunning => _timer.Enabled;
    public bool IsHold => _hold;
    public int AreaIndex => _areaIndex;
    public int GroupIndex => _groupIndex;
    public int TargetIndex => _targetIndex;

    public void SetTargets(List<ScanArea> areas)
    {
        _areas = areas;
        Reset();
    }

    public void Configure(ScanSettings settings)
    {
        _settings = settings;
        UpdateInterval();
    }

    public void Start()
    {
        if (!_areas.Any())
        {
            return;
        }

        _hold = false;
        UpdateInterval();
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }

    public void ToggleHold()
    {
        _hold = !_hold;
    }

    public void Confirm()
    {
        if (_areas.Count == 0)
        {
            return;
        }

        if (Level == 1)
        {
            Level = 2;
            _groupIndex = -1;
        }
        else if (Level == 2)
        {
            Level = 3;
            _targetIndex = -1;
        }
        else
        {
            ActivateCurrentTarget();
            Level = 1;
        }

        UpdateInterval();
    }

    public void Cancel()
    {
        if (Level == 3)
        {
            Level = 2;
            _targetIndex = -1;
        }
        else if (Level == 2)
        {
            Level = 1;
            _groupIndex = -1;
        }
        else
        {
            Reset();
        }

        UpdateInterval();
    }

    public ScanState GetState()
    {
        var label = Level switch
        {
            1 => _areas.ElementAtOrDefault(_areaIndex)?.Label ?? string.Empty,
            2 => _areas.ElementAtOrDefault(_areaIndex)?.Groups.ElementAtOrDefault(_groupIndex)?.Label ?? string.Empty,
            _ => _areas.ElementAtOrDefault(_areaIndex)?.Groups.ElementAtOrDefault(_groupIndex)?.Targets.ElementAtOrDefault(_targetIndex)?.Label ?? string.Empty
        };
        return new ScanState { Level = Level, Index = GetIndex(), Label = label };
    }

    private int GetIndex()
    {
        return Level switch
        {
            1 => _areaIndex,
            2 => _groupIndex,
            _ => _targetIndex
        };
    }

    private void Step()
    {
        if (_hold)
        {
            return;
        }

        lock (_sync)
        {
            if (Level == 1)
            {
                _areaIndex = NextIndex(_areaIndex, _areas.Count);
                _cycles = CountCycles(_areaIndex, _areas.Count, _cycles);
            }
            else if (Level == 2)
            {
                var groups = _areas.ElementAtOrDefault(_areaIndex)?.Groups ?? new List<ScanGroup>();
                _groupIndex = NextIndex(_groupIndex, groups.Count);
                _cycles = CountCycles(_groupIndex, groups.Count, _cycles);
            }
            else
            {
                var targets = _areas.ElementAtOrDefault(_areaIndex)?.Groups.ElementAtOrDefault(_groupIndex)?.Targets ?? new List<ScanTarget>();
                _targetIndex = NextIndex(_targetIndex, targets.Count);
                _cycles = CountCycles(_targetIndex, targets.Count, _cycles);
            }
        }

        if (_settings.AutoStopAfterCycles > 0 && _cycles >= _settings.AutoStopAfterCycles)
        {
            Stop();
            AutoStopped?.Invoke();
            return;
        }

        var state = GetState();
        Application.Current?.Dispatcher.BeginInvoke(() => HighlightChanged?.Invoke(state));
    }

    private static int NextIndex(int index, int count)
    {
        if (count <= 0)
        {
            return -1;
        }

        index++;
        if (index >= count)
        {
            index = 0;
        }

        return index;
    }

    private static int CountCycles(int index, int count, int current)
    {
        if (count <= 0)
        {
            return 0;
        }

        if (index == 0)
        {
            return current + 1;
        }

        return current;
    }

    private void UpdateInterval()
    {
        _timer.Interval = Level switch
        {
            1 => _settings.L1SpeedMs,
            2 => _settings.L2SpeedMs,
            _ => _settings.L3SpeedMs
        };
    }

    private void ActivateCurrentTarget()
    {
        var target = _areas.ElementAtOrDefault(_areaIndex)?.Groups.ElementAtOrDefault(_groupIndex)?.Targets.ElementAtOrDefault(_targetIndex);
        if (target == null)
        {
            return;
        }

        target.Element.Dispatcher.BeginInvoke(() =>
        {
            if (target.Element is System.Windows.Controls.Button button)
            {
                button.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            }
            else
            {
                target.Element.Focus();
            }
        });
    }

    private void Reset()
    {
        _areaIndex = -1;
        _groupIndex = -1;
        _targetIndex = -1;
        _cycles = 0;
        Level = 1;
    }
}
