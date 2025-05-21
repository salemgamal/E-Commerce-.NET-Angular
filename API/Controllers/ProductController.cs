using API.UnitOfWorks;
using API.DTOs.Product;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API.DTOs.Product.ProductDTO;
using API.Models.Products;
using API.Sharing;
using API.Helper;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        IMapper _mapper;
        public ProductController( UnitOfWork work , IMapper mapper) : base(work)
        {
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]ProductParam productParams )
        {

            var products = await _unitOfWork.ProductRepository.GetAllAsync(productParams);
            
            if (products == null)
            {
                return BadRequest();
            }
            var totalCount = await _unitOfWork.ProductRepository.CountAsync();
            return Ok(new Pagination<DisplayProductDTO>(productParams.PageNumber , productParams.PageSize, totalCount , products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id , p => p.Category, products => products.Photos);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = _mapper.Map<DisplayProductDTO>(product);
            return Ok(productDTO);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add([FromForm] CreateProductDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                    return BadRequest("Product data is missing");

                var result = await _unitOfWork.ProductRepository.AddAsync(productDTO);
                if (!result)
                    return BadRequest("Failed to add product");

                return Ok("Product created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id , [FromForm] UpdateProductDTO productDTO)
        {
            try
            {
                await _unitOfWork.ProductRepository.UpdateAsync(productDTO);
                return Ok("Product updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository
                    .GetByIdAsync(id , p => p.Photos , product=> product.Category);
                await _unitOfWork.ProductRepository.DeleteAsync(product);
                return Ok("Product deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Authorize]
        [HttpGet("secret")]
        public IActionResult Secret()
        {
            return Ok("You are authorized!");
        }

    }
}
