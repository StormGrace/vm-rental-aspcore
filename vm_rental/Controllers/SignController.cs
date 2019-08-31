using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using vm_rental.Data.Model;
using vm_rental.Data.Repository.Interface;
using vm_rental.ViewModels.Sign;
using vm_rental.Utility.Services.Email;
using vm_rental.Models.Identity;


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

    [HttpGet("[controller]/Signin")]
    public IActionResult SignIn()
    {
      return View();
    }

    [HttpGet("[controller]/Signup")]
    public IActionResult SignUp()
    {
      return View(new SignUpViewModel(userRepository));
    }

    [HttpPost("[controller]/Signin")]
    public async Task<IActionResult> SignIn(SignInViewModel signInVM)
    {
      if (ModelState.IsValid)
      {
        var result = await signInManager.PasswordSignInAsync(signInVM.Email, signInVM.Password, true, false);
     
        if (result.Succeeded)
        {
           return RedirectToAction("Signin");
        }

        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

      }
      return View("Signin", signInVM);
    }

    [HttpPost("[controller]/Signup")]
    public async Task<ActionResult> SignUp(SignUpViewModel signUpVM)
    {
      SignUpValidator clientValidator = new SignUpValidator(userRepository);
      ValidationResult validationResults = clientValidator.Validate(signUpVM);

      if (validationResults.IsValid)
      {
        User user = await userManager.CreateUser(signUpVM);

        string emailConfirmToken = await userManager.GenerateUserTokenAsync(user, "CustomEmailConfirmation", "email-confirm");

        emailService.SendEmailAsync(signUpVM.Email, signUpVM.OwnerName, emailConfirmToken, EmailSubject.EmailConfirmationSubject);
      }
      else
      {
        validationResults.AddToModelState(ModelState, null);
      }

      return View("SignUp", signUpVM);
    }
  }
}