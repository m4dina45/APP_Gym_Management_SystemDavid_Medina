namespace GymManagementSystem.API.DTOs
{
    public class EnrollmentUpdateDto
    {
        public int MemberId { get; set; }
        public int GymClassId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
