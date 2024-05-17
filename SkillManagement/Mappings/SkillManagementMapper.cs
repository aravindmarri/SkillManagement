using AutoMapper;
using Models.DBModels;
using Models.DTOModels;

public class SkillManagementMapper : Profile
{
    public SkillManagementMapper()
    {
        CreateMap<SkillDto, Skill>().ReverseMap();
        CreateMap<UserSkillDto, UserSkill>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
    }
}