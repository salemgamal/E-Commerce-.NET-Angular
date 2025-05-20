using API.Models.Data;
using API.Models.Products;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EcommerceDBContext context) : base(context)
        {
        }
    }
}
