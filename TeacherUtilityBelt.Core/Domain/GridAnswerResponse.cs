namespace TeacherUtilityBelt.Core.Domain;

public class GridAnswerResponse
{
    /// <summary>
    /// 
    /// </summary>
    public string[][] Grid { get; set;}
    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, List<GridCoordinate>> GridAnswer { get; set;}
}
