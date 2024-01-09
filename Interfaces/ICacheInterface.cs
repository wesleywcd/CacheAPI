namespace CacheAPI.Interfaces;

public interface ICacheInterface
{
    public string GetTotal();
    public string Add(string value);
    public string GetByKey(int key);
    public void Create(int size, int expirationMinutes);
    public string GetLastItem();

}
