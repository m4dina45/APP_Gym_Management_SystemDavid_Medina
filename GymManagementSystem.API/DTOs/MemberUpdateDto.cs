namespace GymManagementSystem.API.DTOs
{
    public class MemberUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int MembershipId { get; set; }
    }
}
