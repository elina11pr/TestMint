using ImageProcessingWebApp.Models;

namespace ImageProcessingWebApp.Services
{
    public class ProcessingJobServices : IProcessingJobServices
    {
        public (double TotalTime, Dictionary<string, int> ImagesPerWorker) CalculateJobDetails(Job job)
        {
            return job.CalculateJobDetails();
        }
    }
}
