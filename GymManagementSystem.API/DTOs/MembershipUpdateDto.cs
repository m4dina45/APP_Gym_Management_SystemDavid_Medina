using GymManagementSystem.Domain.Enums;

namespace GymManagementSystem.API.DTOs
{
    public class MembershipUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MembershipType MembershipType { get; set; }
        public decimal MonthlyPrice { get; set; }
        public int DurationMonths { get; set; }
    }
}
