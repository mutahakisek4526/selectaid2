using System.Collections.ObjectModel;
using SelectAid.Models;

namespace SelectAid.ViewModels;

public class KeyboardLayoutsViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    private readonly ObservableCollection<KeyboardLayout> _layouts;

    public KeyboardLayoutsViewModel(MainViewModel main, ObservableCollection<KeyboardLayout> layouts)
    {
        _main = main;
        _layouts = layouts;
    }

    public ObservableCollection<KeyboardLayout> Layouts => _layouts;

    public void ToggleEnabled(KeyboardLayout layout)
    {
        layout.Enabled = !layout.Enabled;
        _main.SaveAll();
    }
}
