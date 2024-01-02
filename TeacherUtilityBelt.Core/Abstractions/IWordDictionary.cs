
namespace TeacherUtilityBelt.Core.Abstractions;

public interface IWordDictionary
{
    /// <summary>
    /// Get dictionnary
    /// </summary>
    /// <returns></returns>
    Task<IDictionary<string, string>> GetWordDictionary(string language);   
}