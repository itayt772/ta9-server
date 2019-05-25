namespace ta9.Core.interfaces
{
    public interface IDataStorageService<T>
    {
        T Get(string key);
        void Save(string key, T entity);
        void Remove(string key);
    }
}