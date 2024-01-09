using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace CacheAPI.Singletons;

public sealed class UnitCache
{
    public int _total;
    public int _size = 3;
    public int _lastItem;
    public int _expiration = 1;
    public Dictionary<int, string> items;
    private const string _cacheName = "cache";

    private static UnitCache instance = null;
    private IMemoryCache _memoryCache;
    private static readonly object locker = new object();

    UnitCache()
    {
        items = new Dictionary<int, string>();
    }

    public static UnitCache Instance
    {
        get
        {
            lock (locker)
            {
                if (instance == null)
                    instance = new UnitCache();

                return instance;
            }
        }
    }

    public string GetByKey(int key)
    {
        _lastItem = key;
        _memoryCache.TryGetValue(key, out string value);
        items.TryGetValue(key, out string itemRemoved);
        items.Remove(key);
        items.Add(key, itemRemoved);
        return value;
    }

    public string Add(string value)
    {
        if(_memoryCache == null) Create();

        string itemRemoved = string.Empty;
        if (_total >= _size)
        {
            _memoryCache.Remove(items.FirstOrDefault());
            var item = items.FirstOrDefault();
            items.Remove(items.FirstOrDefault().Key);
            itemRemoved = $", item {item.Key} removed, value: {item.Value}";
        }
        
        _total++;
        _lastItem = _total;
        _memoryCache.GetOrCreate(_total, item => { return value; });
        items.Add(_total, value);
        
        return $"Last key inserted is {_total} {itemRemoved}";
    }

    public void Create()
    {
        if (_memoryCache != null)
            throw new Exception($"Already exists a cache, last item is {_lastItem}");
        
        _memoryCache = new MemoryCache(new MemoryCacheOptions()
        {
            ExpirationScanFrequency = TimeSpan.FromMinutes(_expiration)
        });
    }

    public string GetLastItem()
    {
        if (_memoryCache == null)
            return "Cache empty";
        
        var item = _memoryCache.Get(_lastItem);

        return $"Key: {_lastItem}, value: {item}";
    }
}
