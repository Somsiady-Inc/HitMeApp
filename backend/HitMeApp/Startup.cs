using Autofac;
using HitMeApp.Api.Exceptions;
using HitMeApp.Indentity;
using HitMeApp.Shared.Infrastructure.Exceptions;
using HitMeApp.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HitMeApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HitMeApp", Version = "v1" });
            });
            services.AddErrorHandler();

            services.AddIndentityModule();
            services.AddUsersModule();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Autofac registrations
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HitMeApp v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseErrorHandler<GlobalFallbackExceptionMapper>();

            app.UseIdentityModule();
            app.UseUsersModule();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
