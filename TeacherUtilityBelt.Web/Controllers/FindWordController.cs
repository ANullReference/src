using Microsoft.AspNetCore.Mvc;
using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Controllers;

[ApiController]
[Route("[controller]")]
public class FindWordController : ControllerBase
{
    private readonly ILogger<FindWordController> _logger;
    private readonly IRequestManager _requestManager;

    public FindWordController(ILogger<FindWordController> logger, IRequestManager requestManager)
    {
        _logger = logger;
        _requestManager = requestManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("GetGridAnswerResponse")]
    public async Task<GridAnswerResponse> GetGridAnswerResponse()
    {
        int dimension = 12;

        if (dimension <= 4 || dimension >= 20)
        {
            return null;//View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = "Enter a dimension between 4 and 20 for grid size" });
        }

        var response =  await _requestManager.GenerateRandomCrosswordGrid(new GridCoordinate(dimension,dimension));

        return response;
    }
}