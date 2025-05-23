using API.Models.Data;
using API.Models.Products;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using AutoMapper;

namespace API.UnitOfWorks
{
    public class UnitOfWork
    {
        private readonly EcommerceDBContext _context;
        private CategoryRepository _categoryRepository;
        private ProductRepository _productRepository;
        private PhotoRepository _photoRepository;
        private OrderRepository _orderRepository;
        private FavouritesRepository _favouriteRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public UnitOfWork( EcommerceDBContext context , IImageService imageService , IMapper mapper)
        {
            this._context = context;
            this._imageService = imageService;
            this._mapper = mapper;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository =new ProductRepository(_context , _mapper , _imageService);
                }
                return _productRepository;
            }
        }
        public ICategoryRepository CategoryRepository
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
        public IPhotoRepository PhotoRepository
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

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_context);
                }
                return _orderRepository;
            }
        }
        public IFavouritesRepository FavouritesRepository
        {
            get
            {
                if (_favouriteRepository == null)
                {
                    _favouriteRepository = new FavouritesRepository(_context);
                }
                return _favouriteRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
