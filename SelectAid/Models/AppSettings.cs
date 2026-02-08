using System;

namespace SelectAid.Models;

public class AppSettings
{
    public int Version { get; set; } = 1;
    public Guid CurrentProfileId { get; set; } = Guid.Empty;
    public bool AutoStartEnabled { get; set; }
    public bool SafeModeRequested { get; set; }
}
