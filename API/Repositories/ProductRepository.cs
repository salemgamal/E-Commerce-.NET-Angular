using API.DTOs.Product;
using API.Models.Data;
using API.Models.Products;
using API.Repositories.Interfaces;
using API.Services;
using API.Sharing;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static API.DTOs.Product.ProductDTO;

namespace API.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        IMapper _mapper;
        EcommerceDBContext _context;
        IImageService _imageService;
        public ProductRepository(EcommerceDBContext context , IMapper mapper , IImageService imageService) : base(context)
        {
            this._mapper = mapper;
            this._context = context;
            this._imageService = imageService;
        }

        public async Task<bool> AddAsync(CreateProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return false;
            }

            var product = _mapper.Map<Product>(productDTO);
            await _context.Products.AddAsync(product);
            var result = await _context.SaveChangesAsync();

            var ImagePath = await _imageService.UploadImagesAsync(productDTO.photos , productDTO.Name);
            var photo = ImagePath.Select(x => new Photo
            {
                ImageName = x,
                ProductId = product.Id
            }).ToList();
            await _context.Photos.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateAsync(UpdateProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return false;
            }
            var product = await _context.Products.Include( m => m.Category)
                .Include(m => m.Photos)
                .FirstOrDefaultAsync(m => m.Id == productDTO.Id);
            if (product == null)
            {
                return false;
            }
            _mapper.Map(productDTO, product);
            var FindPhoto = await _context.Photos.Where(m => m.ProductId == productDTO.Id).ToListAsync();
            if (FindPhoto != null)
            {
                foreach (var item in FindPhoto)
                {
                    _imageService.DeleteImageAsync(item.ImageName);
                }
                _context.Photos.RemoveRange(FindPhoto);
            }
            var ImagePath = await _imageService.UploadImagesAsync(productDTO.photos, productDTO.Name);
            var photo = ImagePath.Select(x => new Photo
            {
                ImageName = x,
                ProductId = product.Id
            }).ToList();
            await _context.Photos.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteAsync(Product product)
        {
            try
            {
                var FindPhoto = _context.Photos.Where(m => m.ProductId == product.Id).ToList();
                if (FindPhoto != null)
                {
                    foreach (var item in FindPhoto)
                    {
                        _imageService.DeleteImageAsync(item.ImageName);
                    }
                    _context.Photos.RemoveRange(FindPhoto);
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<DisplayProductDTO>> GetAllAsync(ProductParam productParams)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Photos)
                .AsQueryable().AsNoTracking();

            //filtering by word
            if (!string.IsNullOrEmpty(productParams.Search))
            {
                var searchWords = productParams.Search.Split(' ');

                query = query.Where(m => searchWords.All(word => m.Name.ToLower().Contains(word.ToLower()) 
                || m.Description.ToLower().Contains(word.ToLower())));

                //query = query
                //    .Where(p => p.Name.ToLower().Contains(productParams.Search.ToLower()) ||
                //    p.Description.ToLower().Contains(productParams.Search.ToLower()) );
            }

            if (productParams.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == productParams.CategoryId.Value);
            }

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort.ToLower())
                {
                    case "priceasc":
                        query = query.OrderBy(p => p.NewPrice);
                        break;
                    case "pricedesc":
                        query = query.OrderByDescending(p => p.NewPrice);
                        break;
                    case "category":
                        query = query.OrderBy(p => p.Category.Name);
                        break;
                    default:
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            }


            query = query
                .Skip((productParams.PageNumber - 1) * productParams.PageSize)
                .Take(productParams.PageSize);

            var result = await query.ToListAsync();
            return _mapper.Map<IEnumerable<DisplayProductDTO>>(result);
        }
    }
}
