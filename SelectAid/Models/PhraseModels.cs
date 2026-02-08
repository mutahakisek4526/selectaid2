using System.Collections.Generic;

namespace SelectAid.Models;

public class PhraseCatalog
{
    public List<PhraseScene> Scenes { get; set; } = new();
}

public class PhraseScene
{
    public string Name { get; set; } = string.Empty;
    public List<PhraseCategory> Categories { get; set; } = new();
}

public class PhraseCategory
{
    public string Name { get; set; } = string.Empty;
    public List<PhraseItem> Items { get; set; } = new();
}

public class PhraseItem
{
    public string Text { get; set; } = string.Empty;
}
