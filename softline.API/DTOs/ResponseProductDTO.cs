namespace softline.API.DTOs
{
    public class ResponseProductDTO
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public decimal Price { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NetWeight { get; set; }
    }
}
