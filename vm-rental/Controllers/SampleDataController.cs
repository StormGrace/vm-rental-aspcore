using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace vm_rental.Controllers
{
  [Route("api/[controller]")]
  public class SampleDataController : Controller
  {
    [HttpGet("[action]")]
    public IEnumerable<String> WeatherForecasts()
    {
      return null; 
    }
  }
}
