using System;
using Forum.Services.Authentication;
using Forum.Models;
using Forum.Services.Users;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Forum.Services.Community;

namespace Forum
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment HostingEnvironment { get; }
    public Startup(IWebHostEnvironment env, IConfiguration configuration)
    {
      Configuration = configuration;
      HostingEnvironment = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAntiforgery(options =>
      {
        options.HeaderName = "X-XSRF-TOKEN";
      });

      services.AddDbContext<ForumContext>(options =>
      {
        options.UseNpgsql(Configuration.GetConnectionString("ForumConnection"));
      });

      services.AddMvc();
      services.AddSession();
      

      services.AddControllers(o =>
      {
        o.EnableEndpointRouting = false;
        //o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
      })
      .AddApplicationPart(System.Reflection.Assembly.GetExecutingAssembly())
      .AddControllersAsServices();

      services
        .AddControllersWithViews(o =>
        {
          o.EnableEndpointRouting = false;
          //o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        })
        .AddApplicationPart(System.Reflection.Assembly.GetExecutingAssembly())
        .AddControllersAsServices();

      services.AddHttpClient();
      services.AddRazorPages();

      //custom services
      services.AddTransient<UserService>();
      services.AddTransient<AuthenticationHelpers>();
      services.AddTransient<CommunityService>();
    }

    public void Configure(
      IApplicationBuilder app,
      IWebHostEnvironment env,
      ILoggerFactory loggerFactory,
      ForumContext dbContext,
      IAntiforgery antiforgery,
      IServiceProvider services
      )
    {
      app.Use((context, next) =>
      {
        context.Request.EnableBuffering();
        return next();
      });

      app.UseAuthentication();
      app.UseSession();
      app.UseHsts();
      app.UseMvc(routes =>
      {
      });
      app.UseRouting();
    }

  }
}