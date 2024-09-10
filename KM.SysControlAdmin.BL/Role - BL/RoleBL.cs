#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.DAL.Rol___DAL;
using KM.SysControlAdmin.EN.Role___EN;

#endregion

namespace KM.SysControlAdmin.BL.Rol___BL
{
    public class RoleBL
    {
        #region METODO PARA GUARDAR
        // Metodo Para Guardar Un Nuevo Registro a La Base De Datos
        public async Task<int> CreateAsync(Role role)
        {
            return await RoleDAL.CreateAsync(role);
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente En La Base De Datos
        public async Task<int> UpdateAsync(Role role)
        {
            return await RoleDAL.UpdateAsync(role);
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public async Task<int> DeleteAsync(Role role)
        {
            return await RoleDAL.DeleteAsync(role);
        }
        #endregion

        #region METODO PARA MOSTRAR TODOS
        // Metodo Para Listar y Mostrar Todos Los Resultados
        public async Task<List<Role>> GetAllAsync()
        {
            return await RoleDAL.GetAllAsync();
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Obtener Un Registro Por Su Id
        public async Task<Role> GetByIdAsync(Role role)
        {
            return await RoleDAL.GetByIdAsync(role);
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registro Existentes En La Base De Datos
        public async Task<List<Role>> SearchAsync(Role role)
        {
            return await RoleDAL.SearchAsync(role);
        }
        #endregion
    }
}