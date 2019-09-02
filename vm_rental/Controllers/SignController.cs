using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using vm_rental.Data.Model;
using vm_rental.Data.Repository.Interface;
using vm_rental.ViewModels.Sign;
using vm_rental.Utility.Services.Email;
using vm_rental.Models.Identity;
using System;

namespace vm_rental.Controllers
{
  public class SignController : Controller
  {
    private readonly CustomUserManager _userManager;
    private readonly CustomSignInManager _signInManager;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    public SignController(CustomUserManager customUserManager, IUserRepository userRepo, IEmailService emailService, CustomSignInManager customsignInManager)
    {
      _userManager = customUserManager;
      _signInManager = customsignInManager;
      _userRepository = userRepo;
      _emailService = emailService;
    }

    [HttpGet("Signin")]
    public IActionResult SignIn()
    {
      return View();
    }

    [HttpGet("Signup")]
    public IActionResult SignUp()
    {
      return View(new SignUpViewModel());
    }

    [HttpPost("Signin")]
    public async Task<IActionResult> SignIn(SignInViewModel signInVM)
    {
      if (ModelState.IsValid)
      {
        var result = await _signInManager.PasswordSignInAsync(signInVM.Email, signInVM.Password, true, false);
     
        if (result.Succeeded)
        {
           return RedirectToAction("Signin");
        }

        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

      }
      return View("Signin", signInVM);
    }

    [HttpPost("Signup")]
    public async Task<ActionResult> SignUp(SignUpViewModel signUpVM)
    {
      SignUpValidator clientValidator = new SignUpValidator(_userRepository);

      ValidationResult validationResults = clientValidator.Validate(signUpVM);

      if (validationResults.IsValid)
      {
        try
        {
          User user = await _userManager.CreateUser(signUpVM);

          string emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

          string tokenURL = Url.Action("ConfirmEmail", "Account", new { token = emailConfirmToken }, HttpContext.Request.Scheme);

          _emailService.SendEmailAsync(signUpVM.Email, signUpVM.OwnerName, tokenURL, EmailSubject.EmailConfirmationSubject);

          return RedirectToAction("SignUp");
        }
        catch(Exception e)
        {
          return StatusCode(500);
        }
      }
      else
      {
        validationResults.AddToModelState(ModelState, null);
      }

      return View("SignUp", signUpVM);
    }
  }
}