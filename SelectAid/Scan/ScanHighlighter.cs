using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SelectAid.Scan;

public class ScanHighlighter
{
    private readonly Dictionary<Control, (Thickness, Brush?)> _previous = new();
    private Control? _current;

    public void Highlight(FrameworkElement? element)
    {
        if (_current != null)
        {
            Restore(_current);
        }

        if (element is not Control control)
        {
            _current = null;
            return;
        }

        if (!_previous.ContainsKey(control))
        {
            _previous[control] = (control.BorderThickness, control.BorderBrush);
        }

        control.BorderThickness = new Thickness(4);
        var accent = Application.Current?.FindResource("Accent") as Brush ?? Brushes.Yellow;
        control.BorderBrush = accent;
        _current = control;
    }

    private void Restore(Control control)
    {
        if (_previous.TryGetValue(control, out var prev))
        {
            control.BorderThickness = prev.Item1;
            control.BorderBrush = prev.Item2;
        }
    }
}
