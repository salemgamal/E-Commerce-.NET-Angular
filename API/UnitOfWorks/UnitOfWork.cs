using API.Models.Data;
using API.Models.Products;
using API.Repositories;

namespace API.UnitOfWorks
{
    public class UnitOfWork
    {
        private readonly EcommerceDBContext _context;
        private CategoryRepository _categoryRepository;
        private ProductRepository _productRepository;
        private PhotoRepository _photoRepository;

        public UnitOfWork( EcommerceDBContext context)
        {
            this._context = context;
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository =new ProductRepository(_context);
                }
                return _productRepository;
            }
        }
        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_context);
                }
                return _categoryRepository;
            }
        }
        public GenericRepository<Photo> PhotoRepository
        {
            get
            {
                if (_photoRepository == null)
                {
                    _photoRepository = new PhotoRepository(_context);
                }
                return _photoRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
