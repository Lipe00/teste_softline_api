using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace softline.API.DTOs
{
    public class CreateUserDTO
    {
        [Column("user_name")]
        [MaxLength(20, ErrorMessage = "O nome de usuário deve conter no máximo 20 caracteres!")]
        [Required(ErrorMessage = "É obrigatório informar o nome do usuário!!")]
        public string Name { get; set; }


        [Column("user_password")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres!")]
        [Required(ErrorMessage = "É obrigatório informar uma senha para o usuário!!")]
        public string Password { get; set; }
    }
}
