using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
 

namespace vm_rental.Utility.Services.Auth.JWT
{
  public class JWTService : IAuthService
  {
    private readonly IConfiguration _configuration;

    public JWTService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public SecurityToken ReadToken(string receivedToken)
    {
      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

      JwtSecurityToken readToken = null;

      if (tokenHandler.CanReadToken(receivedToken))
      {
        readToken = (JwtSecurityToken)tokenHandler.ReadToken(receivedToken);
      }

      return readToken;
    }

    public bool HasTokenExpired(JwtSecurityToken token)
    {
      bool hasExpired = false;

      object expDate = token.Payload["exp"];

      if(expDate != null)
      {
        IConvertible jwtSeconds = expDate as IConvertible;

        if (jwtSeconds != null)
        {
          DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

          dateTime.AddSeconds(jwtSeconds.ToDouble(null));

          if (dateTime > DateTime.UtcNow)
          {
            hasExpired = true;
          }
        }
      }

      return hasExpired;
    }

    public bool IsTokenValid(string receivedToken, out JwtSecurityToken token)
    {
      bool isValid;

      byte[] secretKey = Encoding.UTF8.GetBytes(_configuration["jwt:SecretKey"]);

      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

      SecurityToken returnedToken = null;

      TokenValidationParameters validationParams = new TokenValidationParameters()
      {
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidIssuer = _configuration["jwt:Iss"],
        ValidAudience = _configuration["jwt:Aud"],
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
      };

      try
      {
        tokenHandler.ValidateToken(receivedToken, validationParams, out returnedToken);

        isValid = true;
      }
      catch (Exception)
      {
        isValid = false;
      }

      token = (JwtSecurityToken)returnedToken;

      return isValid;
    }

    public bool IsTokenValid(string receivedToken, TokenValidationParameters validationParams, out JwtSecurityToken token)
    {
      bool isValid;

      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

      SecurityToken returnedToken = null;

      try
      {
        SecurityToken readToken = ReadToken(receivedToken);
        
        tokenHandler.ValidateToken(receivedToken, validationParams, out returnedToken);

        isValid = true;
      }
      catch (Exception)
      {
        isValid = false;
      }

      token = (JwtSecurityToken)returnedToken;

      return isValid;
    }

    public string GenerateJwtToken(Claim[] claims, TimeSpan timeSpan)
    {
      DateTime dateCreated = DateTime.UtcNow;

      byte[] secretKey = Encoding.UTF8.GetBytes(_configuration["jwt:SecretKey"]);

      SymmetricSecurityKey key = new SymmetricSecurityKey(secretKey);

      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
      {
        Issuer   = _configuration["jwt:Iss"],
        Audience = _configuration["jwt:Aud"],
        Subject  = new ClaimsIdentity(claims),
        IssuedAt = dateCreated,
        Expires  = dateCreated + timeSpan,
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
      };

      IdentityModelEventSource.ShowPII = true;

      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
