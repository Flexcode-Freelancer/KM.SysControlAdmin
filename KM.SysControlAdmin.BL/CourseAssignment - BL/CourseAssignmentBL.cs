#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.DAL.CourseAssignment___DAL;
using KM.SysControlAdmin.EN.CourseAssignament___EN;

#endregion

namespace KM.SysControlAdmin.BL.CourseAssignment___DAL
{
    public class CourseAssignmentBL
    {
        #region METODO PARA CREAR
        // Metodo Para Guardar Un Nuevo Registro
        public async Task<int> CreateAsync(CourseAssignment courseAssignment)
        {
            return await CourseAssignmentDAL.CreateAsync(courseAssignment);
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Guardar Un Nuevo Registro
        public async Task<int> UpdateAsync(CourseAssignment courseAssignment)
        {
            return await CourseAssignmentDAL.UpdateAsync(courseAssignment);
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public async Task<int> DeleteAsync(CourseAssignment courseAssignment)
        {
            return await CourseAssignmentDAL.DeleteAsync(courseAssignment);
        }
        #endregion

        #region METODO PARA MOSTRAR
        // Metodo Para Mostrar Una Lista De Registros
        public async Task<List<CourseAssignment>> GetAllAsync()
        {
            return await CourseAssignmentDAL.GetAllAsync();
        }
        #endregion

        #region METODO PARA MOSTRAR POR ID
        // Metodo Para Mostrar Un Registro Especifico Bajo Un Id
        public async Task<CourseAssignment> GetByIdAsync(CourseAssignment courseAssignment)
        {
            return await CourseAssignmentDAL.GetByIdAsync(courseAssignment);
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registros Existentes
        public async Task<List<CourseAssignment>> SearchAsync(CourseAssignment courseAssignment)
        {
            return await CourseAssignmentDAL.SearchAsync(courseAssignment);
        }
        #endregion

        #region METODO PARA INCLUIR PRIVILEGIO Y PRIVILEGIO
        public async Task<List<CourseAssignment>> SearchIncludeAsync(CourseAssignment courseAssignment)
        {
            return await CourseAssignmentDAL.SearchIncludeAsync(courseAssignment);
        }
        #endregion
    }
}
