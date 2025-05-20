using API.UnitOfWorks;
using API.DTOs.Product;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API.DTOs.Product.ProductDTO;
using API.Models.Products;

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
        public async Task<IActionResult> GetAll()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync( p=> p.Category , products=> products.Photos);
            
            if (products == null)
            {
                return BadRequest();
            }
            var productDTO = _mapper.Map<IEnumerable<DisplayProductDTO>>(products);
            return Ok(productDTO);
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
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _unitOfWork.ProductRepository.DeleteAsync(id);
            _unitOfWork.Save();
            return NoContent();
        }

    }
}
