namespace SelectAid.Persistence;

public class VersionedData<T>
{
    public int Version { get; set; }
    public T Data { get; set; } = default!;
}
