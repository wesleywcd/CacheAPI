using CacheAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CacheAPI.Controllers;

public class CacheController : Controller
{
    private readonly ICacheInterface _cache;

    public CacheController(ICacheInterface cacheInterface)
    {
        _cache = cacheInterface;
    }

    [HttpGet("total")]
    public IActionResult GetTotal()
    {
        return Ok(_cache.GetTotal());
    }
    
    [HttpGet("get-by-key")]
    public IActionResult GetByKey(int key)
    {
        return Ok(_cache.GetByKey(key));
    }
    
    [HttpGet("get-last-item")]
    public IActionResult GetLastItem()
    {
        return Ok(_cache.GetLastItem());
    }
    
    [HttpPost("create-cache")]
    public IActionResult Create([FromBody] int size, int expirationMinutes)
    {
        _cache.Create(size, expirationMinutes);
        return Ok();
    }
    
    [HttpPost("add-item")]
    public IActionResult GetById([FromBody] string value)
    {
        if (string.IsNullOrEmpty(value))
            value = new Random().Next(99999).ToString();
        
        return Ok(_cache.Add(value));
    }
}