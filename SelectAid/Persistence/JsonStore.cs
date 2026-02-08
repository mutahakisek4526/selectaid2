using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SelectAid.Persistence;

public class JsonStore
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    public T Load<T>(string path, Func<T> createDefault)
    {
        if (!File.Exists(path))
        {
            var created = createDefault();
            Save(path, created);
            return created;
        }

        var json = File.ReadAllText(path);
        var data = JsonSerializer.Deserialize<T>(json, _options);
        if (data == null)
        {
            data = createDefault();
            Save(path, data);
        }

        return data;
    }

    public void Save<T>(string path, T data)
    {
        var json = JsonSerializer.Serialize(data, _options);
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
        File.WriteAllText(path, json);
    }

    public VersionedData<T> LoadVersioned<T>(string path, Func<T> createDefault, int currentVersion, Func<T, int, T>? migrate = null)
    {
        if (!File.Exists(path))
        {
            var created = new VersionedData<T> { Version = currentVersion, Data = createDefault() };
            Save(path, created);
            return created;
        }

        var json = File.ReadAllText(path);
        using var doc = JsonDocument.Parse(json);
        if (doc.RootElement.TryGetProperty("version", out var versionProp) && doc.RootElement.TryGetProperty("data", out _))
        {
            var versioned = JsonSerializer.Deserialize<VersionedData<T>>(json, _options) ?? new VersionedData<T> { Version = currentVersion, Data = createDefault() };
            if (versioned.Version < currentVersion && migrate != null)
            {
                versioned.Data = migrate(versioned.Data, versioned.Version);
                versioned.Version = currentVersion;
                Save(path, versioned);
            }

            return versioned;
        }

        var legacy = JsonSerializer.Deserialize<T>(json, _options) ?? createDefault();
        var migrated = migrate != null ? migrate(legacy, 0) : legacy;
        var wrapped = new VersionedData<T> { Version = currentVersion, Data = migrated };
        Save(path, wrapped);
        return wrapped;
    }
}
