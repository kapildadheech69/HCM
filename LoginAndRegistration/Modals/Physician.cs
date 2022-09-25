using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginAndRegistration.Modals
{
    public class Physician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhysicianId { get; set; }
        public String PhysicianName { get; set; }
        public String PhysicianState { get; set; }
        public List<Member> Members { get; set; }
    }
}
