using System.Collections.Generic;

namespace SelectAid.Models;

public class KeyboardLayout
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Columns { get; set; } = 6;
    public bool Enabled { get; set; } = true;
    public List<KeyboardKey> Keys { get; set; } = new();
}

public class KeyboardKey
{
    public string Label { get; set; } = string.Empty;
    public string OutputText { get; set; } = string.Empty;
    public string Action { get; set; } = "Char";
    public string Variant { get; set; } = string.Empty;
}
