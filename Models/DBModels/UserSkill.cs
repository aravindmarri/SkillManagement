using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DBModels
{
    public class UserSkill
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Skill")]
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
