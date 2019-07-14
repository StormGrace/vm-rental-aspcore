using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Message(string fname, int age)
        {
            //return Content($"Hello, {fname}, you are {age} years old!");
            return Content($"Hello, {fname}, you are {age} years old!");
        }
    }
}
