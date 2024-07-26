namespace ImageProcessingWebApp.Models
{
    public class Job
    {
        public int TotalImages { get; }
        private List<Worker> Workers { get; } = new List<Worker>();

        public Job(int totalImages)
        {
            TotalImages = totalImages;
        }

        public void AddWorker(Worker worker)
        {
            Workers.Add(worker);
        }

        public (double TotalTime, Dictionary<string, int> ImagesPerWorker) CalculateJobDetails()
        {
            if (Workers.Count == 0)
                return (0.0, new Dictionary<string, int>());

            double totalMinutesPerImageInverse = Workers.Sum(worker => 1 / worker.Speed);
            var imagesPerWorkerFractional = Workers.ToDictionary(
                worker => worker.Name,
                worker => (1 / worker.Speed / totalMinutesPerImageInverse) * TotalImages
            );

            var imagesPerWorker = new Dictionary<string, int>();
            int totalAssignedImages = 0;

            foreach (var worker in Workers)
            {
                int assignedImages = (int)Math.Round(imagesPerWorkerFractional[worker.Name]);
                imagesPerWorker[worker.Name] = assignedImages;
                totalAssignedImages += assignedImages;
            }

            int difference = TotalImages - totalAssignedImages;

            if (difference != 0)
            {
                var sortedWorkers = Workers
                    .OrderByDescending(worker => imagesPerWorkerFractional[worker.Name] - imagesPerWorker[worker.Name])
                    .ToList();

                foreach (var worker in sortedWorkers)
                {
                    if (difference == 0) break;

                    if (difference > 0)
                    {
                        imagesPerWorker[worker.Name]++;
                        difference--;
                    }
                    else
                    {
                        imagesPerWorker[worker.Name]--;
                        difference++;
                    }
                }
            }

            double maxTime = Workers.Max(worker => imagesPerWorker[worker.Name] * worker.Speed);
            return (maxTime, imagesPerWorker);
        }
    }
}
