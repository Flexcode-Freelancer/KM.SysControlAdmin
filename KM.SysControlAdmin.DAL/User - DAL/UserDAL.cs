#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.EN.User___EN;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace KM.SysControlAdmin.DAL.User___DAL
{
    public class UserDAL
    {
        #region METODO PARA ENCRIPTAR
        // Metodo Para Encriptar Via MD5 El Password
        private static void EncryptMD5(User user)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(user.Password));
                var encryptedStr = "";
                for (int i = 0; i < result.Length; i++)
                {
                    encryptedStr += result[i].ToString("x2").ToLower();
                }
                user.Password = encryptedStr;
            }
        }
        #endregion

        #region METODO PARA VALIDAR EXISTENCIA DEL USUARIO
        // Metodo Para Validar La Existencia o No De Un Usuario
        private static async Task<bool> ExistsUser(User user, ContextDB dbContext)
        {
            bool result = false;
            var userLoginExists = await dbContext.User.FirstOrDefaultAsync(u => u.Email == user.Email && u.Id != user.Id);
            if (userLoginExists != null && userLoginExists.Id > 0 && userLoginExists.Email == user.Email)
                result = true;

            return result;
        }
        #endregion

        #region METODO PARA GUARDAR
        // Metodo Para Guardar Un Nuevo Registro En La Base De Datos
        public static async Task<int> CreateAsync(User user)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                bool UserExists = await ExistsUser(user, dbContext);
                if (UserExists == false)
                {
                    user.DateCreated = DateTime.Now;
                    user.DateModification = DateTime.Now;
                    EncryptMD5(user);
                    dbContext.User.Add(user);
                    result = await dbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("Usuario Ya Existente, Vuelve a Intentarlo.");
            }
            return result;
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente De La Base De Datos
        public static async Task<int> UpdateAsync(User user)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                bool UserExists = await ExistsUser(user, dbContext);
                if (UserExists == false)
                {
                    var userDb = await dbContext.User.FirstOrDefaultAsync(u => u.Id == user.Id);
                    userDb!.Name = user.Name;
                    userDb.LastName = user.LastName;
                    userDb.Email = user.Email;
                    userDb.Status = user.Status;
                    userDb.DateModification = DateTime.Now;
                    userDb.ImageData = user.ImageData;
                    userDb.IdRole = user.IdRole;

                    dbContext.User.Update(userDb);
                    result = await dbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("Usuario Ya Existente, Vuelve a Intentarlo.");
            }
            return result;
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public static async Task<int> DeleteAsync(User user)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var userDb = await dbContext.User.FirstOrDefaultAsync(u => u.Id == user.Id);
                dbContext.User.Remove(userDb!);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }
        #endregion

        #region METODO PARA MOSTRAR TODOS
        // Metodo Para Listar y Mostrar Todos Los Resultados
        public static async Task<List<User>> GetAllAsync()
        {
            var users = new List<User>();
            using (var dbContext = new ContextDB())
            {
                users = await dbContext.User.ToListAsync();
            }
            return users;
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Obtener Un Registro Por Su Id
        public static async Task<User> GetByIdAsync(User user)
        {
            var userDb = new User();
            using (var dbContext = new ContextDB())
            {
                userDb = await dbContext.User.FirstOrDefaultAsync(u => u.Id == user.Id);
            }
            return userDb!;
        }
        #endregion

        #region METODO PARA FILTRAR EN BASE A PARAMETROS
        // Metodo Para Filtrar Resultados De La Busqueda En Base a Parametros 
        internal static IQueryable<User> QuerySelect(IQueryable<User> query, User user)
        {
            if (user.Id > 0)
                query = query.Where(u => u.Id == user.Id);

            if (user.IdRole > 0)
                query = query.Where(u => u.IdRole == user.IdRole);

            if (!string.IsNullOrWhiteSpace(user.Name))
                query = query.Where(u => u.Name.Contains(user.Name));

            if (!string.IsNullOrWhiteSpace(user.LastName))
                query = query.Where(u => u.LastName.Contains(user.LastName));

            if (!string.IsNullOrWhiteSpace(user.Email))
                query = query.Where(u => u.Email.Contains(user.Email));

            if (user.Status > 0)
                query = query.Where(u => u.Status == user.Status);

            if (user.DateCreated.Year > 1000)
            {
                DateTime inicialDate = new DateTime(user.DateCreated.Year, user.DateCreated.Month, user.DateCreated.Day, 0, 0, 0);
                DateTime finalDate = inicialDate.AddDays(1).AddMilliseconds(-1);
                query = query.Where(u => u.DateCreated >= inicialDate && u.DateCreated <= finalDate);
            }

            query = query.OrderByDescending(u => u.Id).AsQueryable();

            if (user.Top_Aux > 0)
                query = query.Take(user.Top_Aux).AsQueryable();

            return query;
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registros Existentes En La Base De Datos
        public static async Task<List<User>> SearchAsync(User user)
        {
            var users = new List<User>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.User.AsQueryable();
                select = QuerySelect(select, user);
                users = await select.ToListAsync();
            }
            return users;
        }
        #endregion

        #region METODO PARA INCLUIR LLAVES FORANEAS A LA BUSQUEDA
        // Metodo Para Incluir El Rol En La Busqueda
        public static async Task<List<User>> SearchIncludeRoleAsync(User user)
        {
            var users = new List<User>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.User.AsQueryable();
                select = QuerySelect(select, user).Include(u => u.Role).AsQueryable();
                users = await select.ToListAsync();
            }
            return users;
        }
        #endregion

        #region METODO PARA INICIAR SESION (LOGIARSE)
        // Metodo Para Autenticar El Usuario
        public static async Task<User> LoginAsync(User user)
        {
            var userDb = new User();
            using (var dbContext = new ContextDB())
            {
                EncryptMD5(user);
                userDb = await dbContext.User.FirstOrDefaultAsync(
                    u => u.Email == user.Email && u.Password == user.Password
                    && u.Status == (byte)User_Status.ACTIVO);
            }
            return userDb!;
        }
        #endregion

        #region METODO PARA CAMBIAR LA CONTRASEÑA
        // Metodo Para Cambiar o Actualizar La Contraseña Del Usuario
        public static async Task<int> ChangePasswordAsync(User user, string oldPassword)
        {
            int result = 0;
            var userOldPass = new User { Password = oldPassword };
            EncryptMD5(userOldPass);
            using (var dbContext = new ContextDB())
            {
                var userDb = await dbContext.User.FirstOrDefaultAsync(u => u.Id == user.Id);
                if (userOldPass.Password == userDb!.Password)
                {
                    EncryptMD5(user);
                    userDb.Password = user.Password;
                    dbContext.User.Update(userDb);
                    result = await dbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("La Contraseña Es Invalida.");
            }
            return result;
        }
        #endregion
    }
}