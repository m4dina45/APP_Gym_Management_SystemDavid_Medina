namespace GymManagementSystem.Domain.Entities
{
    public class GymClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schedule { get; set; }
        public int MaxCapacity { get; set; }
        public int TrainerId { get; set; }

        // Navigation properties
        public Trainer Trainer { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
