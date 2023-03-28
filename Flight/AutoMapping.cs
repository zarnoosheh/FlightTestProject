
using Microsoft.Extensions.DependencyInjection;

namespace Flight
{
    public static class AutoMapping
    {
        public static void AddMapping(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<, .ResultList>().ReverseMap();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
