using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UITimer = System.Timers.Timer;

namespace SelectAid.Views;

public class LongPressButton : Button
{
    private readonly UITimer _timer;
    private bool _pressed;

    public event EventHandler? LongPressed;

    public int HoldMs
    {
        get => (int)GetValue(HoldMsProperty);
        set => SetValue(HoldMsProperty, value);
    }

    public static readonly DependencyProperty HoldMsProperty = DependencyProperty.Register(
        nameof(HoldMs), typeof(int), typeof(LongPressButton), new PropertyMetadata(1500));

    public LongPressButton()
    {
        _timer = new UITimer();
        _timer.AutoReset = false;
        _timer.Elapsed += (_, _) => Dispatcher.BeginInvoke(() =>
        {
            if (_pressed)
            {
                LongPressed?.Invoke(this, EventArgs.Empty);
            }
        });
        PreviewMouseLeftButtonDown += OnMouseDown;
        PreviewMouseLeftButtonUp += OnMouseUp;
        LostMouseCapture += OnLostCapture;
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        _pressed = true;
        _timer.Interval = HoldMs;
        _timer.Stop();
        _timer.Start();
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        _pressed = false;
        _timer.Stop();
    }

    private void OnLostCapture(object sender, MouseEventArgs e)
    {
        _pressed = false;
        _timer.Stop();
    }
}
