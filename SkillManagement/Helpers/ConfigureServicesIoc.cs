using DataAccess.Repositories;
using Models.ViewModels;
using Services;
using Services.Internfaces;
using System.Reflection;

namespace SkillManagement.Helpers
{
    public static class ConfigureServicesIoc
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddTransient<LoginViewModel>();

            //Serivces
            RegisterAsTransient(services, typeof(MainService).Assembly, typeof(IMainService).Assembly);

            return services;
        }

        private static void RegisterAsTransient(IServiceCollection services, Assembly implementationAssembly, Assembly interfacesAssembly)
        {
            var interfaceTypes = interfacesAssembly.GetTypes().Where(t => t.IsInterface);
            foreach (Type iType in interfaceTypes)
            {
                var implementation = implementationAssembly.GetTypes().FirstOrDefault(x => iType.IsAssignableFrom(x) & !x.IsInterface);
                if (implementation != null)
                {
                    services.AddTransient(iType, implementation);
                }
            }
        }
    }
}