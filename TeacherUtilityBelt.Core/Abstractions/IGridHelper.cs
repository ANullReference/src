using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Core.Abstractions;

public interface IGridHelper
{
    public Task<string[][]> GenerateRandomGrid(GridCoordinate coordinate);
}
