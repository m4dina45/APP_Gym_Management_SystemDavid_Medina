namespace GymManagementSystem.API.DTOs
{
    public class EnrollmentDto
    {
        public int MemberId { get; set; }
        public int GymClassId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string MemberName { get; set; }
        public string ClassName { get; set; }
    }
}
