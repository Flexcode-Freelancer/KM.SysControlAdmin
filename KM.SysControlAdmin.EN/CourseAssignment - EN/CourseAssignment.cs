#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using KM.SysControlAdmin.EN.Course___EN;
using KM.SysControlAdmin.EN.Student___EN;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#endregion

namespace KM.SysControlAdmin.EN.CourseAssignament___EN
{
    public class CourseAssignment
    {
        #region ATRIBUTOS DE LA ENTIDAD
        [Key]
        public int Id { get; set; }

        [ForeignKey("Student")]
        [Required(ErrorMessage = "El Alumno/a Es Requerido")]
        [Display(Name = "Alumno/a")]
        public int IdStudent { get; set; }

        [ForeignKey("Course")]
        [Required(ErrorMessage = "El Curso Es Requerido")]
        [Display(Name = "Curso")]
        public int IdCourse { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        public DateTime DateModification { get; set; }
        #endregion

        public Student? Student { get; set; } // Propiedadd de Navegacion

        public Course? Course { get; set; }// Propiedad de Navegacion
    }
}
