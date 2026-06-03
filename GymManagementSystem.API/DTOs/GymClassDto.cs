namespace GymManagementSystem.API.DTOs
{
    public class GymClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schedule { get; set; }
        public int MaxCapacity { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; }
    }
}
