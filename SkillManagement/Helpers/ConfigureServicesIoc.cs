using DataAccess.Repositories;

namespace SkillManagement.Helpers
{
    public static class ConfigureServicesIoc
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}