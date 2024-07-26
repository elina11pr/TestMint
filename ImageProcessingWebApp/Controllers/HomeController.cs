using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ImageProcessingWebApp.Models;
using ImageProcessingWebApp.Services;

namespace ImageProcessingWebApp.Controllers;

public class HomeController : Controller
{
    private readonly IProcessingJobServices _processingJobServices;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IProcessingJobServices processingJobServices)
    {
        _processingJobServices = processingJobServices;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Calculate(int totalImages, List<Worker> workers)
    {
        try
        {
            var job = new Job(totalImages);

            foreach (var worker in workers)
            {
                job.AddWorker(worker);
            }

            var (totalTime, imagesPerWorker) = _processingJobServices.CalculateJobDetails(job);

            ViewBag.TotalTime = totalTime;
            ViewBag.ImagesPerWorker = imagesPerWorker;

            return PartialView("Results");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calculating job details.");
            return StatusCode(500, "Internal server error");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}