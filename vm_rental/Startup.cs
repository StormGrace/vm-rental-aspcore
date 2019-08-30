﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Westwind.AspNetCore.LiveReload;
using vm_rental.Data.Repository.Interface;
using vm_rental.Data.Repository;
using vm_rental.Models.Services;
using vm_rental.ViewModels;
using vm_rental.Models;
using vm_rental.Data;
using vm_rental.Data.JSON;
using vm_rental.Data.Model;
using vm_rental.Utility.Services.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
      //services.AddTransient<SignInManager<User>, CustomSignInManager>();
      //

      services.AddTransient<FluentValidation.IValidator<ClientViewModel>, ClientValidator>();

      //Email Config
      services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
      services.AddTransient<IEmailService, EmailService>();
      //

      services.ConfigureMySqlContext(Configuration);

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IClientRepository, ClientRepository>();
      services.AddScoped<IUserTokenRepository, UserTokenRepository>();

      services.AddScoped<IClientHistoryRepository, ClientHistoryRepository>();
      services.AddScoped<IUserHistoryRepository, UserHistoryRepository>();

      //Identity Framework
      services.AddScoped<IdentityUser<int>, User>();
      services.AddScoped<UserManager<User>, CustomUserManager>();
      services.AddScoped<IUserStore<User>, UserRepository>();
      services.AddScoped<IPasswordHasher<User>, CustomPasswordHasher>();
      services.AddScoped<IUserAuthenticationTokenStore<User>, UserRepository>();
      //

      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      

      }).AddJwtBearer("Bearer", options =>
      {
        var signingKey = Convert.FromBase64String(Configuration["JWT:Key"]);
        options.SaveToken = true;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = Configuration["JTW:Site"],
          ValidIssuer = Configuration["JTW:Site"],
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(signingKey)
        };
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
      services.AddIdentity<User, UserRole>(config =>
      {
        config.SignIn.RequireConfirmedEmail = true;

        config.Tokens.ProviderMap.Add("CustomEmailConfirmation", new TokenProviderDescriptor
        (
          typeof(CustomEmailConfirmationTokenProvider<User>))
        );

        config.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

      }).AddEntityFrameworkStores<VmDbContext>()
        .AddUserManager<CustomUserManager>()
        .AddSignInManager<CustomSignInManager>()
        .AddUserStore<UserRepository>()
        .AddDefaultTokenProviders()
        .AddCustomEmailTokenProvider();
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

      VmDbInitializer.Seed(app);
      JSONRepository.Initialize();
    }
  }
}
