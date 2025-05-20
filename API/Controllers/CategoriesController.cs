using System.Threading.Tasks;
using API.DTOs.Category;
using API.Models.Products;
using API.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        IMapper _mapper;
        public CategoriesController(UnitOfWork work , IMapper mapper) : base(work)
        {
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            
            if (categories == null)
            {
                return BadRequest();
            }

            var categoryDTO = _mapper.Map<IEnumerable<DisplayCategoryDTO>>(categories);
            return Ok(categoryDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            var categoryDTO = _mapper.Map<DisplayCategoryDTO>(category);
            return Ok(categoryDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDTO);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,  UpdateCategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest();
            }
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _mapper.Map(categoryDTO, category);
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            _unitOfWork.Save();
            return Ok(new { message = "Item has been updated" });
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            _unitOfWork.Save();
            return Ok(new { message = "Item has been deleted" });
        }
    }
}
