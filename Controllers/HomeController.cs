using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_sentry_sample.Models;
using Sentry;

namespace my_sentry_sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            SentrySdk.ConfigureScope(scope => 
            {
                scope.SetTag("ambiente", "PRODUCCION");
                scope.User.Email = "Gonza";
            });

            
            // https://docs.sentry.io/error-reporting/capturing/?platform=csharp
            try {

            
            var a = 0;
            var b = 3/a;
            }
            catch(Exception err) {
                SentrySdk.CaptureException(err);
            }

            return View();
        }

        public IActionResult Privacy()
        {
             // MAE: este metodo de controlador tira execpcion y el usuario final vera una pantalla de error o bien un GUID para tracking interno y nosotros desde MAE podriamos levantarlo desde la tabla de errores
             // con Sentry este error queda registrado y categorizado en el dashboard
            var a = 0;
            var b = 3/a;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
