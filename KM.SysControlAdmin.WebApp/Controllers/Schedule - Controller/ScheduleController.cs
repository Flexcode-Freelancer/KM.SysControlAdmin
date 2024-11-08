#region REFERENCIAS
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.BL.Schedule___BL;
using KM.SysControlAdmin.EN.Schedule___EN;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace KM.SysControlAdmin.WebApp.Controllers.Schedule___Controller
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Desarrollador, Administrador, Secretario/a")]
    public class ScheduleController : Controller
    {
        // Instancia De La Clase Logica De Negocio
        ScheduleBL scheduleBL = new ScheduleBL();

        #region METODO PARA INDEX
        // Metodo Para Mostrar La Vista Index
        [Authorize(Roles = "Desarrollador, Administrador, Secretario/a")]
        public async Task<IActionResult> Index(Schedule schedule = null!)
        {
            if (schedule == null)
                schedule = new Schedule();

            var schedules = await scheduleBL.SearchAsync(schedule);
            return View(schedules);
        }
        #endregion

        #region METODO PARA GUARDAR
        // Metodo Para Mostrar La Vista Guardar
        [Authorize(Roles = "Desarrollador, Administrador, Secretario/a")]
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // Metodo Que Recibe y Envia a La Base De Datos
        [Authorize(Roles = "Desarrollador, Administrador, Secretario/a")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Schedule schedule)
        {
            try
            {
                int result = await scheduleBL.CreateAsync(schedule);
                TempData["SuccessMessageCreate"] = "Horario Agregado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(schedule);
            }
        }
        #endregion

        #region METODO PARA MODIFICAR
        [Authorize(Roles = "Desarrollador, Administrador, Secretario/a")]
        // Metodo Para Mostrar La Vista De Modificar
        public async Task<IActionResult> Edit(int id)
        {
            var schedule = await scheduleBL.GetByIdAsync(new Schedule { Id = id });
            ViewBag.Error = "";
            return View(schedule);
        }

        // Metodo Que Recibe y Envia a La Base De Datos
        [Authorize(Roles = "Desarrollador, Administrador, Secretario/a")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Schedule schedule)
        {
            try
            {
                int result = await scheduleBL.UpdateAsync(schedule);
                TempData["SuccessMessageUpdate"] = "Horario Modificado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(schedule);
            }
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Mostrar La Vista De Eliminar
        [Authorize(Roles = "Desarrollador, Administrador, Secretario/a")]
        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await scheduleBL.GetByIdAsync(new Schedule { Id = id });
            ViewBag.Error = "";
            return View(schedule);
        }

        // Metodo Que Recibe y Envia a La Base De Datos
        [Authorize(Roles = "Desarrollador, Administrador, Secretario/a")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Schedule schedule)
        {
            try
            {
                int result = await scheduleBL.DeleteAsync(schedule);
                TempData["SuccessMessageDelete"] = "Horario Eliminado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(schedule);
            }
        }
        #endregion

        #region METODO PARA DETALLES
        // Metodo que Muestra La Vista De Detalles
        [Authorize(Roles = "Desarrollador, Administrador, Secretario/a")]
        public async Task<IActionResult> Details(int id)
        {
            var schedule = await scheduleBL.GetByIdAsync(new Schedule { Id = id });
            return View(schedule);
        }
        #endregion
    }
}
