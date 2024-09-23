#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KM.SysControlAdmin.EN.Course___EN;
using KM.SysControlAdmin.EN.Role___EN;
using KM.SysControlAdmin.EN.Schedule___EN;
using KM.SysControlAdmin.EN.Trainer___EN;
using KM.SysControlAdmin.EN.User___EN;


// Referencias Necesarias Para El Correcto Funcionamiento
using Microsoft.EntityFrameworkCore;



#endregion

namespace KM.SysControlAdmin.DAL
{
    public class ContextDB : DbContext
    {
        #region REFERENCIAS DE TABLAS DE LA BD
        //Coleccion que hace referencia a las tablas de la base de datos
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Course> Course { get; set; }
        #endregion

        #region STRING DE CONEXION
        // Metodo de Conexion a la Base de Datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=KMSysControlAdminDB;Integrated Security=True;Trust Server Certificate=True"); // String de Conexion
        }
        #endregion
    }
}
