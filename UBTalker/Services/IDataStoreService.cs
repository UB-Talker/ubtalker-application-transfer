namespace UBTalker.Services
{
    interface IDataStoreService
    {
        void Set(string key, object value);

        object Get(string key);
    }
}
