using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using vm_rental.Data.Model;
using vm_rental.Models.Identity;
using System;

namespace vm_rental.Controllers
{
  public class AuthController : Controller
  {
    private readonly CustomUserManager userManager;

    public AuthController(CustomUserManager customUserManager)
    {
      userManager = customUserManager;
    }

    [HttpGet("confirm/{emailToken}")]
    public async Task<object> ConfirmEmail(string emailToken)
    {
      if (emailToken == null)
      {
        return View("SignIn");
      }

      IdentityModelEventSource.ShowPII = true;

      var tokenHandler = new JwtSecurityTokenHandler();

      var validationParameters = new TokenValidationParameters()
      {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cThIIoDvwdueQB468K5xDc5633seEFoqwxjF_xSJyQQ")),
        ValidAudience = "user",
        ValidIssuer = ConfigurationManager.AppSettings["JWT:Iss"],
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true
      };

      bool isTokenValid = false;

      try
      {
        tokenHandler.ValidateToken(emailToken, validationParameters, out SecurityToken validatedToken);

        JwtSecurityToken rawToken = (JwtSecurityToken)validatedToken;

        object key = rawToken.Payload["unique_name"];

        User user = new User()
        {
          Id = int.Parse(key.ToString())
        };

         isTokenValid = await userManager.VerifyUserTokenAsync(user, "CustomEmailConfirmation", "email-confirm", rawToken.RawData);
      }
      catch (Exception e)
      {

      }

      if (isTokenValid)
      {
        return RedirectToAction("SignIn", "Sign");
      }
      else
      {
        return new StatusCodeResult(404);
      }
    }
  }
}
