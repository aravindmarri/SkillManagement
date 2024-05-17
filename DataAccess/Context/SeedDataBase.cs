using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models.DBModels;

namespace DataAccess.Context
{
    public class SeedDataBase
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<SkillManagementContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                IdentityUser identityUser = new IdentityUser
                {
                    Email = "test@yopmail.com",
                    UserName = "test1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                User user = new User
                {
                    AspNetUserId = identityUser.Id,
                    Address = "Vermillion, SD",
                    UserSkills = new List<UserSkill>
                    {
                        new UserSkill
                        {
                            SkillId = 1
                        },
                        new UserSkill
                        {
                            SkillId = 2
                        }
                    }
                };
                await userManager.CreateAsync(identityUser, "Abcd@1234");
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }
    }
}

