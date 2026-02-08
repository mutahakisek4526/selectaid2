using System;

namespace SelectAid.Input;

public class InputRouter
{
    public event Action<InputAction>? ActionReceived;

    public bool IsPaused { get; private set; }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        ActionReceived?.Invoke(InputAction.PauseToggle);
    }

    public void Raise(InputAction action)
    {
        if (IsPaused && action != InputAction.PauseToggle && action != InputAction.EmergencyStop)
        {
            return;
        }

        ActionReceived?.Invoke(action);
    }
}
