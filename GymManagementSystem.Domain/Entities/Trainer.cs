namespace GymManagementSystem.Domain.Entities
{
    public class Trainer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Navigation properties
        public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();
    }
}
