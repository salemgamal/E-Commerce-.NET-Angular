using API.DTOs.Category;
using API.Models.Products;
using AutoMapper;
using static API.DTOs.Product.ProductDTO;

namespace API.MapperConfig
{
    public class MappConfig : Profile
    {
        public MappConfig()
        {
            //Category
            CreateMap<Category, DisplayCategoryDTO>().ReverseMap();
            CreateMap<AddCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();

            //Product
            CreateMap<Product, DisplayProductDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "No Category"))
            .ReverseMap();

            CreateMap<CreateProductDTO, Product>()
            .ForMember(p => p.Photos , op => op.Ignore() ).ReverseMap();

            CreateMap<UpdateCategoryDTO, Product>()
            .ForMember(p => p.Photos, op => op.Ignore()).ReverseMap();
            //Photo
            CreateMap<Photo, PhotoDTO>().ReverseMap();

        }
    }
}
