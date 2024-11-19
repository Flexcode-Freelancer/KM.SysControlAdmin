#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.DAL.Course___DAL;
using KM.SysControlAdmin.EN.Course___EN;


#endregion

namespace KM.SysControlAdmin.BL.Course__BL
{
    public class CourseBL
    {
        #region METODO PARA CREAR
        // Metodo Para Guardar Un Nuevo Registro
        public async Task<int> CreateAsync(Course course)
        {
            return await CourseDAL.CreateAsync(course);
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Guardar Un Nuevo Registro
        public async Task<int> UpdateAsync(Course course)
        {
            return await CourseDAL.UpdateAsync(course);
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public async Task<int> DeleteAsync(Course course)
        {
            return await CourseDAL.DeleteAsync(course);
        }
        #endregion

        #region METODO PARA MOSTRAR
        // Metodo Para Mostrar Una Lista De Registros
        public async Task<List<Course>> GetAllAsync()
        {
            return await CourseDAL.GetAllAsync();
        }
        #endregion

        #region METODO PARA MOSTRAR POR ID
        // Metodo Para Mostrar Un Registro Especifico Bajo Un Id
        public async Task<Course> GetByIdAsync(Course course)
        {
            return await CourseDAL.GetByIdAsync(course);
        }

        // Metodo para que admita int al hacer uso del metodo antecesor para automatizacion.
        public async Task<Course> GetByIdAsync(int id)
        {
            // Crear una instancia de Membership y asignarle el ID
            var course = new Course { Id = id };

            // Llamar al método existente con el objeto Membership
            return await CourseDAL.GetByIdAsync(course);
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registros Existentes
        public async Task<List<Course>> SearchAsync(Course course)
        {
            return await CourseDAL.SearchAsync(course);
        }
        #endregion

        #region METODO PARA INCLUIR HORARIO E INSTRUCTOR
        public async Task<List<Course>> SearchIncludeScheduleAndTrainerAsync(Course course)
        {
            return await CourseDAL.SearchIncludeScheduleAndTrainerAsync(course);
        }
        #endregion


        #region MÉTODO PARA MOSTRAR CURSOS ASIGNADOS A UN INSTRUCTOR
        // Método para mostrar una lista de cursos asignados al instructor autenticado
        public async Task<List<Course>> GetAllByTrainerAsync(ClaimsPrincipal user)
        {
            return await CourseDAL.GetAllByTrainerAsync(user); // Delegación directa al método DAL
        }
        #endregion

        #region MÉTODO PARA BUSCAR CURSOS INCLUYENDO HORARIO E INSTRUCTOR ASIGNADOS A UN INSTRUCTOR
        // Método para buscar cursos asignados al instructor autenticado, incluyendo horario e instructor
        public async Task<List<Course>> SearchIncludeScheduleAndTrainerByTrainerAsync(ClaimsPrincipal user, Course course)
        {
            return await CourseDAL.SearchIncludeScheduleAndTrainerByTrainerAsync(user, course); // Delegación directa al método DAL
        }
        #endregion
    }
}
