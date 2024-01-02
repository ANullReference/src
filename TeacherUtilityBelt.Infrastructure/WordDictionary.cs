using System.Text.Json;
using TeacherUtilityBelt.Core.Abstractions;

namespace TeacherUtilityBelt.Infrastructure;

public class WordDictionary : IWordDictionary
{
    private ICacheManager _cacheManager;

    public WordDictionary(ICacheManager cacheManager)
    {
        _cacheManager = cacheManager;
    }

    public async Task<IDictionary<string, string>> GetWordDictionary(string language)
    {
        string key = $"dictionary_{language}";

        if (_cacheManager.Exists(key))
        {
            return (IDictionary<string, string>)_cacheManager.Get(key);
        }

        Dictionary<string, string> items = new Dictionary<string, string>();

        using (StreamReader r = new StreamReader($"./wwwroot/data/dictionary_{language}.json"))
        {
            string json = r.ReadToEnd();
            
            if (!string.IsNullOrEmpty(json))
            {
                items = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();    

                if (items.Count > 0)
                {
                    _cacheManager.Add(key, items);
                }
            }
        }
    
        return await Task.FromResult(items);
    }
}