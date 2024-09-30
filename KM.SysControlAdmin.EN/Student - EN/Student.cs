#region REFERENIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using System.ComponentModel.DataAnnotations;


#endregion

namespace KM.SysControlAdmin.EN.Student___EN
{
    public class Student
    {
        #region Atributos de la Entidad
        [Key]
        public int Id { get; set; }

        [StringLength(6, ErrorMessage = "Maximo 6 caracteres")]
        [Display(Name = "Codigo de Estudiante")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ0123456789 ]+$", ErrorMessage = "Debe contener solo Letras y Numeros")]
        public string StudentCode { get; set; } = string.Empty;

        [StringLength(6, ErrorMessage = "Maximo 6 caracteres")]
        [Display(Name = "Codigo del Proyecto")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ0123456789 ]+$", ErrorMessage = "Debe contener solo Letras y Numeros")]
        public string ProjectCode { get; set; } = string.Empty;

        [StringLength(11, ErrorMessage = "Maximo 11 caracteres")]
        [Display(Name = "Codigo del Participante")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ0123456789 ]+$", ErrorMessage = "Debe contener solo Letras y Numeros")]
        public string ParticipantCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Nombre Es Requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [Display(Name = "Nombres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Debe contener solo Letras")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Apellido Es Requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [Display(Name = "Apellidos")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Debe contener solo Letras")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Fecha De Nacimiento Es Requerida")]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date, ErrorMessage = "Por favor, introduce una fecha válida")]
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        [Required(ErrorMessage = "La Edad Es Requerida")]
        [StringLength(3, ErrorMessage = "Maximo 3 caracteres")]
        [Display(Name = "Edad")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "La edad debe contener solo números")]
        public string Age { get; set; } = string.Empty;

        [StringLength(60, ErrorMessage = "Maximo 60 caracteres")]
        [Display(Name = "Nombre De La Iglesia")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Debe contener solo Letras")]
        public string Church { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Estatus Es Requerido")]
        [Display(Name = "Estatus")]
        public byte Status { get; set; }

        [Display(Name = "Fotografia")]
        public byte[]? ImageData { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        public DateTime DateModification { get; set; }
        #endregion
    }
}
