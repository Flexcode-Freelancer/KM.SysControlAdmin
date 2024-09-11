#region REFERENCIAS
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.BL.Rol___BL;
using KM.SysControlAdmin.BL.User___BL;
using KM.SysControlAdmin.EN.Role___EN;
using KM.SysControlAdmin.EN.User___EN;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;


#endregion

namespace KM.SysControlAdmin.WebApp.Controllers.User___Controller
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Desarrollador")]
    public class UserController : Controller
    {
        // Creamos Las Instancias Para Acceder a Los Metodos
        UserBL userBL = new UserBL();
        RoleBL roleBL = new RoleBL();

        #region METODO PARA INDEX
        // Metodo Para Mostrar La Vista Index
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Index(User user = null!)
        {
            if (user == null)
                user = new User();
            if (user.Top_Aux == 0)
                user.Top_Aux = 10; // setear el número de registros a mostrar
            else if (user.Top_Aux == -1)
                user.Top_Aux = 0;

            var users = await userBL.SearchIncludeRoleAsync(user);
            var roles = await roleBL.GetAllAsync();

            ViewBag.Roles = roles;
            ViewBag.Top = user.Top_Aux;

            return View(users);
        }
        #endregion

        #region METODO PARA GUARDAR
        // Accion Que Muestra El Formulario
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Create()
        {
            var roles = await roleBL.GetAllAsync();
            ViewBag.Roles = roles;
            return View();
        }

        // Accion Que Recibe Los Datos y Los Envia a La Base De Datos
        [Authorize(Roles = "Desarrollador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, IFormFile imagen)
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

                    user.ImageData = imagenData; // Asigna el array de bytes al campo de la imagen en tu modelo Membership
                }
                user.DateCreated = DateTime.Now;
                user.DateModification = DateTime.Now;
                int result = await userBL.CreateAsync(user);
                TempData["SuccessMessageCreate"] = "Usuario Agregado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.Roles = await roleBL.GetAllAsync();
                return View(user);
            }
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Accion Que Muestra El Formulario
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userBL.GetByIdAsync(new User { Id = id });
            user.Role = await roleBL.GetByIdAsync(new Role { Id = user.Id });
            ViewBag.Roles = await roleBL.GetAllAsync();
            return View(user);
        }

        // Accion Que Recibe Los Datos y Los Envia a La Base De Datos
        [Authorize(Roles = "Desarrollador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            try
            {
                int result = await userBL.UpdateAsync(user);
                TempData["SuccessMessageUpdate"] = "Usuario Modificado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.Roles = await roleBL.GetAllAsync();
                return View(user);
            }
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Accion Que Muestra El Formulario
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userBL.GetByIdAsync(new User { Id = id });
            user.Role = await roleBL.GetByIdAsync(new Role { Id = user.IdRole });

            return View(user);
        }

        // Accion Que Recibe Los Datos y Los Envia a La Base De Datos
        [Authorize(Roles = "Desarrollador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, User user)
        {
            try
            {
                int result = await userBL.DeleteAsync(user);
                TempData["SuccessMessageDelete"] = "Usuario Eliminado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                var userDb = await userBL.GetByIdAsync(user);
                if (userDb == null)
                    userDb = new User();
                if (userDb.Id > 0)
                    userDb.Role = await roleBL.GetByIdAsync(new Role { Id = userDb.IdRole });
                return View(userDb);
            }
        }
        #endregion

        #region METODO PARA DETALLES
        // Accion Que Muestra El Formulario
        [Authorize(Roles = "Desarrollador")]
        public async Task<IActionResult> Details(int id)
        {
            var user = await userBL.GetByIdAsync(new User { Id = id });
            user.Role = await roleBL.GetByIdAsync(new Role { Id = user.IdRole });
            return View(user);
        }
        #endregion

        #region METODO DE INICIO DE SESION Y CERRAR SESION (LOGIN, LOGOUT)
        // Accion Que Muestra El Formulario
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null!)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = returnUrl;
            ViewBag.Error = "";
            return View();
        }

        // Accion Que Ejecuta La Autenticacion Del Usuario
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user, string returnUrl = null!)
        {
            try
            {
                var userDb = await userBL.LoginAsync(user);
                if (userDb != null && userDb.Id > 0 && userDb.Email == user.Email)
                {
                    userDb.Role = await roleBL.GetByIdAsync(new Role { Id = userDb.IdRole });
                    var claims = new[] { new Claim(ClaimTypes.Name, userDb.Email), new Claim(ClaimTypes.Role, userDb.Role.Name) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
                else
                    throw new Exception("Credenciales Incorrectas, Vuelve a Intentarlo");

                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.Url = returnUrl;
                ViewBag.Error = e.Message;
                return View(new User { Email = user.Email });
            }
        }

        // Accion Que Permite Cerrar La Sesion
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl = null!)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
        #endregion

        #region METODO PARA CAMBIAR LA CONTRASEÑA
        // Accion Que Muestra El Formulario
        public async Task<IActionResult> ChangePassword()
        {
            var users = await userBL.SearchAsync(new User { Email = User.Identity!.Name!, Top_Aux = 1 });
            var actualUser = users.FirstOrDefault();
            return View(actualUser);
        }

        // Accion Que Recibe La Contraseña Actualizada y La Envia a La Base De Datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(User user, string oldPassword)
        {
            try
            {
                int result = await userBL.ChangePasswordAsync(user, oldPassword);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "User");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                var users = await userBL.SearchAsync(new User { Email = User.Identity!.Name!, Top_Aux = 1 });
                var actualUser = users.FirstOrDefault();
                return View(actualUser);
            }
        }
        #endregion
    }
}