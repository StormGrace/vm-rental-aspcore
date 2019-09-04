using Microsoft.AspNetCore.Mvc;

namespace vm_rental.Areas.Controllers.CPanel
{
    [Area("CPanel")]  
    public class AccountController : Controller
    {
        public IActionResult Account()
        {
            return View();
        }
    }
}