#region REFERENCIAS
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.BL.Course__BL;
using KM.SysControlAdmin.BL.Schedule___BL;
using KM.SysControlAdmin.BL.Trainer___BL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace KM.SysControlAdmin.WebApp.Controllers.Course___Controller
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Desarrollador, Instructor/Docente")]
    public class CourseAssignmentTrainerController : Controller
    {
        // Creamos las instancias para acceder a los métodos
        CourseBL courseBL = new CourseBL();
        TrainerBL trainerBL = new TrainerBL();
        ScheduleBL scheduleBL = new ScheduleBL();

        #region MÉTODO PARA MOSTRAR CURSOS ASIGNADOS AL INSTRUCTOR
        // Acción para mostrar la vista con los cursos asignados al instructor autenticado
        [Authorize(Roles = "Desarrollador, Instructor/Docente")]
        public async Task<IActionResult> Index()
        {
            // Obtener los cursos asignados al instructor autenticado
            var courses = await courseBL.GetAllByTrainerAsync(User);

            // Cargar datos relacionados (si aplica)
            var trainer = await trainerBL.GetAllAsync();
            var schedule = await scheduleBL.GetAllAsync();

            // Pasar datos a la vista mediante ViewBag
            ViewBag.Trainers = trainer;
            ViewBag.Schedule = schedule;

            // Retornar la vista con la lista de cursos
            return View(courses);
        }
        #endregion

    }
}
