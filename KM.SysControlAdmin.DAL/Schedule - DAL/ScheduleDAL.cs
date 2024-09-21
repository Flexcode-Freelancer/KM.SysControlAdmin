#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.EN.Schedule___EN;
using Microsoft.EntityFrameworkCore;

#endregion

namespace KM.SysControlAdmin.DAL.Schedule___DAL
{
    public class ScheduleDAL
    {
        #region METODO PARA VALIDAR UNICA EXISTENCIA DEL REGISTRO
        // Metodo Para Validar La Unica Existencia De Un Registro y No Haber Duplicidad
        private static async Task<bool> ExistSchedule(Schedule schedule, ContextDB dbContext)
        {
            bool result = false;
            var schedules = await dbContext.Schedule.FirstOrDefaultAsync(r => r.StartTime == schedule.StartTime && r.EndTime == schedule.EndTime && r.Id != schedule.Id);
            if (schedules != null && schedules.Id > 0 && schedules.StartTime == schedule.StartTime && schedules.EndTime == schedule.EndTime)
                result = true;
            return result;
        }
        #endregion

        #region METODO PARA GUARDAR
        // Metodo Para Guardar Un Nuevo Registro En La Base De Datos
        public static async Task<int> CreateAsync(Schedule schedule)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                bool scheduleExists = await ExistSchedule(schedule, dbContext);
                if (scheduleExists == false)
                {
                    dbContext.Schedule.Add(schedule);
                    result = await dbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("Horario Ya Existente, Vuelve a Intentarlo");
            }
            return result;
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente En La Base De Datos
        public static async Task<int> UpdateAsync(Schedule schedule)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var scheduleDb = await dbContext.Schedule.FirstOrDefaultAsync(r => r.Id == schedule.Id);
                if (scheduleDb != null)
                {
                    bool scheduleExists = await ExistSchedule(schedule, dbContext);
                    if (scheduleExists == false)
                    {
                        scheduleDb.StartTime = schedule.StartTime;
                        scheduleDb.EndTime = schedule.EndTime;
                        dbContext.Schedule.Update(scheduleDb);
                        result = await dbContext.SaveChangesAsync();
                    }
                    else
                        throw new Exception("Horario Ya Existente, Vuelve a Intentarlo");
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public static async Task<int> DeleteAsync(Schedule schedule)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var scheduleDb = await dbContext.Schedule.FirstOrDefaultAsync(r => r.Id == schedule.Id);
                if (scheduleDb != null)
                {
                    dbContext.Schedule.Remove(scheduleDb);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA MOSTRAR TODOS
        // Metodo Para Listar y Mostrar Todos Los Resultados
        public static async Task<List<Schedule>> GetAllAsync()
        {
            var schedules = new List<Schedule>();
            using (var dbContext = new ContextDB())
            {
                schedules = await dbContext.Schedule.ToListAsync();
            }
            return schedules;
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Obtener Un Registro Por Su Id
        public static async Task<Schedule> GetByIdAsync(Schedule schedule)
        {
            var scheduleDb = new Schedule();
            using (var dbContext = new ContextDB())
            {
                scheduleDb = await dbContext.Schedule.FirstOrDefaultAsync(r => r.Id == schedule.Id);
            }
            return scheduleDb!;
        }
        #endregion

        #region METODO PARA FILTRAR BUSQUEDA
        // Metodo Para Filtrar La Busqueda Por Parametros
        internal static IQueryable<Schedule> QuerySelect(IQueryable<Schedule> query, Schedule schedule)
        {
            if (schedule.Id > 0)
                query = query.Where(r => r.Id == schedule.Id);

            if (schedule.StartTime != default)
                query = query.Where(r => r.StartTime == schedule.StartTime);

            if (schedule.EndTime != default)
                query = query.Where(r => r.EndTime == schedule.EndTime);

            query = query.OrderByDescending(r => r.Id);

            return query;
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registros Existentes En La Base De Datos
        public static async Task<List<Schedule>> SearchAsync(Schedule schedule)
        {
            var schedules = new List<Schedule>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Schedule.AsQueryable();
                select = QuerySelect(select, schedule);
                schedules = await select.ToListAsync();
            }
            return schedules;
        }
        #endregion
    }
}
