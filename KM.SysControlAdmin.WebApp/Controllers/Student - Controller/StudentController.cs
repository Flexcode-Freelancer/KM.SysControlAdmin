#region REFERENCIAS
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.BL.Student___BL;
using KM.SysControlAdmin.BL.User___BL;
using KM.SysControlAdmin.EN.Student___EN;
using KM.SysControlAdmin.EN.Trainer___EN;
using KM.SysControlAdmin.EN.User___EN;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


#endregion

namespace KM.SysControlAdmin.WebApp.Controllers.Student___Controller
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Desarrollador")]
    public class StudentController : Controller
    {
        // Creamos Una Instancia Para Acceder a Los Metodos
        StudentBL studentBL = new StudentBL();
        UserBL userBL = new UserBL();

        #region METODO PARA MOSTRAR INDEX
        // Accion Para Mostrar La Vista Index
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Index(Student student = null!)
        {
            if (student == null)
                student = new Student();

            var students = await studentBL.SearchAsync(student);
            return View(students);
        }
        #endregion

        #region METODO PARA CREAR
        // Accion Para Mostrar La Vista De Crear
        [Authorize(Roles = "Desarrollador")]
        public ActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // Accion Que Recibe Los Datos Del Formulario Para Ser Enviados a La BD
        [Authorize(Roles = "Desarrollador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student, IFormFile imagen)
        {
            try
            {
                // Mapeo de img a ArrayByte
                if (imagen != null && imagen.Length > 0)
                {
                    byte[] imagenData = null!;
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        imagenData = memoryStream.ToArray();
                    }

                    student.ImageData = imagenData; // Asigna el array de bytes al campo de la imagen en tu modelo Membership
                }
                student.DateCreated = DateTime.Now;
                student.DateModification = DateTime.Now;
                int result = await studentBL.CreateAsync(student);

                // Crear un nuevo objeto de tipo User y mapear las propiedades de Trainer con Server
                var user = new User
                {
                    IdRole = 4,
                    Name = student.Name,
                    LastName = student.LastName,
                    Email = student.Email,
                    Password = student.Password,
                    Status = student.Status,
                    DateCreated = student.DateCreated,
                    DateModification = student.DateModification,
                    ImageData = student.ImageData,
                };

                // Guardar en la tabla User
                int resultUser = await userBL.CreateAsync(user);
                TempData["SuccessMessageCreate"] = "Estudiante Agregado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Acción que muestra la vista de modificar
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Student student = await studentBL.GetByIdAsync(new Student { Id = id });
                if (student == null)
                {
                    return NotFound();
                }
                // Convertir el array de bytes en imagen para mostrar en la vista
                if (student.ImageData != null && student.ImageData.Length > 0)
                {
                    ViewBag.ImageUrl = Convert.ToBase64String(student.ImageData);
                }
                return View(student);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(); // Devolver la vista sin ningún objeto Membership
            }
        }

        // Acción que recibe los datos del formulario para ser enviados a la base de datos
        [Authorize(Roles = "Desarrollador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student, IFormFile imagen)
        {
            try
            {
                if (id != student.Id)
                {
                    return BadRequest();
                }
                if (imagen != null && imagen.Length > 0) // Verificar si se ha subido una nueva imagen
                {
                    byte[] imagenData = null!;
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        imagenData = memoryStream.ToArray();
                    }
                    student.ImageData = imagenData; // Asignar el array de bytes de la nueva imagen a la entidad Membership
                }
                else
                {
                    // Si no se proporciona una nueva imagen, se conserva la imagen existente
                    Student existingStudent = await studentBL.GetByIdAsync(new Student { Id = id });
                    student.ImageData = existingStudent.ImageData;
                }
                student.DateModification = DateTime.Now;
                int result = await studentBL.UpdateAsync(student);
                TempData["SuccessMessageUpdate"] = "Estudiante Modificado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }
        #endregion

        #region METODO PARA MOSTRAR DETALLES
        // Accion Que Muestra El Detalle De Un Registro
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                Student student = await studentBL.GetByIdAsync(new Student { Id = id });
                if (student == null)
                {
                    return NotFound();
                }
                // Convertir el array de bytes en imagen para mostrar en la vista
                if (student.ImageData != null && student.ImageData.Length > 0)
                {
                    ViewBag.ImageUrl = Convert.ToBase64String(student.ImageData);
                }
                return View(student); // Retornamos los Detalles a La Vista
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(); // Devolver la vista sin ningún objeto Membership
            }
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Accion Que Muestra La Vista De Eliminar
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Student student = await studentBL.GetByIdAsync(new Student { Id = id });

                if (student == null)
                {
                    return NotFound();
                }
                // Convertir el array de bytes en imagen para mostrar en la vista
                if (student.ImageData != null && student.ImageData.Length > 0)
                {
                    ViewBag.ImageUrl = Convert.ToBase64String(student.ImageData);
                }
                return View(student);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(); // Devolver la vista sin ningún objeto Membership
            }
        }

        // Accion Que Recibe Los Datos Del Formulario Para Ser Enviados a La BD
        [Authorize(Roles = "Desarrollador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Student student)
        {
            try
            {
                Student studentDB = await studentBL.GetByIdAsync(student);
                int result = await studentBL.DeleteAsync(studentDB);
                TempData["SuccessMessageDelete"] = "Estudiante Eliminado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var studentDB = await studentBL.GetByIdAsync(student);
                if (studentDB == null)
                    studentDB = new Student();
                return View(studentDB);
            }
        }
        #endregion
    }
}
