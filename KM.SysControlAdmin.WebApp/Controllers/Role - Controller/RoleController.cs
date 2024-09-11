#region REFERENCIAS
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.BL.Rol___BL;
using KM.SysControlAdmin.EN.Role___EN;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


#endregion

namespace KM.SysControlAdmin.WebApp.Controllers.Role___Controller
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Desarrollador")]
    public class RoleController : Controller
    {
        // Creamos Las Instancias Para Acceder a Los Metodos
        RoleBL roleBL = new RoleBL();

        #region METODO PARA INDEX
        // Metodo Para Mostrar La Vista Index
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Index(Role role = null!)
        {
            if (role == null)
                role = new Role();

            var roles = await roleBL.SearchAsync(role);
            return View(roles);
        }
        #endregion

        #region METODO PARA GUARDAR
        // Metodo Para Mostrar La Vista Guardar
        [Authorize(Roles = "Desarrollador")]
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // Metodo Que Recibe y Envia a La Base De Datos
        [Authorize(Roles = "Desarrollador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            try
            {
                int result = await roleBL.CreateAsync(role);
                TempData["SuccessMessageCreate"] = "Rol Agregado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }
        #endregion

        #region METODO PARA MODIFICAR
        [Authorize(Roles = "Desarrollador")]
        // Metodo Para Mostrar La Vista De Modificar
        public async Task<IActionResult> Edit(int id)
        {
            var role = await roleBL.GetByIdAsync(new Role { Id = id });
            ViewBag.Error = "";
            return View(role);
        }

        // Metodo Que Recibe y Envia a La Base De Datos
        [Authorize(Roles = "Desarrollador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Role role)
        {
            try
            {
                int result = await roleBL.UpdateAsync(role);
                TempData["SuccessMessageUpdate"] = "Rol Modificado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Mostrar La Vista De Eliminar
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await roleBL.GetByIdAsync(new Role { Id = id });
            ViewBag.Error = "";
            return View(role);
        }

        // Metodo Que Recibe y Envia a La Base De Datos
        [Authorize(Roles = "Desarrollador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Role role)
        {
            try
            {
                int result = await roleBL.DeleteAsync(role);
                TempData["SuccessMessageDelete"] = "Rol Eliminado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }
        #endregion

        #region METODO PARA DETALLES
        // Metodo que Muestra La Vista De Detalles
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Details(int id)
        {
            var role = await roleBL.GetByIdAsync(new Role { Id = id });
            return View(role);
        }
        #endregion
    }
}
