using System.Linq;
using SelectAid.Models;

namespace SelectAid.ViewModels;

public class PhrasesViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    private readonly PhraseCatalog _catalog;
    private string _newPhrase = string.Empty;

    public PhrasesViewModel(MainViewModel main, PhraseCatalog catalog)
    {
        _main = main;
        _catalog = catalog;
        if (!_catalog.Scenes.Any())
        {
            _catalog.Scenes.Add(new PhraseScene { Name = "シーン" });
        }

        if (!_catalog.Scenes[0].Categories.Any())
        {
            _catalog.Scenes[0].Categories.Add(new PhraseCategory { Name = "カテゴリ" });
        }
    }

    public PhraseCatalog Catalog => _catalog;

    public string NewPhrase
    {
        get => _newPhrase;
        set => SetProperty(ref _newPhrase, value);
    }

    public void AddPhrase()
    {
        if (string.IsNullOrWhiteSpace(NewPhrase))
        {
            return;
        }

        _catalog.Scenes[0].Categories[0].Items.Add(new PhraseItem { Text = NewPhrase });
        NewPhrase = string.Empty;
        _main.SaveAll();
    }
}
