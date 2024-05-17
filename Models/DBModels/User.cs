using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Models.DBModels
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("IdentityUser")]
        [MaxLength(450)]
        [DataType("nvarchar")]
        public string AspNetUserId { get; set; }

        [MaxLength(128)]
        [DataType("nvarchar")]
        public string? Address { get; set; }

        public List<UserSkill>? UserSkills { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
