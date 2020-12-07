using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;


namespace Mezubo.Ruleta.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            string redisServer = Configuration.GetValue<string>("RedisServer");
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisServer);
            services.AddScoped(x => redis.GetDatabase());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {          
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
           
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roulette V1");
                c.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
