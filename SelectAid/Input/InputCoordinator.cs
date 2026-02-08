using System.Windows;
using SelectAid.Models;

namespace SelectAid.Input;

public class InputCoordinator
{
    private readonly SwitchInput _switchInput;
    private readonly EyeInputAdapter _eyeInput;
    private readonly GyroInputAdapter _gyroInput;

    public InputCoordinator(InputRouter router)
    {
        _switchInput = new SwitchInput(router);
        _eyeInput = new EyeInputAdapter(router);
        _gyroInput = new GyroInputAdapter(router);
    }

    public void Attach(Window window)
    {
        _switchInput.Attach(window);
        _eyeInput.Attach(window);
        _gyroInput.Attach(window);
    }

    public void Configure(Profile profile)
    {
        _switchInput.Configure(profile.Switch);
        _switchInput.SetEnabled(profile.InputMode is InputMode.EyeSwitch or InputMode.SwitchScan);
        _eyeInput.SetEnabled(profile.InputMode is InputMode.EyeOnly or InputMode.EyeSwitch or InputMode.EyeGyro);
        _gyroInput.SetEnabled(profile.InputMode is InputMode.GyroOnly or InputMode.EyeGyro);
    }
}
