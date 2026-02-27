using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.entidades
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int Id { get; set; }


        [Column("product_code")]
        [Required(ErrorMessage = "É obrigatório informar o código do produto!")]
        public int Code { get; set; }


        [Column("product_description")]
        [Required(ErrorMessage = "É obrigatório informar a descrição do produto!")]
        [MaxLength(60, ErrorMessage = "A descrição do produto deve conter no máximo 60 caracteres!")]
        public string Description { get; set; }


        [Column("product_barCode")]
        [Required(ErrorMessage = "É obrigatório informar o código de barras do produto!")]
        [MaxLength(14, ErrorMessage = "O código de barras do produto deve conter no máximo 14 caracteres!")]
        public string BarCode { get; set; }


        [Column("product_price", TypeName="decimal(6,2)")]
        [Required(ErrorMessage = "É obrigatório informar o preço de venda do produto!")]
        public decimal Price { get; set; }

        [Column("product_grossWeight", TypeName = "decimal(6,3)")]
        [Required(ErrorMessage = "É obrigatório informar o peso do produto!")]
        public decimal GrossWeight { get; set; }

        [Column("product_netWeight", TypeName = "decimal(6,3)")]
        [Required(ErrorMessage = "É obrigatório informar o peso do produto!")]
        public decimal NetWeight { get; set; }

    }
}
