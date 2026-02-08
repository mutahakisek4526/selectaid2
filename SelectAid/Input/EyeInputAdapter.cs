using System.Windows;
using System.Windows.Input;

namespace SelectAid.Input;

public class EyeInputAdapter
{
    private readonly InputRouter _router;
    private bool _enabled;

    public EyeInputAdapter(InputRouter router)
    {
        _router = router;
    }

    public void SetEnabled(bool enabled)
    {
        _enabled = enabled;
    }

    public void Attach(Window window)
    {
        window.MouseMove += OnMouseMove;
        window.MouseDown += OnMouseDown;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (!_enabled)
        {
            return;
        }

        _ = e.GetPosition((IInputElement)sender);
        _router.Raise(InputAction.PointerMoveAbs);
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (!_enabled)
        {
            return;
        }

        if (e.ChangedButton == MouseButton.Left)
        {
            _router.Raise(InputAction.LeftClick);
        }
    }
}
