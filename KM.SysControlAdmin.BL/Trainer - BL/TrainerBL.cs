#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.DAL.Trainer___DAL;
using KM.SysControlAdmin.EN.Trainer___EN;

#endregion

namespace KM.SysControlAdmin.BL.Trainer___BL
{
    public class TrainerBL
    {
        #region METODO PARA CREAR
        // Metodo Para Guardar Un Nuevo Registro
        public async Task<int> CreateAsync(Trainer trainer)
        {
            return await TrainerDAL.CreateAsync(trainer);
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Guardar Un Nuevo Registro
        public async Task<int> UpdateAsync(Trainer trainer)
        {
            return await TrainerDAL.UpdateAsync(trainer);
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public async Task<int> DeleteAsync(Trainer trainer)
        {
            return await TrainerDAL.DeleteAsync(trainer);
        }
        #endregion

        #region METODO PARA MOSTRAR
        // Metodo Para Mostrar Una Lista De Registros
        public async Task<List<Trainer>> GetAllAsync()
        {
            return await TrainerDAL.GetAllAsync();
        }
        #endregion

        #region METODO PARA MOSTRAR POR ID
        // Metodo Para Mostrar Un Registro Especifico Bajo Un Id
        public async Task<Trainer> GetByIdAsync(Trainer trainer)
        {
            return await TrainerDAL.GetByIdAsync(trainer);
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registros Existentes
        public async Task<List<Trainer>> SearchAsync(Trainer trainer)
        {
            return await TrainerDAL.SearchAsync(trainer);
        }
        #endregion
    }
}
