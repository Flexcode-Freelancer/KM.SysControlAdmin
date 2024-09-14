#region REFERENCIASA
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using System.ComponentModel.DataAnnotations;
using KM.SysControlAdmin.EN.Role___EN;
using System.ComponentModel.DataAnnotations.Schema;
using KM.SysControlAdmin.EN.User___EN;


#endregion

namespace KM.SysControlAdmin.EN.Trainer___EN
{
    public class Trainer
    {
        #region Atributos de la Entidad
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Codigo Es Requerido")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        [Display(Name = "Codigo")]
        public string Code { get; set; } = string.Empty;

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

        [Required(ErrorMessage = "El Dui Es Requerido")]
        [StringLength(10, ErrorMessage = "Maximo 10 caracteres")]
        [Display(Name = "Dui")]
        public string Dui { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Fecha De Nacimiento Es Requerida")]
        [Display(Name = "Fecha De Nacimiento")]
        [DataType(DataType.Date, ErrorMessage = "Por favor, introduce una fecha válida")]
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        [Required(ErrorMessage = "La Edad Es Requerida")]
        [StringLength(3, ErrorMessage = "Maximo 3 caracteres")]
        [Display(Name = "Edad")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "La edad debe contener solo números")]
        public string Age { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Genero Es Requerido")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        [Display(Name = "Genero")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Debe contener solo Letras")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Estado Civil Es Requerido")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        [Display(Name = "Estado Civil")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ/ ]+$", ErrorMessage = "Debe contener solo Letras")]
        public string CivilStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Telefono Es Requerido")]
        [StringLength(9, ErrorMessage = "Maximo 9 caracteres")]
        [Display(Name = "Telefono")]
        [RegularExpression("^[0-9-]+$", ErrorMessage = "El Telefono debe contener solo números")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Direccion Es Requerida")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [Display(Name = "Direccion De Residencia")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Estado Es Requerido")]
        [Display(Name = "Estado")]
        public byte Status { get; set; }

        [Required(ErrorMessage = "Los Comentarios u Observaciones Son Requeridas")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [Display(Name = "Comentarios u Observaciones")]
        public string CommentsOrObservations { get; set; } = string.Empty;

        [Display(Name = "Fecha De Creación")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Fecha De Modificación")]
        public DateTime DateModification { get; set; }

        [Display(Name = "Fotografia")]
        public byte[]? ImageData { get; set; }

        #endregion

        #region Atributos No Mapeables Externos
        [NotMapped]
        [Required(ErrorMessage = "El Correo Electronico es requerido")]
        [MaxLength(50, ErrorMessage = "Máximo 30 caracteres")]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; } = string.Empty;

        [NotMapped]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "La contraseña debe estar entre 6 y 32 caracteres", MinimumLength = 6)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [NotMapped]
        [ForeignKey("Role")]
        [Required(ErrorMessage = "El rol es requerido")]
        [Display(Name = "Rol")]
        public int IdRole { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "La confirmación de la contraseña es requerida")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [StringLength(32, ErrorMessage = "La contraseña debe tener entre 6 y 32 caracteres", MinimumLength = 6)]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword_Aux { get; set; } = string.Empty; //propiedad auxiliar

        [NotMapped]
        public User? User { get; set; } //propiedad de navegación
        #endregion

        //public List<Course> Course { get; set; } = new List<Course>(); // Propiedad de navegacion
    }
}
