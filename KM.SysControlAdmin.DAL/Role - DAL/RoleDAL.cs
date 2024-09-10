#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.EN.Role___EN;
using Microsoft.EntityFrameworkCore;

#endregion

namespace KM.SysControlAdmin.DAL.Rol___DAL
{
    public class RoleDAL
    {
        #region METODO PARA GUARDAR
        // Metodo Para Guardar Un Nuevo Registro En La Base De Datos
        public static async Task<int> CreateAsync(Role role)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                bool rolExists = await ExistRol(role, dbContext);
                if (rolExists == false)
                {
                    dbContext.Role.Add(role);
                    result = await dbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("Rol Ya Existente, Vuelve a Intentarlo");
            }
            return result;
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente En La Base De Datos
        public static async Task<int> UpdateAsync(Role role)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var roleDb = await dbContext.Role.FirstOrDefaultAsync(r => r.Id == role.Id);
                if (roleDb != null)
                {
                    bool rolExists = await ExistRol(role, dbContext);
                    if (rolExists == false)
                    {
                        roleDb.Name = role.Name;
                        dbContext.Role.Update(roleDb);
                        result = await dbContext.SaveChangesAsync();
                    }
                    else
                        throw new Exception("Rol Ya Existente, Vuelve a Intentarlo");
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public static async Task<int> DeleteAsync(Role role)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var roleDb = await dbContext.Role.FirstOrDefaultAsync(r => r.Id == role.Id);
                if (roleDb != null)
                {
                    dbContext.Role.Remove(roleDb);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA MOSTRAR TODOS
        // Metodo Para Listar y Mostrar Todos Los Resultados
        public static async Task<List<Role>> GetAllAsync()
        {
            var roles = new List<Role>();
            using (var dbContext = new ContextDB())
            {
                roles = await dbContext.Role.ToListAsync();
            }
            return roles;
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Obtener Un Registro Por Su Id
        public static async Task<Role> GetByIdAsync(Role role)
        {
            var roleDb = new Role();
            using (var dbContext = new ContextDB())
            {
                roleDb = await dbContext.Role.FirstOrDefaultAsync(r => r.Id == role.Id);
            }
            return roleDb!;
        }
        #endregion

        #region METODO PARA FILTRAR BUSQUEDA
        // Metodo Para Filtrar La Busqueda Por Parametros
        internal static IQueryable<Role> QuerySelect(IQueryable<Role> query, Role role)
        {
            if (role.Id > 0)
                query = query.Where(r => r.Id == role.Id);

            if (!string.IsNullOrWhiteSpace(role.Name))
                query = query.Where(r => r.Name.Contains(role.Name));

            query = query.OrderByDescending(r => r.Id);

            return query;
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registros Existentes En La Base De Datos
        public static async Task<List<Role>> SearchAsync(Role role)
        {
            var roles = new List<Role>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Role.AsQueryable();
                select = QuerySelect(select, role);
                roles = await select.ToListAsync();
            }
            return roles;
        }
        #endregion

        #region METODO PARA VALIDAR UNICA EXISTENCIA DEL REGISTRO
        // Metodo Para Validar La Unica Existencia De Un Registro y No Haber Duplicidad
        private static async Task<bool> ExistRol(Role role, ContextDB dbContext)
        {
            bool result = false;
            var roles = await dbContext.Role.FirstOrDefaultAsync(r => r.Name == role.Name && r.Id != role.Id);
            if (roles != null && roles.Id > 0 && roles.Name == role.Name)
                result = true;
            return result;
        }
        #endregion
    }
}
