using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
