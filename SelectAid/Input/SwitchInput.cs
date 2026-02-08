using System;
using System.Windows;
using System.Windows.Input;
using SelectAid.Models;

namespace SelectAid.Input;

public class SwitchInput
{
    private readonly InputRouter _router;
    private bool _enabled;
    private SwitchSettings _settings = new();

    public SwitchInput(InputRouter router)
    {
        _router = router;
    }

    public void Configure(SwitchSettings settings)
    {
        _settings = settings;
    }

    public void SetEnabled(bool enabled)
    {
        _enabled = enabled;
    }

    public void Attach(Window window)
    {
        window.PreviewKeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (!_enabled)
        {
            return;
        }

        var key = e.Key.ToString();
        if (string.Equals(key, _settings.ConfirmKey, StringComparison.OrdinalIgnoreCase))
        {
            _router.Raise(InputAction.Confirm);
            e.Handled = true;
        }
        else if (string.Equals(key, _settings.CancelKey, StringComparison.OrdinalIgnoreCase))
        {
            _router.Raise(InputAction.Cancel);
            e.Handled = true;
        }
        else if (string.Equals(key, _settings.EmergencyKey, StringComparison.OrdinalIgnoreCase))
        {
            _router.Raise(InputAction.EmergencyStop);
            e.Handled = true;
        }
    }
}
