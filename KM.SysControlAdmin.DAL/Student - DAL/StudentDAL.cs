#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.EN.Student___EN;
using Microsoft.EntityFrameworkCore;

#endregion

namespace KM.SysControlAdmin.DAL.Student___DAL
{
    public class StudentDAL
    {
        #region METODO PARA VALIDAR UNICA EXISTENCIA DEL REGISTRO
        // Metodo Para Validar La Unica Existencia De Un Registro y No Haber Duplicidad
        private static async Task<bool> ExistStudent(Student student, ContextDB contextDB)
        {
            bool result = false;
            var students = await contextDB.Student.FirstOrDefaultAsync(m => m.Name == student.Name && m.LastName == student.LastName &&
                                                                       m.DateOfBirth == student.DateOfBirth && m.Id != student.Id);
            if (students != null && students.Id > 0 && students.Name == student.Name &&
                                                       students.LastName == student.LastName &&
                                                       students.DateOfBirth == student.DateOfBirth)
                result = true;
            return result;
        }
        #endregion

        #region METODO PARA CREAR
        // Metodo Para Guardar Un Nuevo Registro En La Base De Datos
        public static async Task<int> CreateAsync(Student student)
        {
            int result = 0;
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                bool studentExists = await ExistStudent(student, dbContext);
                if (studentExists == false)
                {
                    dbContext.Student.Add(student);
                    result = await dbContext.SaveChangesAsync(); // Await sirve para esperar a terminar todos los procesos para devolverlos todos juntos
                }
                else
                    throw new Exception("Alumno/a Ya Existente, Vuelve a Intentarlo Nuevamente.");
            }
            return result;  // Si se realizo con exito devuelve 1 sino devuelve 0
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente De La Base De Datos
        public static async Task<int> UpdateAsync(Student student)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var studentDB = await dbContext.Student.FirstOrDefaultAsync(m => m.Id == student.Id);
                if (studentDB != null)
                {
                    bool studentExists = await ExistStudent(student, dbContext);
                    if (studentExists == false)
                    {
                        studentDB.StudentCode = student.StudentCode;
                        studentDB.ProjectCode = student.ProjectCode;
                        studentDB.ParticipantCode = student.ParticipantCode;
                        studentDB.Name = student.Name;
                        studentDB.LastName = student.LastName;
                        studentDB.DateOfBirth = student.DateOfBirth;
                        studentDB.Age = student.Age;
                        studentDB.Church = student.Church;
                        studentDB.Status = student.Status;
                        studentDB.ImageData = student.ImageData;
                        studentDB.DateCreated = student.DateCreated;
                        studentDB.DateModification = student.DateModification;

                        dbContext.Update(studentDB);
                        result = await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Alumno/a Ya Existente, Vuelve a Intentarlo Nuevamente.");
                    }
                }
                else
                {
                    throw new Exception("Alumno/a No Encontrado Para Actualizar.");
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public static async Task<int> DeleteAsync(Student student)
        {
            int result = 0;
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                var studentDB = await dbContext.Student.FirstOrDefaultAsync(m => m.Id == student.Id);
                if (studentDB != null)
                {
                    dbContext.Student.Remove(studentDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;  // Si se realizo con exito devuelve 1 sino devuelve 0
        }
        #endregion

        #region METODO PARA MOSTRAR
        // Metodo Para Mostrar La Lista De Registros
        public static async Task<List<Student>> GetAllAsync()
        {
            var students = new List<Student>();
            using (var dbContext = new ContextDB())
            {
                students = await dbContext.Student.ToListAsync();
            }
            return students;
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Mostrar Un Registro En Base A Su Id
        public static async Task<Student> GetByIdAsync(Student student)
        {
            var studentDB = new Student();
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                studentDB = await dbContext.Student.FirstOrDefaultAsync(m => m.Id == student.Id);
            }
            return studentDB!; // Retornamos el Registro Encontrado
        }
        #endregion

        #region METODO PARA BUSCAR REGISTROS MEDIANTE EL USO DE FILTROS
        // Metodo Para Buscar Por Filtros
        // IQueryable es una interfaz que toma un coleccion a la cual se le pueden implementar multiples consultas (Filtros)
        internal static IQueryable<Student> QuerySelect(IQueryable<Student> query, Student student)
        {
            // Por ID
            if (student.Id > 0)
                query = query.Where(m => m.Id == student.Id);

            if (!string.IsNullOrWhiteSpace(student.ParticipantCode))
                query = query.Where(m => m.ParticipantCode.Contains(student.ParticipantCode));

            if (!string.IsNullOrWhiteSpace(student.StudentCode))
                query = query.Where(m => m.StudentCode.Contains(student.StudentCode));

            if (!string.IsNullOrWhiteSpace(student.Name))
                query = query.Where(m => m.Name.Contains(student.Name));

            if (!string.IsNullOrWhiteSpace(student.LastName))
                query = query.Where(m => m.LastName.Contains(student.LastName));

            // Ordenamos de Manera Desendente
            query = query.OrderByDescending(c => c.Id).AsQueryable();

            return query;
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo para Buscar Registros Existentes
        public static async Task<List<Student>> SearchAsync(Student student)
        {
            var students = new List<Student>();
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Student.AsQueryable();
                select = QuerySelect(select, student);
                students = await select.ToListAsync();
            }
            return students;
        }
        #endregion
    }
}