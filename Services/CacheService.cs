using CacheAPI.Interfaces;
using CacheAPI.Singletons;

namespace CacheAPI.Services
{
    public class CacheService : ICacheInterface
    {
        public CacheService()
        {
        }

        public string GetTotal()
        {
            return $"{ UnitCache.Instance._lastItem } item(s)";  
        }

        public string GetByKey(int key)
        {
            return UnitCache.Instance.GetByKey(key);
        }

        public void Create(int size, int expirationMinutes)
        {
            UnitCache.Instance._size = size;
            UnitCache.Instance._expiration = expirationMinutes;
            
            UnitCache.Instance.Create();
        }

        public string GetLastItem()
        {
            return UnitCache.Instance.GetLastItem();
        }

        public string Add(string value)
        {
            return UnitCache.Instance.Add(value);
        }
    }
}