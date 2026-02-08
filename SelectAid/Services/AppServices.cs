using System;
using System.IO;
using SelectAid.Input;
using SelectAid.Persistence;
using SelectAid.Scan;
using SelectAid.Overlay;

namespace SelectAid.Services;

public static class AppServices
{
    public static string DataRoot { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SelectAid");
    public static JsonStore Store { get; } = new();
    public static LoggingService Logger { get; } = new(DataRoot);
    public static MetricsService Metrics { get; } = new(DataRoot);
    public static ProfileService Profiles { get; } = new(Store, DataRoot);
    public static ThemeService Themes { get; } = new();
    public static SpeechService Speech { get; } = new();
    public static PcControlService PcControl { get; } = new();
    public static InputRouter InputRouter { get; } = new();
    public static InputCoordinator InputCoordinator { get; } = new(InputRouter);
    public static ScanEngine ScanEngine { get; } = new();
    public static ScanHighlighter ScanHighlighter { get; } = new();
    public static InputTimingController TimingController { get; } = new();
    public static OverlayManager OverlayManager { get; } = new();
    public static InputSender InputSender { get; } = new();
}
