using System.Windows;
using SelectAid.Services;

namespace SelectAid.Overlay;

public partial class OverlayWindow : Window
{
    private bool _dragging;

    public OverlayWindow()
    {
        InitializeComponent();
    }

    private void OnLeft(object sender, RoutedEventArgs e) => AppServices.InputSender.LeftClick();
    private void OnRight(object sender, RoutedEventArgs e) => AppServices.InputSender.RightClick();
    private void OnDouble(object sender, RoutedEventArgs e) => AppServices.InputSender.DoubleClick();

    private void OnDragToggle(object sender, RoutedEventArgs e)
    {
        _dragging = !_dragging;
        if (_dragging)
        {
            AppServices.InputSender.LeftDown();
        }
        else
        {
            AppServices.InputSender.LeftUp();
        }
    }

    private void OnScrollUp(object sender, RoutedEventArgs e) => AppServices.InputSender.Scroll(120);
    private void OnScrollDown(object sender, RoutedEventArgs e) => AppServices.InputSender.Scroll(-120);
    private void OnBack(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x1B);
    private void OnTab(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x09);
    private void OnShiftTab(object sender, RoutedEventArgs e)
    {
        AppServices.InputSender.KeyPress(0x10);
        AppServices.InputSender.KeyPress(0x09);
    }

    private void OnEnter(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x0D);
    private void OnSpace(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x20);
    private void OnBackspace(object sender, RoutedEventArgs e) => AppServices.InputSender.KeyPress(0x08);

    private void OnHome(object sender, RoutedEventArgs e) => Close();
    private void OnPause(object sender, RoutedEventArgs e) => AppServices.InputRouter.TogglePause();
}
