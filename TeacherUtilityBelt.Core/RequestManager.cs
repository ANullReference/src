using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.Extensions.Options;

namespace TeacherUtilityBelt.Core;

public class RequestManager : IRequestManager
{
    private IWordDictionary _wordDictionary;
    private IWordGridNavigator _wordGridNavigator;
    private IGridHelper _gridHelper;
    private ILogger<RequestManager> _logger;
    private AppSettings _appSettings;

    public RequestManager(IWordDictionary wordDictionary, IWordGridNavigator wordGridNavigator, IGridHelper gridHelper, ILogger<RequestManager> logger, IOptions<AppSettings> options)
    {
        _wordDictionary = wordDictionary;
        _wordGridNavigator = wordGridNavigator;
        _gridHelper = gridHelper;
        _logger = logger;
        _appSettings = options.Value;
    }

    /// <summary>
    /// Generate randdom word grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public async Task<GridAnswerResponse> GenerateRandomCrosswordGrid(GridCoordinate coordinate)
    {
        var dict = await _wordDictionary.GetWordDictionary("en");
        var maxDiagonal = coordinate.X;
        
        //maybe fetch only keys that apply....
        var keys = dict.Keys.Where(w => w.Length <= maxDiagonal && w.Length >= _appSettings.FoundWordMinCount).ToList();

        List<string> foundString = new List<string>();

        var gridNavigator = await _wordGridNavigator.GetNavigationPoints();
        var grid = await _gridHelper.GenerateRandomGrid(coordinate);

        GridAnswerResponse gridAnswerResponse = new () { Grid = grid, GridAnswer = new Dictionary<string, List<GridCoordinate>>() };

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                foreach(var gn in gridNavigator)
                {
                    var foundMatches = new List<GridCoordinate>() { new GridCoordinate(i, j) };


                    var found = await FoundWordsRecursion(grid, i, j, keys, grid[i][j], gn, foundMatches);

                    if (found == null)
                    {
                        continue;
                    }

                    if (gridAnswerResponse.GridAnswer.Keys.Any(a => a.Equals(found.Item1, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    gridAnswerResponse.GridAnswer.Add(found.Item1, found.Item2);
                }
            }
        }

        return gridAnswerResponse;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="GridAnswer"></param>
    /// <returns></returns>
    public async Task<GridAnswerResponse> GenerateCrosswordGrid(Dictionary<string, List<GridCoordinate>> gridAnswer, int dimension)
    {
        if (gridAnswer == null || gridAnswer.Count() == 0)
        {
            return null;
        }

        string[][] grid = await CreateEmptyGrid(dimension);

        foreach(var ga in gridAnswer)
        {


        }

        return null;
    }

    #region private methods

    private async Task<string[][]> CreateEmptyGrid(int dimension)
    {
        string[][] grid = new string[dimension][];

        for(int i = 0; i < dimension; i++ )
        {
            grid[i] =  new string[dimension];
        }
        
        return await Task.FromResult(grid);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="keyWords"></param>
    /// <param name="builtString"></param>
    /// <returns></returns>
    private async Task<Tuple<string, List<GridCoordinate>>> FoundWordsRecursion(string[][] s, int x, int y, List<string> keyWords, string builtString, GridCoordinate operation, List<GridCoordinate> foundMatches)
    {

        #region is the word found....
        var singleWordFound = keyWords
                        .Where(k => k.Equals(builtString, StringComparison.OrdinalIgnoreCase) && builtString.Length >= _appSettings.FoundWordMinCount)
                        .FirstOrDefault();

        if (!string.IsNullOrEmpty(singleWordFound))
        {
            return new Tuple<string, List<GridCoordinate>>(singleWordFound, foundMatches);
        }
        #endregion


        //word not found keep searching by incrementing navigation
        var nextX = x + operation.X;
        var nextY = y + operation.Y;

        if (nextX < 0 || nextY < 0 || nextY >= s.Length || nextX >= s.Length)
        {
            //out of bound situation
            return null;
        }
        
        builtString += s[nextX][nextY];
        foundMatches.Add(new GridCoordinate(nextX, nextY));

        PrintGrid(s, builtString, x, y);

       var filteredKeyList = keyWords
                            .Where(sel => sel.StartsWith(builtString, StringComparison.OrdinalIgnoreCase))
                            .ToList();
            
        if (filteredKeyList == null ||  filteredKeyList.Count() == 0)
        {
            //no words found that match built pattern
            return null;
        }

        return await FoundWordsRecursion(s, nextX , nextY, filteredKeyList.ToList() , builtString, operation, foundMatches);
    }
    private void PrintGrid(string [][] s, string lookingFor, int x, int y)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = 0; j < s[i].Length; j++)
            {
                sb.Append(string.Format($" {s[i][j]} "));
            }

            sb.AppendLine(string.Empty);
        }

        _logger.LogDebug(sb.ToString());
        _logger.LogDebug($"Looking for: {lookingFor} at {x},{y}");
    }

    #endregion
}