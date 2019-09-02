using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using vm_rental.Data.Model;
using vm_rental.Models.Identity;
using vm_rental.Utility.Services.Auth;
using vm_rental.Utility.Services.Auth.JWT;

namespace vm_rental.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly CustomUserManager _userManager;
        private readonly JWTService _jwtService;

      public AccountController(CustomUserManager userManager, IAuthService authService)
      {
        _userManager = userManager;
        _jwtService = (JWTService)authService;
      }

      [HttpGet("confirm")]
      public object ConfirmEmail(string token)
      {
        if (token == null)
        {
          return RedirectToAction("Index", "Home");
        }

        bool isTokenValid = _jwtService.IsTokenValid(token, out JwtSecurityToken validatedToken);

        if (isTokenValid)
        {
          object userName = validatedToken.Payload["username"];
          //object audience = validatedToken.Payload["aud"];

          if (userName != null)
          {
            _userManager.ConfirmUserEmail(new User(){UserName = userName.ToString()});

            return View("EmailConfirmed");
          }
          else if (_jwtService.HasTokenExpired(validatedToken))
          {
            //Email Token Expired
          }
        }

        return RedirectToAction("Index", "Home");
      }
    }
}