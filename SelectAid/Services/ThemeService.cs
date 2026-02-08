using System;
using System.Linq;
using System.Windows;

namespace SelectAid.Services;

public class ThemeService
{
    public string CurrentThemeId { get; private set; } = "Friendly";

    public void ApplyTheme(string themeId, bool highContrast = false)
    {
        CurrentThemeId = themeId;
        var app = Application.Current;
        if (app == null)
        {
            return;
        }

        var baseDict = app.Resources.MergedDictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme/Base.xaml"));
        var themeDicts = app.Resources.MergedDictionaries.Where(d => d.Source != null && d.Source.OriginalString.Contains("Theme/") && !d.Source.OriginalString.Contains("Base.xaml")).ToList();
        foreach (var dict in themeDicts)
        {
            app.Resources.MergedDictionaries.Remove(dict);
        }

        var uri = themeId switch
        {
            "Stylish" => new Uri("Theme/Stylish.xaml", UriKind.Relative),
            "Kids" => new Uri("Theme/Kids.xaml", UriKind.Relative),
            _ => new Uri("Theme/Friendly.xaml", UriKind.Relative)
        };

        app.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = uri });
        if (highContrast)
        {
            app.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Theme/HighContrast.xaml", UriKind.Relative) });
        }
        if (baseDict == null)
        {
            app.Resources.MergedDictionaries.Insert(0, new ResourceDictionary { Source = new Uri("Theme/Base.xaml", UriKind.Relative) });
        }
    }
}
