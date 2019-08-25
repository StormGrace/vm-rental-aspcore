using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vm_rental.Data.Repository.Interface;
using vm_rental.Models.Utility.Email;
using vm_rental.Data.Repository;
using vm_rental.Models.Services;
using vm_rental.ViewModels;
using Microsoft.AspNetCore.Identity;
using vm_rental.Models;
using Westwind.AspNetCore.LiveReload;
using FluentValidation.AspNetCore;
using vm_rental.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using vm_rental.Data.JSON;
using vm_rental.Data.Model;

namespace vm_rental
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddLiveReload();

      //UserManager Config
      services.AddTransient<UserManager<User>, CustomUserManager>();
      //

      services.AddTransient<FluentValidation.IValidator<ClientViewModel>, ClientValidator>();

      //Email Config
      services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
      services.AddTransient<IEmailService, EmailService>();
      //

      services.ConfigureMySqlContext(Configuration);

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IClientRepository, ClientRepository>();

      services.AddScoped<IClientHistoryRepository, ClientHistoryRepository>();
      services.AddScoped<IUserHistoryRepository, UserHistoryRepository>();

      //Identity Framework
      services.AddScoped<IdentityUser<int>, User>();
      services.AddScoped<UserManager<User>, CustomUserManager>();
      services.AddScoped<IUserStore<User>, UserRepository>();
      //

      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.ConfigureApplicationCookie(options =>
      {
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        options.Cookie.Name = "vm_rental";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.LoginPath = "/Sign/Login";
        //ReturnUrlParameter requires 
        //using Microsoft.AspNetCore.Authentication.Cookies;
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.SlidingExpiration = true;
      });

      services.ConfigureMySqlContext(Configuration);


      //Identity Framework
      services.AddIdentity<User, UserRole>()
              .AddEntityFrameworkStores<VmDbContext>()
              .AddUserManager<CustomUserManager>()
              .AddUserStore<UserRepository>()
              .AddDefaultTokenProviders();
      //

      services.AddMvc()
        .AddFluentValidation(fv =>
        {
          fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
          fv.RegisterValidatorsFromAssemblyContaining<Startup>();
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseLiveReload();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();
      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");

          routes.MapRoute(
            name: "SignUp",
            template: "{controller=Sign}/{action=SignUp}/{id?}");

          routes.MapRoute(
            name: "SignIn",
            template: "{controller=Sign}/{action=SignIn}/{id?}");
      });

      DbInitializer.Seed(app);
      JSONRepository.Initialize();
    }
  }
}
