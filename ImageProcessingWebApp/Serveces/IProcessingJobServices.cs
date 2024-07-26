using ImageProcessingWebApp.Models;

namespace ImageProcessingWebApp.Services
{
    public interface IProcessingJobServices
    {
        (double TotalTime, Dictionary<string, int> ImagesPerWorker) CalculateJobDetails(Job job);
    }
}
