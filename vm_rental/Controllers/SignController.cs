using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using vm_rental.Data.Interface;
using vm_rental.Data.JSON;
using vm_rental.Data.Model;
using vm_rental.Models;
using vm_rental.ViewModels;


namespace vm_rental.Controllers
{
  public class SignController : Controller
  {
    private readonly ISignManager _signManager;
    private readonly IUserHistoryRepository _userHistoryRepository;
    private readonly IEmailService _emailService;

    public SignController(IUserManager userManager, IUserHistoryRepository userHistoryRepository){
      _userManager = userManager;
      _userHistoryRepository = userHistoryRepository;
      _emailService = emailService;
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
      return View(new ClientViewModel(_userHistoryRepository));
    }

    [HttpPost]
    public IActionResult SignUp(ClientViewModel clientVM)
    {
      ClientValidator clientValidator = new ClientValidator(_userHistoryRepository);
      ValidationResult results = clientValidator.Validate(clientVM);

      if (validationResults.IsValid)
      {
        _signManager.RegisterClient(clientVM);

         return RedirectToAction("SignUp");
      }
      else
      {
        validationResults.AddToModelState(ModelState, null);
      }

      return View("SignUp", clientVM);
    }

    public IActionResult EmailExists(string Email)
    {
      bool result;

      if (Email.Equals("nightavenger54@abv.bg"))
      {
        result = false;
      }
      else
      {
        result = true;
      }
      return Json(result);
    }

    public IActionResult CountryIsValid(string State)
     {
      bool result;

      if (JSONRepository.countries.CountryExists(State))
      {
        result = true;
      }
      else
      {
        result = false;
      }

      return Json(result);
    }
  }
}