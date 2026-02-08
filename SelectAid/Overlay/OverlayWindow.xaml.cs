using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SelectAid.Scan;
using SelectAid.Services;

namespace SelectAid.Overlay;

public partial class OverlayWindow : Window
{
    private bool _dragging;
    private bool _transparent;
    private List<ScanArea> _areas = new();

    public OverlayWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLeft(object sender, RoutedEventArgs e) => AppServices.InputSender.LeftClick();
    private void OnRight(object sender, RoutedEventArgs e) => AppServices.InputSender.RightClick();
    private void OnDouble(object sender, RoutedEventArgs e) => AppServices.InputSender.DoubleClick();

    private void OnDragToggle(object sender, RoutedEventArgs e)
    {
        _dragging = !_dragging;
        if (_dragging)
        {
            AppServices.InputSender.LeftDown();
        }
        else
        {
            AppServices.InputSender.LeftUp();
        }
    }

    private void OnScrollUp(object sender, RoutedEventArgs e) => AppServices.InputSender.Scroll(120);
    private void OnScrollDown(object sender, RoutedEventArgs e) => AppServices.InputSender.Scroll(-120);
    private void OnBack(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x1B);
    private void OnTab(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x09);
    private void OnShiftTab(object sender, RoutedEventArgs e)
    {
        AppServices.InputSender.KeyPress(0x10);
        AppServices.InputSender.KeyPress(0x09);
    }

    private void OnEnter(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x0D);
    private void OnSpace(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x20);
    private void OnBackspace(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x08);
    private void OnMouseGrid(object sender, RoutedEventArgs e) => AppServices.OverlayManager.ShowMouseGrid();

    private void OnTransparent(object sender, RoutedEventArgs e)
    {
        _transparent = !_transparent;
        Opacity = _transparent ? 0.3 : 1.0;
    }

    private void OnHome(object sender, RoutedEventArgs e) => Close();
    private void OnPause(object sender, RoutedEventArgs e) => AppServices.InputRouter.TogglePause();

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _areas = new List<ScanArea>
        {
            new()
            {
                Id = "overlay",
                Label = "Overlay",
                Groups = new List<ScanGroup>
                {
                    new()
                    {
                        Id = "overlay-controls",
                        Label = "Controls",
                        Targets = FindButtons(this).Select((b, i) => new ScanTarget { Id = $"overlay-{i}", Label = b.Content?.ToString() ?? "Overlay", Element = b }).ToList()
                    }
                }
            }
        };
        AppServices.ScanEngine.SetTargets(_areas);
        AppServices.ScanEngine.HighlightChanged += OnScanHighlight;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        AppServices.ScanEngine.HighlightChanged -= OnScanHighlight;
    }

    private void OnScanHighlight(ScanState state)
    {
        var element = _areas.ElementAtOrDefault(AppServices.ScanEngine.AreaIndex)?.Groups.FirstOrDefault()?.Targets.ElementAtOrDefault(state.Index)?.Element;
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
}
