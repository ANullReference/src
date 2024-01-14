
using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Core.Abstractions;

public interface IRequestManager
{
    Task<GridAnswerResponse> GenerateRandomCrosswordGrid(GridCoordinate coordinate);

    Task<GridAnswerResponse> GenerateCrosswordGrid(Dictionary<string, List<GridCoordinate>> GridAnswer, int dimension);
}