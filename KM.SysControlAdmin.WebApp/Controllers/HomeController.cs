#region REFERENCIAS
/// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.WebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


#endregion

namespace KM.SysControlAdmin.WebApp.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Desarrollador")]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Desarrollador")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Desarrollador")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
