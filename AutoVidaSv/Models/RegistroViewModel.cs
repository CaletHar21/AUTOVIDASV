using System.ComponentModel.DataAnnotations;

namespace AutoVidaSv.Models
{
        public class RegistroViewModel
        {
            [Required(ErrorMessage = "El campo Nombres es requerido.")]
            public string Nombres { get; set; }

            [Required(ErrorMessage = "El campo Apellidos es requerido.")]
            public string Apellidos { get; set; }

            [Required(ErrorMessage = "El campo Correo electrónico es requerido.")]
            [EmailAddress(ErrorMessage = "El campo Correo electrónico no tiene un formato válido.")]
            public string Correo { get; set; }

            [Required(ErrorMessage = "El campo Contraseña es requerido.")]
            [DataType(DataType.Password)]
            public string Contrasena { get; set; }

            [Required(ErrorMessage = "Por favor, confirme su contraseña.")]
            [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
            [DataType(DataType.Password)]
            public string ConfirmarContrasena { get; set; }
        }
}
