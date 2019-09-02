using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using vm_rental.Data;
using vm_rental.Data.Model;
using vm_rental.Data.Repository;
using vm_rental.Data.Repository.Interface;
using vm_rental.Utility.Services.Email;
using vm_rental.Utility.Security.Hashing.Argon;
using vm_rental.Utility.Services.Email.Mailkit;
using vm_rental.Models.Identity;
using vm_rental.ViewModels.Sign;
using vm_rental.Utility.Services.Auth;
using vm_rental.Utility.Services.Auth.JWT;

namespace vm_rental.ServiceExtensions
{
    public static class ServiceExtensions
    {
        //Configures the MySQL connection parameters, required to establish a valid Db connection.
        public static void ConfigureMySQL(this IServiceCollection services, IConfiguration config)
        {
          services.AddDbContext<VmDbContext>(db => db.UseMySql(config["connectionStrings:MySQL"]));
        }
        
        //Configures the Email connection parameters, used during all Email related operations.
        public static void ConfigureEmail(this IServiceCollection services, IConfiguration config)
        {
          services.AddTransient<IEmailService, MailkitService>();
          services.AddSingleton<IEmailConfiguration>(config.GetSection("EmailConfig").Get<EmailConfiguration>());
        }
        
        //Configures all JWT-related Dependencies.
        public static void ConfigureJWT(this IServiceCollection services)
        {
          services.AddScoped<IAuthService, JWTService>();
        }

        //Configures all Repository Dependencies, used to abide the Repository Pattern.
        public static void ConfigureRepositoryDI(this IServiceCollection services)
        {
          //Repositories.
          services.AddScoped<IUserRepository, UserRepository>();
          services.AddScoped<IClientRepository, ClientRepository>();
          services.AddScoped<IUserTokenRepository, UserTokenRepository>();

          //History Repositories.
          services.AddScoped<IUserHistoryRepository, UserHistoryRepository>();
          services.AddScoped<IClientHistoryRepository, ClientHistoryRepository>();
        }

        //Configures all Identity-Framework related Dependencies.
        public static void ConfigureIdentityDI(this IServiceCollection services, IConfiguration config)
        {
          //Custom User.
          services.AddScoped<IdentityUser<int>, User>();
          
          //Custom Managers.
          services.AddScoped<UserManager<User>, CustomUserManager>();
          services.AddScoped<SignInManager<User>, CustomSignInManager>();

          //Custom Stores.
          services.AddScoped<IUserStore<User>, UserRepository>();
          services.AddScoped<IUserAuthenticationTokenStore<User>, UserRepository>();

          //Custom PasswordHasher.
          services.AddScoped<IPasswordHasher<User>, CustomPasswordHasher>();
          services.AddScoped<IArgonConfiguration, ArgonConfiguration>();
          services.AddSingleton<IArgonConfiguration>(config.GetSection("hashing").GetSection("passwordHashing").Get<ArgonConfiguration>());
        }
        
        //Configures all Input Validators.
        public static void ConfigureFluentValidators(this IServiceCollection services)
        {
          services.AddTransient<FluentValidation.IValidator<SignUpViewModel>, SignUpValidator>();
        }
    }
}