namespace GymManagementSystem.API.DTOs
{
    public class GymClassCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schedule { get; set; }
        public int MaxCapacity { get; set; }
        public int TrainerId { get; set; }
    }
}
