using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using vm_rental.Data.Interface;
using vm_rental.Data.Model;
using vm_rental.Data.Repository;
using vm_rental.ViewModels.Account;

namespace vm_rental.Controllers
{
    public class AccountController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientHistoryRepository _clientHistoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserHistoryRepository _userHistoryRepository;

        public AccountController(IClientRepository clientRepository, IClientHistoryRepository clientHistoryRepository,
                                 IUserRepository userRepository, IUserHistoryRepository userHistoryRepository)
        {
            _clientRepository = clientRepository;
            _clientHistoryRepository = clientHistoryRepository;
            _userRepository = userRepository;
            _userHistoryRepository = userHistoryRepository;
        }

        [Route("Account")]
        [Route("Account/Signin")]
        [Route("Account/Login")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("Account/Signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new ClientViewModel());
        }

        [HttpPost]
        public IActionResult SignUp(ClientViewModel clientVM)
        {
            ClientValidator clientValidator = new ClientValidator();
            ValidationResult clientValdiationResult = clientValidator.Validate(clientVM);

            if (clientValdiationResult.IsValid)
            { 
                Client client = _clientRepository.CreateClient(clientVM);
                User user = _userRepository.CreateUser(client);
                ClientHistory clientHistory = _clientHistoryRepository.CreateClientHistory(clientVM, client, user);
                UserHistory userHistory = _userHistoryRepository.CreateUserHistory(clientVM, user);
                  
                return RedirectToAction("SignUp");
            }
            else
            {
                foreach (ValidationFailure failer in clientValdiationResult.Errors)

                {

                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);

                }
            }

            return View("SignUp", clientVM);
        }
    }
}