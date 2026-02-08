using System;
using System.Collections.ObjectModel;
using System.Linq;
using SelectAid.Models;
using SelectAid.Services;

namespace SelectAid.ViewModels;

public class AacViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    private readonly ObservableCollection<KeyboardLayout> _layouts;
    private readonly PhraseCatalog _phrases;
    private readonly HistoryLog _history;
    private readonly UserDictionary _dictionary;
    private string _composeText = string.Empty;
    private KeyboardLayout _selectedLayout;
    private ObservableCollection<string> _predictions = new();

    public AacViewModel(MainViewModel main, ObservableCollection<KeyboardLayout> layouts, PhraseCatalog phrases, HistoryLog history, UserDictionary dictionary)
    {
        _main = main;
        _layouts = layouts;
        _phrases = phrases;
        _history = history;
        _dictionary = dictionary;
        _selectedLayout = _layouts.FirstOrDefault(l => l.Enabled) ?? _layouts.First();
        RefreshPredictions();
    }

    public ObservableCollection<KeyboardLayout> Layouts => _layouts;
    public PhraseCatalog Phrases => _phrases;
    public ObservableCollection<HistoryEntry> History => new(_history.Entries.OrderByDescending(h => h.Timestamp).Take(20));

    public KeyboardLayout SelectedLayout
    {
        get => _selectedLayout;
        set
        {
            SetProperty(ref _selectedLayout, value);
            _main.SaveAll();
        }
    }

    public string ComposeText
    {
        get => _composeText;
        set => SetProperty(ref _composeText, value);
    }

    public ObservableCollection<string> Predictions
    {
        get => _predictions;
        set => SetProperty(ref _predictions, value);
    }

    public void KeyPress(KeyboardKey key)
    {
        if (key.Value == "dakuten")
        {
            ComposeText += "゛";
        }
        else if (key.Value == "handakuten")
        {
            ComposeText += "゜";
        }
        else
        {
            ComposeText += key.Value;
        }

        RefreshPredictions();
    }

    public void ApplyPrediction(string word)
    {
        ComposeText += word;
    }

    public void Backspace()
    {
        if (!string.IsNullOrEmpty(ComposeText))
        {
            ComposeText = ComposeText.Substring(0, ComposeText.Length - 1);
        }
    }

    public void Clear() => ComposeText = string.Empty;

    public void Speak()
    {
        if (string.IsNullOrWhiteSpace(ComposeText))
        {
            return;
        }

        AppServices.Speech.Speak(ComposeText);
        _history.Entries.Add(new HistoryEntry { Text = ComposeText, Timestamp = DateTime.Now });
        _main.SaveAll();
        ComposeText = string.Empty;
        RefreshPredictions();
    }

    public void SpeakPhrase(PhraseItem item)
    {
        AppServices.Speech.Speak(item.Text);
        _history.Entries.Add(new HistoryEntry { Text = item.Text, Timestamp = DateTime.Now });
        _main.SaveAll();
    }

    public void InsertPhrase(PhraseItem item)
    {
        ComposeText += item.Text;
    }

    public void Undo()
    {
        if (_history.Entries.Count == 0)
        {
            return;
        }

        _history.Entries.RemoveAt(_history.Entries.Count - 1);
        _main.SaveAll();
    }

    private void RefreshPredictions()
    {
        var recent = _history.Entries.OrderByDescending(h => h.Timestamp).Select(h => h.Text).Distinct().Take(3);
        var freq = _history.Entries.GroupBy(h => h.Text).OrderByDescending(g => g.Count()).Select(g => g.Key);
        var combined = recent.Concat(freq).Concat(_dictionary.Words).Distinct().Take(8).ToList();
        Predictions = new ObservableCollection<string>(combined);
    }
}
