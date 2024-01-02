using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Infrastructure;

public class WordGridNavigation : IWordGridNavigator
{
    public async Task<List<Coordinate>> GetNavigationPoints()
    {
        var list =  new List<Coordinate>() 
        {
            new Coordinate (-1,0), //left 1 unit
            new Coordinate (1,0), // right 1 unit
            new Coordinate (0,1), // up 1 unit
            new Coordinate (0,-1), // down 1 unit
            new Coordinate (1,1), // down right 1 unit
            new Coordinate (-1,-1), // left up right 1 unit
            new Coordinate (1,-1), // right up 1 unit
            new Coordinate (-1,1) // left down 1 unit
        };

        return await Task.FromResult(list);
    }

}
