using Microsoft.AspNetCore.Identity;
using Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOModels
{
    public class UserDto
    {
        public int Id { get; set; }

        public string AspNetUserId { get; set; }

        public string? Address { get; set; }

        public List<UserSkillDto>? UserSkills { get; set; }
    }
}
