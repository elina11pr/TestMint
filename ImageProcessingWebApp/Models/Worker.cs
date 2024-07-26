namespace ImageProcessingWebApp.Models
{
    public class Worker
    {
        public string Name { get; set; } = string.Empty;
        public double Speed { get; set; } = 1.0;

        public Worker() { }

        public Worker(string name, double speed)
        {
            Name = name;
            Speed = speed;
        }
    }
}
