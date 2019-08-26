using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using vm_rental.Models;
using vm_rental.ViewModels;
using System;
using vm_rental.Data.Model;
using System.Threading.Tasks;
using vm_rental.Data.JSON;
using vm_rental.Data.Repository.Interface;
using vm_rental.Models.Utility.Email;

namespace vm_rental.Controllers
{
  public class SignController : Controller
  {
    private readonly CustomUserManager userManager;
    private readonly IEmailService emailServices;
    private readonly IUserRepository userRepository;

    public SignController(CustomUserManager customUserManager, IUserRepository userRepo, IEmailService emailServ)
    {
      userManager = customUserManager;
      userRepository = userRepo;
            emailServices = emailServ;
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

        IdentityResult result = await userManager.CreateUser(clientVM);
        emailServices.Send(clientVM.FirstName, clientVM.Email);
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