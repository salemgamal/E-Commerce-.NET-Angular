using API.Models.Products;
using API.Sharing;
using static API.DTOs.Product.ProductDTO;

namespace API.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<IEnumerable<DisplayProductDTO>> GetAllAsync(ProductParam productParams);
        public Task<bool> AddAsync(CreateProductDTO productDTO);
        public Task<bool> UpdateAsync(UpdateProductDTO productDTO);
        public Task<bool> DeleteAsync(Product product);

    }
}
