using System;
using System.Windows;
using System.Windows.Controls;
using SelectAid.ViewModels;

namespace SelectAid.Views;

public partial class SupporterView : UserControl
{
    public SupporterView()
    {
        InitializeComponent();
    }

    private SupporterViewModel ViewModel => (SupporterViewModel)DataContext;

    private void OnShutdown(object sender, EventArgs e) => ViewModel.Shutdown();
    private void OnRestart(object sender, EventArgs e) => ViewModel.Restart();
    private void OnSleep(object sender, EventArgs e) => ViewModel.Sleep();
    private void OnLogoff(object sender, EventArgs e) => ViewModel.Logoff();
}
