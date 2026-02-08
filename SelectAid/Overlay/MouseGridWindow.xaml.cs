using System.Windows;
using System.Windows.Controls;
using SelectAid.Services;

namespace SelectAid.Overlay;

public partial class MouseGridWindow : Window
{
    private Rect _currentRect;

    public MouseGridWindow()
    {
        InitializeComponent();
        _currentRect = new Rect(0, 0, SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
    }

    private void OnCell(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }

        var index = int.Parse(button.Content.ToString() ?? "1") - 1;
        var row = index / 3;
        var col = index % 3;
        var cellWidth = _currentRect.Width / 3;
        var cellHeight = _currentRect.Height / 3;
        var newRect = new Rect(
            _currentRect.X + col * cellWidth,
            _currentRect.Y + row * cellHeight,
            cellWidth,
            cellHeight);
        _currentRect = newRect;
        var center = new Point(_currentRect.X + _currentRect.Width / 2, _currentRect.Y + _currentRect.Height / 2);
        AppServices.InputSender.MovePointer(center);
    }

    private void OnLeft(object sender, RoutedEventArgs e)
    {
        AppServices.InputSender.LeftClick();
    }

    private void OnRight(object sender, RoutedEventArgs e)
    {
        AppServices.InputSender.RightClick();
    }

    private void OnClose(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
