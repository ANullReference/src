using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;
using TeacherUtilityBelt.Models;

namespace TeacherUtilityBelt.Controllers;

public class FindWordController : Controller
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
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> RandomGridGeneration(int dimension)
    {
        if (dimension <= 4 || dimension >= 20)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = "Enter a dimension between 4 and 20 for grid size" });
        }

        var response = await _requestManager.GenerateRandomCrosswordGrid(new GridCoordinate(dimension,dimension));
        return View(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult GridGeneration(int dimension)
    {
        //var grid = 
        return View(dimension);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
