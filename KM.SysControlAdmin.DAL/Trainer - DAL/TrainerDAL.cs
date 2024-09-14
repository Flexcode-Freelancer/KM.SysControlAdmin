#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.EN.Trainer___EN;
using Microsoft.EntityFrameworkCore;


#endregion

namespace KM.SysControlAdmin.DAL.Trainer___DAL
{
    public class TrainerDAL
    {
        #region METODO PARA VALIDAR UNICA EXISTENCIA DEL REGISTRO
        // Metodo Para Validar La Unica Existencia De Un Registro y No Haber Duplicidad
        private static async Task<bool> ExistTrainer(Trainer trainer, ContextDB contextDB)
        {
            bool result = false;
            var trainers = await contextDB.Trainer.FirstOrDefaultAsync(m => m.Dui == trainer.Dui && m.Code == trainer.Code && m.Id != trainer.Id);
            if (trainers != null && trainers.Id > 0 && trainers.Dui == trainer.Dui && trainers.Code == trainer.Code)
                result = true;
            return result;
        }
        #endregion

        #region METODO PARA CREAR
        // Metodo Para Guardar Un Nuevo Registro En La Base De Datos
        public static async Task<int> CreateAsync(Trainer trainer)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                bool trainerExists = await ExistTrainer(trainer, dbContext);
                if (trainerExists == false)
                {
                    dbContext.Trainer.Add(trainer);
                    result = await dbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("Instructor/Docente Ya Existente, Vuelve a Intentarlo Nuevamente.");
            }
            return result;  // Si se realizo con exito devuelve 1 sino devuelve 0
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente De La Base De Datos
        public static async Task<int> UpdateAsync(Trainer trainer)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var trainerDB = await dbContext.Trainer.FirstOrDefaultAsync(m => m.Id == trainer.Id);
                if (trainerDB != null)
                {
                    bool trainerExists = await ExistTrainer(trainer, dbContext);
                    if (trainerExists == false)
                    {
                        trainerDB.Code = trainer.Code;
                        trainerDB.Name = trainer.Name;
                        trainerDB.LastName = trainer.LastName;
                        trainerDB.Dui = trainer.Dui;
                        trainerDB.DateOfBirth = trainer.DateOfBirth;
                        trainerDB.Age = trainer.Age;
                        trainerDB.Gender = trainer.Gender;
                        trainerDB.CivilStatus = trainer.CivilStatus;
                        trainerDB.Phone = trainer.Phone;
                        trainerDB.Address = trainer.Address;
                        trainerDB.Status = trainer.Status;
                        trainerDB.CommentsOrObservations = trainer.CommentsOrObservations;
                        trainerDB.DateCreated = trainer.DateCreated;
                        trainerDB.DateModification = trainer.DateModification;
                        trainerDB.ImageData = trainer.ImageData;

                        dbContext.Update(trainerDB);
                        result = await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Instructutor/Docente Ya Existente, Vuelve a Intentarlo Nuevamente.");
                    }
                }
                else
                {
                    throw new Exception("Instructor/Docente No Encontrado Para Actualizar.");
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public static async Task<int> DeleteAsync(Trainer trainer)
        {
            int result = 0;
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                var trainerDB = await dbContext.Trainer.FirstOrDefaultAsync(m => m.Id == trainer.Id);
                if (trainerDB != null)
                {
                    dbContext.Trainer.Remove(trainerDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;  // Si se realizo con exito devuelve 1 sino devuelve 0
        }
        #endregion

        #region METODO PARA MOSTRAR
        // Metodo Para Mostrar La Lista De Registros
        public static async Task<List<Trainer>> GetAllAsync()
        {
            var trainer = new List<Trainer>();
            using (var dbContext = new ContextDB())
            {
                trainer = await dbContext.Trainer.ToListAsync();
            }
            return trainer;
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Mostrar Un Registro En Base A Su Id
        public static async Task<Trainer> GetByIdAsync(Trainer trainer)
        {
            var trainerDB = new Trainer();
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                trainerDB = await dbContext.Trainer.FirstOrDefaultAsync(m => m.Id == trainer.Id);
            }
            return trainerDB!; // Retornamos el Registro Encontrado
        }
        #endregion

        #region METODO PARA BUSCAR REGISTROS MEDIANTE EL USO DE FILTROS
        // Metodo Para Buscar Por Filtros
        // IQueryable es una interfaz que toma un coleccion a la cual se le pueden implementar multiples consultas (Filtros)
        internal static IQueryable<Trainer> QuerySelect(IQueryable<Trainer> query, Trainer trainer)
        {
            // Por ID
            if (trainer.Id > 0)
                query = query.Where(m => m.Id == trainer.Id);

            if (!string.IsNullOrWhiteSpace(trainer.Name))
                query = query.Where(m => m.Name.Contains(trainer.Name));

            if (!string.IsNullOrWhiteSpace(trainer.LastName))
                query = query.Where(m => m.LastName.Contains(trainer.LastName));

            if (!string.IsNullOrWhiteSpace(trainer.Dui))
                query = query.Where(m => m.Dui.Contains(trainer.Dui));

            if (!string.IsNullOrWhiteSpace(trainer.Code))
                query = query.Where(m => m.Code.Contains(trainer.Code));

            // Ordenamos de Manera Desendente
            query = query.OrderByDescending(c => c.Id).AsQueryable();

            return query;
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo para Buscar Registros Existentes
        public static async Task<List<Trainer>> SearchAsync(Trainer trainer)
        {
            var trainers = new List<Trainer>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Trainer.AsQueryable();
                select = QuerySelect(select, trainer);
                trainers = await select.ToListAsync();
            }
            return trainers;
        }
        #endregion
    }
}
