namespace MiJenner.ConfigUtils
{
    public interface IUserSettingsHandler<T>
    {
        bool TryWrite(T settings);
        bool TryRead(out T settings);
        public string FilePath { get; set; }
    }
}
