using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using vm_rental.Data.Interface;
using vm_rental.Data.JSON;
using vm_rental.Models;
using vm_rental.ViewModels;

namespace vm_rental.Controllers
{
  public class SignController : Controller
  {
    private readonly ISignManager _signManager;
    private readonly IUserHistoryRepository _userHistoryRepository;

    public SignController(ISignManager signManager, IUserHistoryRepository userHistoryRepository){
      _signManager = signManager;
      _userHistoryRepository = userHistoryRepository;
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
      ValidationResult validationResults = clientValidator.Validate(clientVM);

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
  }
}