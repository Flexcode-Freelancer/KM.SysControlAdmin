#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.EN.Course___EN;
using Microsoft.EntityFrameworkCore;


#endregion

namespace KM.SysControlAdmin.DAL.Course___DAL
{
    public class CourseDAL
    {
        #region METODO PARA VALIDAR UNICA EXISTENCIA DEL REGISTRO
        // Metodo Para Validar La Unica Existencia De Un Registro y No Haber Duplicidad
        private static async Task<bool> ExistCourse(Course course, ContextDB contextDB)
        {
            bool result = false;
            var courses = await contextDB.Course.FirstOrDefaultAsync(c => c.Code == course.Code && c.Id != course.Id);
            if (courses != null && courses.Id > 0 && courses.Code == course.Code)
                result = true;
            return result;
        }
        #endregion

        #region METODO PARA CREAR
        // Metodo Para Guardar Un Nuevo Registro En La Base De Datos
        public static async Task<int> CreateAsync(Course course)
        {
            int result = 0;
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                bool courseExists = await ExistCourse(course, dbContext);
                if (courseExists == false)
                {
                    dbContext.Course.Add(course);
                    result = await dbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("Curso Ya Existente, Vuelve a Intentarlo Nuevamente");
            }
            return result;
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente De La Base De Datos
        public static async Task<int> UpdateAsync(Course course)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var courseDB = await dbContext.Course.FirstOrDefaultAsync(c => c.Id == course.Id);
                if (courseDB != null)
                {
                    bool courseExists = await ExistCourse(course, dbContext);
                    if (courseExists == false)
                    {
                        courseDB.Code = course.Code;
                        courseDB.Name = course.Name;
                        courseDB.Price = course.Price;
                        courseDB.StartTime = course.StartTime;
                        courseDB.EndTime = course.EndTime;
                        courseDB.MaxStudent = course.MaxStudent;
                        courseDB.IdSchedule = course.IdSchedule;
                        courseDB.IdTrainer = course.IdTrainer;
                        courseDB.Status = course.Status;
                        courseDB.DateCreated = course.DateCreated;
                        courseDB.DateModification = course.DateModification;

                        dbContext.Update(courseDB);
                        result = await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Curso Ya Existente, Vuelve a Intentarlo Nuevamente");
                    }
                }
                else
                {
                    throw new Exception("Curso No Encontrado Para Actualizar.");
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public static async Task<int> DeleteAsync(Course course)
        {
            int result = 0;
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                var courseDB = await dbContext.Course.FirstOrDefaultAsync(c => c.Id == course.Id);
                if (courseDB != null)
                {
                    dbContext.Course.Remove(courseDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA MOSTRAR
        // Metodo Para Mostrar La Lista De Registros
        public static async Task<List<Course>> GetAllAsync()
        {
            var courses = new List<Course>();
            using (var dbContext = new ContextDB())
            {
                courses = await dbContext.Course.ToListAsync();
            }
            return courses;
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Mostrar Un Registro En Base A Su Id
        public static async Task<Course> GetByIdAsync(Course course)
        {
            var courseDB = new Course();

            // Un bloque de conexión que mientras se permanezca en el bloque, la base de datos permanecerá abierta y al terminar se destruirá
            using (var dbContext = new ContextDB())
            {
                courseDB = await dbContext.Course
                    .Include(c => c.Schedule).Include(c => c.Trainer)
                    .FirstOrDefaultAsync(m => m.Id == course.Id);
            }

            return courseDB!; // Retornamos el registro encontrado
        }

        // Normalmente el metodo deberia ser asi como el siguiente pero, por modificaciones en
        // el proceso de servidores se a modificado pero se deja la forma normal para aprenderla.
        // *********** Metodo Para Mostrar Un Registro En Base A Su Id *************
        //public static async Task<Course> GetByIdAsync(Course course)
        //{
        //    var courseDB = new Course();
        //    using (var dbContext = new ContextDB())
        //    {
        //        courseDB = await dbContext.Course.FirstOrDefaultAsync(c => c.Id == course.Id);
        //    }
        //    return courseDB!;
        //}
        #endregion

        #region METODO PARA BUSCAR REGISTROS MEDIANTE EL USO DE FILTROS
        // Metodo Para Buscar Por Filtros
        // IQueryable es una interfaz que toma un coleccion a la cual se le pueden implementar multiples consultas (Filtros)
        internal static IQueryable<Course> QuerySelect(IQueryable<Course> query, Course course)
        {
            // Por ID
            if (course.Id > 0)
                query = query.Where(c => c.Id == course.Id);

            // Por Su Instructor
            if (course.IdTrainer > 0)
                query = query.Where(c => c.IdTrainer == course.IdTrainer);

            // Por Codigo, Si es verdadero lo vuelve falso y viceversa 
            if (!string.IsNullOrWhiteSpace(course.Code))
                query = query.Where(c => c.Code.Contains(course.Code));

            // Por Nomnbre, Si es verdadero lo vuelve falso y viceversa 
            if (!string.IsNullOrWhiteSpace(course.Name))
                query = query.Where(c => c.Name.Contains(course.Name));

            // Por Su Estatus
            if (course.Status > 0)
                query = query.Where(c => c.Status == course.Status);

            // Ordenamos de Manera Desendente
            query = query.OrderByDescending(c => c.Id).AsQueryable();

            return query;
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo para Buscar Registros Existentes
        public static async Task<List<Course>> SearchAsync(Course course)
        {
            var courses = new List<Course>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Course.AsQueryable();
                select = QuerySelect(select, course);
                courses = await select.ToListAsync();
            }
            return courses;
        }
        #endregion

        #region METODO PARA INCLUIR HORARIO E INSTRUCTOR
        // Método que incluye el Horario y el Instructor para la búsqueda
        public static async Task<List<Course>> SearchIncludeScheduleAndTrainerAsync(Course course)
        {
            var courses = new List<Course>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Course.AsQueryable();
                select = QuerySelect(select, course).Include(c => c.Schedule).Include(c => c.Trainer).AsQueryable();
                courses = await select.ToListAsync();
            }
            return courses;
        }
        #endregion





        #region LOGICA PARA MOSTRA LISTA DE CURSOS ASIGNADO AL DOCENTE
            #region MÉTODO PARA OBTENER EL ID DEL INSTRUCTOR DESDE LOS CLAIMS
            // Método que obtiene el ID del instructor desde los Claims
            private static int GetTrainerIdFromClaims(ClaimsPrincipal user)
            {
                var trainerIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(trainerIdClaim, out int trainerId))
                {
                    return trainerId;
                }

                throw new Exception("No se pudo encontrar al Instructor/Docente");
            }
            #endregion

            #region MÉTODO PARA MOSTRAR CURSOS ASIGNADOS A UN INSTRUCTOR
            // Método para traer la lista de cursos asignados al instructor autenticado
            public static async Task<List<Course>> GetAllByTrainerAsync(ClaimsPrincipal user)
            {
                using (var dbContext = new ContextDB())
                {
                    int trainerId = GetTrainerIdFromClaims(user); // Obtén el ID del instructor desde los claims

                    return await dbContext.Course
                        .Where(c => c.IdTrainer == trainerId) // Filtrar por el ID del instructor
                        .ToListAsync();
                }
            }
            #endregion

            #region MÉTODO PARA BUSCAR CURSOS INCLUYENDO HORARIO E INSTRUCTOR ASIGNADOS A UN INSTRUCTOR
            // Método para buscar cursos asignados al instructor autenticado, incluyendo horario e instructor
            public static async Task<List<Course>> SearchIncludeScheduleAndTrainerByTrainerAsync(ClaimsPrincipal user, Course course)
            {
                using (var dbContext = new ContextDB())
                {
                    int trainerId = GetTrainerIdFromClaims(user); // Obtén el ID del instructor desde los claims

                    var select = dbContext.Course.AsQueryable();

                    // Aplicar los filtros del curso recibido y el ID del instructor
                    select = QuerySelect(select, course)
                        .Where(c => c.IdTrainer == trainerId) // Filtrar por el instructor autenticado
                        .Include(c => c.Schedule)            // Incluir información del horario
                        .Include(c => c.Trainer);            // Incluir información del instructor

                    return await select.ToListAsync();
                }
            }
            #endregion
        #endregion

    }
}
