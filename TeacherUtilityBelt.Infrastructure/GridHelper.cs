using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Infrastructure;

public class GridHelper : IGridHelper
{
    public async Task<string[][]> GenerateRandomGrid(GridCoordinate coordinate)
    {
        string[][] s = new string[coordinate.Y][];
        Random random = new Random();

        for (short i = 0; i < coordinate.Y; i++)
        {
            s[i] = new string[coordinate.X];

            for (short j = 0; j < coordinate.X; j++)
            {
                Alphabet a = (Alphabet)random.Next(0,25);
                s[i][j] = a.ToString();
            }
        }

        return await Task.FromResult(s);
    }

}
