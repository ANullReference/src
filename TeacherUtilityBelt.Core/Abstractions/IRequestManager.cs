
using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Core.Abstractions;

public interface IRequestManager
{
    Task<GridAnswerResponse> GenerateCrosswordGrid(GridCoordinate coordinate);
}