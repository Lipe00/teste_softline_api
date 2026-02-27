using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace softline.API.DTOs
{
    public class CreateClientDTO
    {
        [Column("client_name")]
        [Required(ErrorMessage = "É obrigatório informar o nome do cliente!")]
        public string Name { get; set; }


        [Column("client_fantasyName")]
        [Required(ErrorMessage = "É obrigatório informar o nome fantasia do cliente!")]
        public string Fantasy_name { get; set; }


        [Column("client_document")]
        [Required(ErrorMessage = "É obrigatório informar o cpf ou o cnpj do cliente!")]
        [StringLength(14, MinimumLength = 11,
            ErrorMessage = "Documento deve conter 11 (CPF) ou 14 (CNPJ) dígitos")]
        [RegularExpression(@"^(\d{11}|\d{14})$",
            ErrorMessage = "Documento deve conter apenas números e ter 11 ou 14 dígitos")]
        [MinLength(11)]
        [MaxLength(14)]
        public string Document { get; set; }


        [Column("client_address")]
        [Required(ErrorMessage = "É obrigatório informar o endereço do cliente!")]
        public string Address { get; set; }
    }
}
