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
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Desarrollador, Administrador, Secretario/a")]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Desarrollador, Administrador, Secretario/a, Instructor/Docente, Alumno/a")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Desarrollador, Administrador")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
