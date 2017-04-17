using System.Collections.Generic;

namespace UBTalker.Services
{
    class DataStoreService : IDataStoreService
    {
        private Dictionary<string, object> _store = new Dictionary<string, object>();

        public object Get(string key)
        {
            return _store[key];
        }

        public void Set(string key, object value)
        {
            _store[key] = value;
        }
    }
}
