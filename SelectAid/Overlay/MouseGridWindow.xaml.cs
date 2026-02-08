using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SelectAid.Services;

namespace SelectAid.Overlay;

public partial class MouseGridWindow : Window
{
    private readonly Stack<Rect> _history = new();
    private Rect _currentRect;
    private int _divisions = 3;

    public MouseGridWindow()
    {
        InitializeComponent();
        _currentRect = new Rect(0, 0, SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
        _history.Push(_currentRect);
    }

    private void OnCell(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }

        if (!int.TryParse(button.Content.ToString(), out var index))
        {
            index = 1;
        }

        index -= 1;
        var row = index / _divisions;
        var col = index % _divisions;
        var cellWidth = _currentRect.Width / _divisions;
        var cellHeight = _currentRect.Height / _divisions;
        var newRect = new Rect(
            _currentRect.X + col * cellWidth,
            _currentRect.Y + row * cellHeight,
            cellWidth,
            cellHeight);
        _currentRect = newRect;
        _history.Push(_currentRect);
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

    private void OnBack(object sender, RoutedEventArgs e)
    {
        if (_history.Count > 1)
        {
            _history.Pop();
            _currentRect = _history.Peek();
        }
    }

    private void OnClose(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnDivisionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox combo && combo.SelectedItem is ComboBoxItem item && int.TryParse(item.Content.ToString(), out var value))
        {
            _divisions = Math.Clamp(value, 2, 6);
            ResetGrid();
        }
    }

    private void ResetGrid()
    {
        _currentRect = new Rect(0, 0, SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
        _history.Clear();
        _history.Push(_currentRect);
        GridButtons.Columns = _divisions;
        GridButtons.Rows = _divisions;
        GridButtons.Children.Clear();
        var total = _divisions * _divisions;
        for (var i = 1; i <= total; i++)
        {
            var button = new Button
            {
                Content = i.ToString(),
                Style = (Style)FindResource("PrimaryButton")
            };
            button.Click += OnCell;
            GridButtons.Children.Add(button);
        }
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Close();
        }
    }
}
