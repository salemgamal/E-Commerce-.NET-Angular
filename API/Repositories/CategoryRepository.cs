using API.Models.Data;
using API.Models.Products;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EcommerceDBContext context) : base(context)
        {
        }
    }
}
