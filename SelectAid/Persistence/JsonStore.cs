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
}
