using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vm_rental.Data.Interface;
using vm_rental.Data.Model;
using vm_rental.Models;

namespace vm_rental.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public HomeController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
