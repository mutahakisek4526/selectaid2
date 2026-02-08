using System;
using System.Collections.Generic;

namespace SelectAid.Models;

public class HistoryEntry
{
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string Text { get; set; } = string.Empty;
}

public class HistoryLog
{
    public List<HistoryEntry> Entries { get; set; } = new();
}

public class UserDictionary
{
    public List<string> Words { get; set; } = new();
}
