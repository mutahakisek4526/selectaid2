using SelectAid.Overlay;

namespace SelectAid.ViewModels;

public class OverlayPanelViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    private readonly OverlayManager _overlayManager = new();

    public OverlayPanelViewModel(MainViewModel main)
    {
        _main = main;
    }

    public void ToggleOverlay() => _overlayManager.ToggleOverlay();
    public void ShowGrid() => _overlayManager.ShowMouseGrid();
    public void BackHome()
    {
        _overlayManager.HideAll();
        _main.Navigate(_main.Home);
    }
}
