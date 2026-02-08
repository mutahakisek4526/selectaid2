using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SelectAid.Models;
using SelectAid.Scan;
using SelectAid.Services;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class AacView : UserControl
{
    private List<ScanArea> _areas = new();

    public AacView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private AacViewModel ViewModel => (AacViewModel)DataContext;

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _areas = BuildScanAreas();
        AppServices.ScanEngine.SetTargets(_areas);
        AppServices.ScanEngine.HighlightChanged += OnScanHighlight;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        AppServices.ScanEngine.HighlightChanged -= OnScanHighlight;
    }

    private List<ScanArea> BuildScanAreas()
    {
        return new List<ScanArea>
        {
            new()
            {
                Id = "history",
                Label = "History",
                Groups = new List<ScanGroup>
                {
                    new()
                    {
                        Id = "history-items",
                        Label = "History Items",
                        Targets = FindButtons(HistoryPane).Select((b, i) => new ScanTarget { Id = $"history-{i}", Label = b.Content?.ToString() ?? "History", Element = b }).ToList()
                    }
                }
            },
            new()
            {
                Id = "prediction",
                Label = "Predictions",
                Groups = new List<ScanGroup>
                {
                    new()
                    {
                        Id = "prediction-items",
                        Label = "Prediction Items",
                        Targets = FindButtons(PredictionPane).Select((b, i) => new ScanTarget { Id = $"pred-{i}", Label = b.Content?.ToString() ?? "Prediction", Element = b }).ToList()
                    }
                }
            },
            new()
            {
                Id = "input",
                Label = "Input",
                Groups = new List<ScanGroup>
                {
                    new()
                    {
                        Id = "input-keys",
                        Label = "Keys",
                        Targets = FindButtons(InputPane).Select((b, i) => new ScanTarget { Id = $"key-{i}", Label = b.Content?.ToString() ?? "Key", Element = b }).ToList()
                    }
                }
            },
            new()
            {
                Id = "control",
                Label = "Control",
                Groups = new List<ScanGroup>
                {
                    new()
                    {
                        Id = "control-buttons",
                        Label = "Control Buttons",
                        Targets = FindButtons(ControlPane).Select((b, i) => new ScanTarget { Id = $"control-{i}", Label = b.Content?.ToString() ?? "Control", Element = b }).ToList()
                    }
                }
            }
        };
    }

    private void OnScanHighlight(ScanState state)
    {
        var element = state.Level switch
        {
            1 => _areas.ElementAtOrDefault(state.Index)?.Groups.FirstOrDefault()?.Targets.FirstOrDefault()?.Element,
            2 => _areas.ElementAtOrDefault(AppServices.ScanEngine.AreaIndex)?.Groups.ElementAtOrDefault(state.Index)?.Targets.FirstOrDefault()?.Element,
            _ => _areas.ElementAtOrDefault(AppServices.ScanEngine.AreaIndex)?.Groups.ElementAtOrDefault(AppServices.ScanEngine.GroupIndex)?.Targets.ElementAtOrDefault(state.Index)?.Element
        };
        AppServices.ScanHighlighter.Highlight(element);
    }

    private static IEnumerable<Button> FindButtons(DependencyObject root)
    {
        var results = new List<Button>();
        var count = VisualTreeHelper.GetChildrenCount(root);
        for (var i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(root, i);
            if (child is Button button)
            {
                results.Add(button);
            }
            results.AddRange(FindButtons(child));
        }
        return results;
    }

    private void OnKey(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is KeyboardKey key)
        {
            ViewModel.KeyPress(key);
        }
    }

    private void OnSpeak(object sender, RoutedEventArgs e) => ViewModel.Speak();
    private void OnBackspace(object sender, RoutedEventArgs e) => ViewModel.Backspace();
    private void OnClear(object sender, RoutedEventArgs e) => ViewModel.Clear();
    private void OnUndo(object sender, RoutedEventArgs e) => ViewModel.Undo();

    private void OnPrediction(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is string word)
        {
            ViewModel.ApplyPrediction(word);
        }
    }

    private void OnHistorySpeak(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is HistoryEntry entry)
        {
            ViewModel.ReSpeak(entry);
        }
    }

    private void OnHistoryCopy(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is HistoryEntry entry)
        {
            ViewModel.CopyHistory(entry);
        }
    }

    private void OnHistoryDelete(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is HistoryEntry entry)
        {
            ViewModel.DeleteHistory(entry);
        }
    }

    private void OnBuzzer(object sender, RoutedEventArgs e)
    {
        SystemSounds.Beep.Play();
    }

    private void OnPause(object sender, RoutedEventArgs e)
    {
        AppServices.InputRouter.TogglePause();
    }

    private void OnOverlay(object sender, RoutedEventArgs e) => ViewModel.GoOverlay();

    private void OnHome(object sender, RoutedEventArgs e) => ViewModel.GoHome();
}
