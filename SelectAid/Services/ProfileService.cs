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
        var wrapped = _store.LoadVersioned(Path.Combine(_dataRoot, "settings.json"), () => new AppSettings(), Migration.CurrentVersion, (data, version) => Migration.MigrateSettings(data, version));
        return wrapped.Data;
    }

    public void SaveSettings(AppSettings settings)
    {
        _store.Save(Path.Combine(_dataRoot, "settings.json"), new VersionedData<AppSettings> { Version = Migration.CurrentVersion, Data = settings });
    }

    public List<Profile> LoadProfiles()
    {
        var wrapped = _store.LoadVersioned(Path.Combine(_dataRoot, "profiles.json"), () => new List<Profile> { new Profile() }, Migration.CurrentVersion);
        var profiles = wrapped.Data;
        if (profiles.Count == 0)
        {
            profiles.Add(new Profile());
        }

        return profiles;
    }

    public void SaveProfiles(List<Profile> profiles)
    {
        _store.Save(Path.Combine(_dataRoot, "profiles.json"), new VersionedData<List<Profile>> { Version = Migration.CurrentVersion, Data = profiles });
    }

    public List<KeyboardLayout> LoadKeyboardLayouts()
    {
        var wrapped = _store.LoadVersioned(Path.Combine(_dataRoot, "keyboardLayouts.json"), SampleData.CreateDefaultLayouts, Migration.CurrentVersion);
        return wrapped.Data;
    }

    public void SaveKeyboardLayouts(List<KeyboardLayout> layouts)
    {
        _store.Save(Path.Combine(_dataRoot, "keyboardLayouts.json"), new VersionedData<List<KeyboardLayout>> { Version = Migration.CurrentVersion, Data = layouts });
    }

    public PhraseCatalog LoadPhrases()
    {
        var wrapped = _store.LoadVersioned(Path.Combine(_dataRoot, "phrases.json"), SampleData.CreateDefaultPhrases, Migration.CurrentVersion);
        return wrapped.Data;
    }

    public void SavePhrases(PhraseCatalog catalog)
    {
        _store.Save(Path.Combine(_dataRoot, "phrases.json"), new VersionedData<PhraseCatalog> { Version = Migration.CurrentVersion, Data = catalog });
    }

    public UserDictionary LoadUserDictionary()
    {
        var wrapped = _store.LoadVersioned(Path.Combine(_dataRoot, "userDict.json"), () => new UserDictionary(), Migration.CurrentVersion);
        return wrapped.Data;
    }

    public void SaveUserDictionary(UserDictionary dict)
    {
        _store.Save(Path.Combine(_dataRoot, "userDict.json"), new VersionedData<UserDictionary> { Version = Migration.CurrentVersion, Data = dict });
    }

    public HistoryLog LoadHistory()
    {
        var wrapped = _store.LoadVersioned(Path.Combine(_dataRoot, "history.json"), () => new HistoryLog(), Migration.CurrentVersion);
        return wrapped.Data;
    }

    public void SaveHistory(HistoryLog history)
    {
        _store.Save(Path.Combine(_dataRoot, "history.json"), new VersionedData<HistoryLog> { Version = Migration.CurrentVersion, Data = history });
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
