using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vm_rental.Data.Repository.Interface;

namespace vm_rental.Models
{
  //Change to Email Token Provider ->                               v
  public class CustomEmailConfirmationTokenProvider<User>: DataProtectorTokenProvider<User> where User : vm_rental.Data.Model.User
  {
    private readonly IUserTokenRepository userTokenRepository;

    public CustomEmailConfirmationTokenProvider(IUserTokenRepository userTokenRepository, IDataProtectionProvider dataProtectionProvider, IOptions<EmailConfirmationTokenProviderOptions> options)
    : base(dataProtectionProvider, options)
    {
      this.userTokenRepository = userTokenRepository;
    }

    public override Task<string> GenerateAsync(string purpose, UserManager<User> userManager, User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var keyStr = Encoding.ASCII.GetBytes("cThIIoDvwdueQB468K5xDc5633seEFoqwxjF_xSJyQQ");
      var key = new SymmetricSecurityKey(keyStr);

      var ep = new EncryptingCredentials(key, SecurityAlgorithms.RsaSha256, SecurityAlgorithms.RsaSha256);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Issuer = "vm_rental",
        Audience = "user",
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString())
        }),
        IssuedAt = DateTime.UtcNow,
        Expires = DateTime.UtcNow.AddHours(24),
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256),
        EncryptingCredentials = ep
      };

      IdentityModelEventSource.ShowPII = true;

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return Task.FromResult(tokenHandler.WriteToken(token));
    }

    public override Task<bool> ValidateAsync(string purpose, string token, UserManager<User> userManager, User user)
    {
      return Task.FromResult(userTokenRepository.isTokenValid("CustomEmailConfirmation", purpose, token, user));
    }
  }
  public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
  {
    public EmailConfirmationTokenProviderOptions()
    {
      Name = "CustomEmailConfirmation";
      TokenLifespan = TimeSpan.FromHours(24);
    }
  }
}
