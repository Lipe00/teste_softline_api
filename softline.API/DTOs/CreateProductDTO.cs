using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softline.API.DTOs
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "É obrigatório informar o código do produto!")]
        public int Code { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a descrição do produto!")]
        [MaxLength(60)]
        public string Description { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o código de barras do produto!")]
        [MaxLength(14)]
        public string BarCode { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o preço de venda do produto!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o peso do produto!")]
        public decimal GrossWeight { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o peso do produto!")]
        public decimal NetWeight { get; set; }
    }
}
