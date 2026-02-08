using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SelectAid.Models;
using SelectAid.Persistence;

namespace SelectAid.Services;

public class ProfileService
{
    private readonly JsonStore _store;
    private readonly string _dataRoot;

    public ProfileService(JsonStore store, string dataRoot)
    {
        _store = store;
        _dataRoot = dataRoot;
    }

    public AppSettings LoadSettings()
    {
        return _store.Load(Path.Combine(_dataRoot, "settings.json"), () => new AppSettings());
    }

    public void SaveSettings(AppSettings settings)
    {
        _store.Save(Path.Combine(_dataRoot, "settings.json"), settings);
    }

    public List<Profile> LoadProfiles()
    {
        var profiles = _store.Load(Path.Combine(_dataRoot, "profiles.json"), () => new List<Profile> { new Profile() });
        if (profiles.Count == 0)
        {
            profiles.Add(new Profile());
        }

        return profiles;
    }

    public void SaveProfiles(List<Profile> profiles)
    {
        _store.Save(Path.Combine(_dataRoot, "profiles.json"), profiles);
    }

    public List<KeyboardLayout> LoadKeyboardLayouts()
    {
        return _store.Load(Path.Combine(_dataRoot, "keyboardLayouts.json"), SampleData.CreateDefaultLayouts);
    }

    public void SaveKeyboardLayouts(List<KeyboardLayout> layouts)
    {
        _store.Save(Path.Combine(_dataRoot, "keyboardLayouts.json"), layouts);
    }

    public PhraseCatalog LoadPhrases()
    {
        return _store.Load(Path.Combine(_dataRoot, "phrases.json"), SampleData.CreateDefaultPhrases);
    }

    public void SavePhrases(PhraseCatalog catalog)
    {
        _store.Save(Path.Combine(_dataRoot, "phrases.json"), catalog);
    }

    public UserDictionary LoadUserDictionary()
    {
        return _store.Load(Path.Combine(_dataRoot, "userDict.json"), () => new UserDictionary());
    }

    public void SaveUserDictionary(UserDictionary dict)
    {
        _store.Save(Path.Combine(_dataRoot, "userDict.json"), dict);
    }

    public HistoryLog LoadHistory()
    {
        return _store.Load(Path.Combine(_dataRoot, "history.json"), () => new HistoryLog());
    }

    public void SaveHistory(HistoryLog history)
    {
        _store.Save(Path.Combine(_dataRoot, "history.json"), history);
    }

    public Profile GetCurrentProfile(AppSettings settings, List<Profile> profiles)
    {
        if (settings.CurrentProfileId == Guid.Empty)
        {
            settings.CurrentProfileId = profiles[0].Id;
        }

        return profiles.FirstOrDefault(p => p.Id == settings.CurrentProfileId) ?? profiles[0];
    }
}
