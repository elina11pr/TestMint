using ImageProcessingWebApp.Models;

namespace ImageProcessingWebApp.Tests
{
    public class JobTests
    {
        [Fact]
        public void CalculateJobDetails_ShouldCorrectlyAssignImagesAndTime()
        {
            int totalImages = 1000;
            var job = new Job(totalImages);
            job.AddWorker(new Worker { Name = "P1", Speed = 2 });
            job.AddWorker(new Worker { Name = "P2", Speed = 3 });
            job.AddWorker(new Worker { Name = "P3", Speed = 4 });

            var (totalTime, imagesPerWorker) = job.CalculateJobDetails();

            Assert.Equal(totalImages, imagesPerWorker["P1"] + imagesPerWorker["P2"] + imagesPerWorker["P3"]);

            Assert.Equal(462, imagesPerWorker["P1"]);
            Assert.Equal(308, imagesPerWorker["P2"]);
            Assert.Equal(230, imagesPerWorker["P3"]);

            Assert.Equal(924.0, totalTime);
        }
    }
}