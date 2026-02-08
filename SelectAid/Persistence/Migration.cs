using SelectAid.Models;

namespace SelectAid.Persistence;

public static class Migration
{
    public const int CurrentVersion = 1;

    public static AppSettings MigrateSettings(AppSettings settings, int fromVersion)
    {
        settings.Version = CurrentVersion;
        return settings;
    }

    public static Profile MigrateProfile(Profile profile, int fromVersion)
    {
        return profile;
    }
}
