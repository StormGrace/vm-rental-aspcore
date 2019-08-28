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

namespace vm_rental.Controllers
{
  public class SignController : Controller
  {
    private readonly CustomUserManager userManager;
    private readonly IUserRepository userRepository;
    private readonly IEmailService emailService;
    public SignController(CustomUserManager customUserManager, IUserRepository userRepo, IEmailService emailService)
    {
      userManager = customUserManager;
      userRepository = userRepo;
      this.emailService = emailService;
    }

    [Route("[controller]/Signin")]
    [Route("[controller]/Login")]
    public IActionResult SignIn()
    {
      return View();
    }

    [HttpGet]
    [Route("Sign/Signup")]
    public IActionResult SignUp()
    {
      return View(new ClientViewModel(userRepository));
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
        string emailToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
        emailService.SendEmailAsync(clientVM.FirstName, clientVM.Email);

        return RedirectToAction("SignUp");
      }
      else
      {
        validationResults.AddToModelState(ModelState, null);
      }

      return View("SignUp", clientVM);
    }
  }
}