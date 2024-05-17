namespace Models.DTOModels
{
    public class UserSkillDto
    {
        public int UserId { get; set; }
        public int SkillId { get; set; }

        public SkillDto Skill { get; set;}
    }
}