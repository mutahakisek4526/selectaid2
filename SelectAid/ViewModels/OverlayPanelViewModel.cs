using SelectAid.Services;

namespace SelectAid.ViewModels;

public class OverlayPanelViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    public OverlayPanelViewModel(MainViewModel main)
    {
        _main = main;
    }

    public void ToggleOverlay() => AppServices.OverlayManager.ToggleOverlay();
    public void ShowGrid() => AppServices.OverlayManager.ShowMouseGrid();
    public void BackHome()
    {
        AppServices.OverlayManager.HideAll();
        _main.Navigate(_main.Home);
    }
}
