using System;

namespace SelectAid.Models;

public class Profile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Default";
    public string ThemeId { get; set; } = "Friendly";
    public UiSettings Ui { get; set; } = new();
    public InputMode InputMode { get; set; } = InputMode.EyeOnly;
    public EyeSettings EyePointer { get; set; } = new();
    public DwellSettings Dwell { get; set; } = new();
    public GyroSettings Gyro { get; set; } = new();
    public SwitchSettings Switch { get; set; } = new();
    public ScanSettings Scan { get; set; } = new();
    public SpeechSettings Speech { get; set; } = new();
    public SafetySettings Safety { get; set; } = new();
    public PcControlSettings PCControl { get; set; } = new();
}

public enum InputMode
{
    EyeOnly,
    EyeSwitch,
    EyeGyro,
    GyroOnly,
    SwitchScan
}

public class UiSettings
{
    public string Scale { get; set; } = "Normal";
    public bool HighContrast { get; set; }
    public int DensityLimit { get; set; } = 12;
}

public class EyeSettings
{
    public int PointerSpeed { get; set; } = 10;
}

public class DwellSettings
{
    public int DwellMs { get; set; } = 800;
    public int ConfirmGuardMs { get; set; } = 300;
}

public class GyroSettings
{
    public int Sensitivity { get; set; } = 10;
}

public class SwitchSettings
{
    public string ConfirmKey { get; set; } = "Space";
    public string CancelKey { get; set; } = "Escape";
    public string EmergencyKey { get; set; } = "F12";
}

public class ScanSettings
{
    public int L1SpeedMs { get; set; } = 1000;
    public int L2SpeedMs { get; set; } = 900;
    public int L3SpeedMs { get; set; } = 800;
    public int AutoStopAfterCycles { get; set; } = 3;
    public int UndoWindowMs { get; set; } = 1500;
    public int ConfirmGuardMs { get; set; } = 300;
}

public class SpeechSettings
{
    public string Voice { get; set; } = string.Empty;
    public int Rate { get; set; } = 0;
    public int Volume { get; set; } = 100;
}

public class SafetySettings
{
    public bool CareLock { get; set; }
    public int IdleAutoLockMinutes { get; set; } = 0;
    public bool AllowShutdown { get; set; } = false;
    public bool AllowRestart { get; set; } = false;
    public bool AllowSleep { get; set; } = false;
    public bool AllowLogoff { get; set; } = false;
}

public class PcControlSettings
{
    public bool OverlayClickThrough { get; set; }
    public bool MouseGridSimpleClick { get; set; }
}
