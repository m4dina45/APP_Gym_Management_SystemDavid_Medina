namespace GymManagementSystem.Domain.Entities
{
    public class Enrollment
    {
        public int MemberId { get; set; }
        public int GymClassId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Navigation properties
        public Member Member { get; set; }
        public GymClass GymClass { get; set; }
    }
}
