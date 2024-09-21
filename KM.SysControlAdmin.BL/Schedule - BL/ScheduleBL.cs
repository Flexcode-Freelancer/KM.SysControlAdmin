#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento}
using KM.SysControlAdmin.DAL.Schedule___DAL;
using KM.SysControlAdmin.EN.Schedule___EN;


#endregion

namespace KM.SysControlAdmin.BL.Schedule___BL
{
    public class ScheduleBL
    {
        #region METODO PARA GUARDAR
        // Metodo Para Guardar Un Nuevo Registro a La Base De Datos
        public async Task<int> CreateAsync(Schedule schedule)
        {
            return await ScheduleDAL.CreateAsync(schedule);
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente En La Base De Datos
        public async Task<int> UpdateAsync(Schedule schedule)
        {
            return await ScheduleDAL.UpdateAsync(schedule);
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public async Task<int> DeleteAsync(Schedule schedule)
        {
            return await ScheduleDAL.DeleteAsync(schedule);
        }
        #endregion

        #region METODO PARA MOSTRAR TODOS
        // Metodo Para Listar y Mostrar Todos Los Resultados
        public async Task<List<Schedule>> GetAllAsync()
        {
            return await ScheduleDAL.GetAllAsync();
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Obtener Un Registro Por Su Id
        public async Task<Schedule> GetByIdAsync(Schedule schedule)
        {
            return await ScheduleDAL.GetByIdAsync(schedule);
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registro Existentes En La Base De Datos
        public async Task<List<Schedule>> SearchAsync(Schedule schedule)
        {
            return await ScheduleDAL.SearchAsync(schedule);
        }
        #endregion
    }
}
