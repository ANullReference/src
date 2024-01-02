namespace TeacherUtilityBelt.Core.Abstractions;

public interface ICacheManager
{
    void Add(string key, object obj);
    bool Exists(string key);
    object Get(string key);
}
