namespace TeacherUtilityBelt.Core.Domain;

public class GridCoordinate
{
    public int X  { get; set; }
    public int Y  { get; set; }

    public GridCoordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return string.Format("X: {0} Y: {1}", X, Y);
    }
}  