using API.Models.Products;
using static API.DTOs.Product.ProductDTO;

namespace API.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> AddAsync(CreateProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO productDTO);
    }
}
