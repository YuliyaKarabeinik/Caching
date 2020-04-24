namespace Infrastructure
{
    public interface ICache
    {
        object Get(string key);

        void Set(string key, object value);
    }
}