namespace API.DTOs.Product
{
    public class ProductDTO
    {
        public record DisplayProductDTO
        (
            int Id,
            string Name,
            string Description,
            decimal NewPrice,
            decimal OldPrice,

            int CategoryId,
            List<PhotoDTO> Photos,
            string CategoryName

        );
        public record PhotoDTO
        (
            string ImageName,
            int ProductId
        );
        public record CreateProductDTO
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal NewPrice { get; set; }
            public decimal OldPrice {  get; set; }
            public int CategoryId {  get; set; }
            public List<IFormFile> photos { get; set; }
        }
        public record UpdateProductDTO : CreateProductDTO
        {
            public int Id { get; set; }
        }
            
        
    }
}
