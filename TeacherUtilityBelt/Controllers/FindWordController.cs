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
    public async Task<IActionResult> Index()
    {
        var response = await _requestManager.GenerateCrosswordGrid(new GridCoordinate(19,19));
        return View(response);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
