using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using vm_rental.Models;
using vm_rental.ViewModels;
using vm_rental.Data.Repository.Interface;
using vm_rental.Utility.Services.Email;
using vm_rental.Data.Model;
using System;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;

namespace vm_rental.Controllers
{
  public class SignController : Controller
  {
    private readonly CustomUserManager userManager;
    private readonly CustomSignInManager signInManager;
    private readonly IUserRepository userRepository;
    private readonly IEmailService emailService;

    public SignController(CustomUserManager customUserManager, IUserRepository userRepo, IEmailService emailService, CustomSignInManager signInMan)
    {
      userManager = customUserManager;
      userRepository = userRepo;
      this.emailService = emailService;
      signInManager = signInMan;
    }

    [HttpGet]
    [Route("[controller]/Signin")]
    [Route("Signin")]
    public IActionResult SignIn()
    {
      //string test = Request.Headers["Authorization"];
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(LoginViewModel loginVm)
    {
      if (ModelState.IsValid)
      {
        var result = await signInManager.PasswordSignInAsync(loginVm.Email, loginVm.Password, true, false);
     
        if (result.Succeeded)
        {
           RedirectToAction("Signin");
        }

        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

      }
      return View("Signin", loginVm);
    }

    [HttpGet]
    [Route("Sign/Signup")]
    public IActionResult SignUp()
    {
      return View(new ClientViewModel(userRepository));
    }
 
    [Route("confirm/{emailToken}")]
    public async Task<object> ConfirmEmail(string emailToken)
    {
      if (emailToken == null)
      {
        RedirectToAction("SignIn");
      }

      IdentityModelEventSource.ShowPII = true;

      var tokenHandler = new JwtSecurityTokenHandler();

      var validationParameters = new TokenValidationParameters()
      {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("cThIIoDvwdueQB468K5xDc5633seEFoqwxjF_xSJyQQ")),
        ValidAudience = "user",
        ValidIssuer = "vm_rental",
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true
      };

      tokenHandler.ValidateToken(emailToken, validationParameters, out SecurityToken validatedToken);

      JwtSecurityToken rawToken = (JwtSecurityToken)validatedToken;

      object key = rawToken.Payload["unique_name"];

      User user = new User()
      {
        Id = int.Parse(key.ToString())
      };

      bool isTokenValid = await userManager.VerifyUserTokenAsync(user, "CustomEmailConfirmation", "email-confirm", rawToken.RawData);
 
      if (isTokenValid)
      {
        return View("SignIn");
      }
      else
      {
        return View("SignUp");
      }
    }

    [HttpPost]
    [Route("Sign/Signup")]
    public async Task<ActionResult> SignUpAsync(ClientViewModel clientVM)
    {
      ClientValidator clientValidator = new ClientValidator(userRepository);
      ValidationResult validationResults = clientValidator.Validate(clientVM);

      if (validationResults.IsValid)
      {
        User user = await userManager.CreateUser(clientVM);

        string emailConfirmToken = await userManager.GenerateUserTokenAsync(user, "CustomEmailConfirmation", "email-confirm");

        //await userManager.SetAuthenticationTokenAsync(user, "CustomEmailConfirmation", "email-confirm", emailConfirmToken);
  
       // var HTTPClient = new HttpClient();

       /// HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + emailConfirmToken);

        emailService.SendEmailAsync(clientVM.Email, clientVM.OwnerName, emailConfirmToken, EmailSubject.EmailConfirmationSubject);

        RedirectToAction("SignIn");
      }
      else
      {
        validationResults.AddToModelState(ModelState, null);
      }

      return View("SignUp", clientVM);
    }
  }

}