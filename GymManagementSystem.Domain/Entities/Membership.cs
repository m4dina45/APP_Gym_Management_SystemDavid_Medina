using GymManagementSystem.Domain.Enums;

namespace GymManagementSystem.Domain.Entities
{
    public class Membership
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MembershipType MembershipType { get; set; }
        public decimal MonthlyPrice { get; set; }
        public int DurationMonths { get; set; }

        // Navigation properties
        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
