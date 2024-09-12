#region REFERENCIAS
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.BL.Trainer___BL;
using KM.SysControlAdmin.EN.Trainer___EN;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;



#endregion

namespace KM.SysControlAdmin.WebApp.Controllers.Trainer___Controller
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Desarrollador")]
    public class TrainerController : Controller
    {
        // Creamos Una Instancia Para Acceder a Los Metodos
        TrainerBL trainerBL = new TrainerBL();

        #region METODO PARA MOSTRAR INDEX
        // Accion Para Mostrar La Vista Index
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Index(Trainer trainer = null!)
        {
            if (trainer == null)
                trainer = new Trainer();

            var trainers = await trainerBL.SearchAsync(trainer);
            return View(trainers);
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
        public async Task<IActionResult> Create(Trainer trainer, IFormFile imagen)
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

                    trainer.ImageData = imagenData; // Asigna el array de bytes al campo de la imagen en tu modelo Membership
                }
                trainer.DateCreated = DateTime.Now;
                trainer.DateModification = DateTime.Now;
                int result = await trainerBL.CreateAsync(trainer);
                TempData["SuccessMessageCreate"] = "Instructor/Docente Agregado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(trainer);
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
                Trainer trainer = await trainerBL.GetByIdAsync(new Trainer { Id = id });
                if (trainer == null)
                {
                    return NotFound();
                }
                // Convertir el array de bytes en imagen para mostrar en la vista
                if (trainer.ImageData != null && trainer.ImageData.Length > 0)
                {
                    ViewBag.ImageUrl = Convert.ToBase64String(trainer.ImageData);
                }
                return View(trainer);
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
        public async Task<IActionResult> Edit(int id, Trainer trainer, IFormFile imagen)
        {
            try
            {
                if (id != trainer.Id)
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
                    trainer.ImageData = imagenData; // Asignar el array de bytes de la nueva imagen a la entidad Membership
                }
                else
                {
                    // Si no se proporciona una nueva imagen, se conserva la imagen existente
                    Trainer existingTrainer = await trainerBL.GetByIdAsync(new Trainer { Id = id });
                    trainer.ImageData = existingTrainer.ImageData;
                }
                trainer.DateModification = DateTime.Now;
                int result = await trainerBL.UpdateAsync(trainer);
                TempData["SuccessMessageUpdate"] = "Instructor/Docente Modificado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(trainer); // Devolver la vista con el objeto Membership para que el usuario pueda corregir los datos
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
                Trainer trainer = await trainerBL.GetByIdAsync(new Trainer { Id = id });
                if (trainer == null)
                {
                    return NotFound();
                }
                // Convertir el array de bytes en imagen para mostrar en la vista
                if (trainer.ImageData != null && trainer.ImageData.Length > 0)
                {
                    ViewBag.ImageUrl = Convert.ToBase64String(trainer.ImageData);
                }
                return View(trainer); // Retornamos los Detalles a La Vista
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
                Trainer trainer = await trainerBL.GetByIdAsync(new Trainer { Id = id });

                if (trainer == null)
                {
                    return NotFound();
                }
                // Convertir el array de bytes en imagen para mostrar en la vista
                if (trainer.ImageData != null && trainer.ImageData.Length > 0)
                {
                    ViewBag.ImageUrl = Convert.ToBase64String(trainer.ImageData);
                }
                return View(trainer);
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
        public async Task<IActionResult> Delete(int id, Trainer trainer)
        {
            try
            {
                Trainer trainerDB = await trainerBL.GetByIdAsync(trainer);
                int result = await trainerBL.DeleteAsync(trainerDB);
                TempData["SuccessMessageDelete"] = "Instructor/Docente Eliminado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var trainerDB = await trainerBL.GetByIdAsync(trainer);
                if (trainerDB == null)
                    trainerDB = new Trainer();
                return View(trainerDB);
            }
        }
        #endregion
    
        #region METODO PARA REPORTE
        // Metodo Para Generar Ficha o Reporte En PDF 
        public async Task<ActionResult> GeneratePDFfile(int id)
        {
            var generatePDF = await trainerBL.GetByIdAsync(new Trainer { Id = id });
            string fileName = $"Ficha_{generatePDF.Name}_{generatePDF.LastName}_{generatePDF.Dui}_KM.pdf";
            return new ViewAsPdf("GeneratePDFfile", generatePDF)
            {
                FileName = fileName
            };
        }
        #endregion
    }
}
