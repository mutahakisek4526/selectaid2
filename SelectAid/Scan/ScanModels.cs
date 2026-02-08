using System.Collections.Generic;
using System.Windows;

namespace SelectAid.Scan;

public class ScanTarget
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public FrameworkElement Element { get; set; } = null!;
}

public class ScanGroup
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public List<ScanTarget> Targets { get; set; } = new();
}

public class ScanArea
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public List<ScanGroup> Groups { get; set; } = new();
}

public class ScanState
{
    public int Level { get; set; }
    public int Index { get; set; }
    public string Label { get; set; } = string.Empty;
}
