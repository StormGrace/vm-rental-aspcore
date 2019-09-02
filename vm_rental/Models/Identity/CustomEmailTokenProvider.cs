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

namespace vm_rental.Models.Identity
{
  public class CustomEmailConfirmationTokenProvider<User>: DataProtectorTokenProvider<User> where User : vm_rental.Data.Model.User
  { 
    public CustomEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<EmailConfirmationTokenProviderOptions> options): base(dataProtectionProvider, options){}

    public override Task<string> GenerateAsync(string purpose, UserManager<User> userManager, User user)
    {
      var keyStr = Encoding.ASCII.GetBytes("cThIIoDvwdueQB468K5xDc5633seEFoqwxjF_xSJyQQ");

      var key = new SymmetricSecurityKey(keyStr);

      // var ep = new EncryptingCredentials(key, SecurityAlgorithms.RsaSha256, SecurityAlgorithms.RsaSha256);
      var tokenHandler = new JwtSecurityTokenHandler();

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
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
        //EncryptingCredentials = ep
      };

      IdentityModelEventSource.ShowPII = true;

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return Task.FromResult("https://localhost:5001" + "/confirm/" + tokenHandler.WriteToken(token));
    }

    public override Task<bool> ValidateAsync(string purpose, string token, UserManager<User> userManager, User user)
    {
      //return Task.FromResult(userTokenRepository.isTokenValid("CustomEmailConfirmation", purpose, token, user));
      return Task.FromResult(true);
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
