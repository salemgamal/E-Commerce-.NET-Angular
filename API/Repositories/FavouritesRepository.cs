using API.Models.Data;
using API.Models.Products;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class FavouritesRepository : GenericRepository<Favourite> , IFavouritesRepository
    {
        public FavouritesRepository(EcommerceDBContext context) : base(context)
        {
            
        }
    }
}
