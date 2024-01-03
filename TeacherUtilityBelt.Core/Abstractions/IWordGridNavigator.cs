using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Core.Abstractions;

public interface IWordGridNavigator
{
    Task<List<GridCoordinate>> GetNavigationPoints();
}