using System.Windows;

namespace SelectAid.Overlay;

public class OverlayManager
{
    private OverlayWindow? _overlay;
    private MouseGridWindow? _grid;

    public void ToggleOverlay()
    {
        if (_overlay == null)
        {
            _overlay = new OverlayWindow();
            _overlay.Closed += (_, _) => _overlay = null;
            _overlay.Show();
        }
        else
        {
            _overlay.Close();
        }
    }

    public void ShowMouseGrid()
    {
        if (_grid == null)
        {
            _grid = new MouseGridWindow();
            _grid.Closed += (_, _) => _grid = null;
            _grid.Show();
        }
        else
        {
            _grid.Activate();
        }
    }

    public void HideAll()
    {
        _grid?.Close();
        _overlay?.Close();
    }
}
