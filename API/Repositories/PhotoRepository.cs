using API.Models.Data;
using API.Models.Products;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(EcommerceDBContext context) : base(context)
        {
        }
    }
}
