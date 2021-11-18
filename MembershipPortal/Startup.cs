using MembershipPortal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MembershipPortal.ErrorModel;
using Microsoft.AspNetCore.Mvc.Formatters.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace MembershipPortal
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorPol", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowCredentials().
                    AllowAnyHeader()
                    .WithMethods("PUT", "DELETE", "GET","POST");
                });
            });
         services.AddDbContextPool<RepositoryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MemberDBCon")));
         services.AddScoped<IMemberRepository,RepositorySQLImplementation>();
         services.AddScoped<IPublicationRepository, PublicationSQLImplementation>();
            //services.AddControllersWithViews().AddJsonOptions(options => 
            //{ options.JsonSerializerOptions.WriteIndented = true; });
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.OutputFormatters.Insert(0, new CsvOutputFormatter());
            }).AddXmlDataContractSerializerFormatters().AddJsonOptions(options =>
            { options.JsonSerializerOptions.WriteIndented = true; });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<RepositoryContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
