﻿using API.Models.Data;
using API.Models.Products;
using API.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using API.DTOs.Order;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly EcommerceDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavouritesController(EcommerceDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToFavourites(int productId)
        {
            var userId = User.FindFirstValue("uid");
            if (userId == null)
                return Unauthorized();

            var exists = await _context.Favourites
                .AnyAsync(f => f.UserId == userId && f.ProductId == productId);

            if (exists)
                return BadRequest("Product is already in favourites.");

            var favourite = new Favourite
            {
                UserId = userId,
                ProductId = productId
            };

            _context.Favourites.Add(favourite);
            await _context.SaveChangesAsync();

            return Ok("Added to favourites.");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromFavourites(int productId)
        {
            var userId = User.FindFirstValue("uid");
            var favourite = await _context.Favourites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            if (favourite == null)
                return NotFound();

            _context.Favourites.Remove(favourite);
            await _context.SaveChangesAsync();

            return Ok("Removed from favourites.");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFavourites()
        {
            var userId = User.FindFirstValue("uid");

            var favourites = await _context.Favourites
                .Include(f => f.Product)
                .ThenInclude(p => p.Photos)
                .Where(f => f.UserId == userId)
                .Select(f => new FavouriteDTO
                {
                    Id = f.Id,
                    ProductId = f.Product.Id,
                    Name = f.Product.Name,
                    NewPrice = f.Product.NewPrice,
                    Photos = f.Product.Photos.Select(p => p.ImageName).ToList()
                }).ToListAsync();

            return Ok(favourites);
        }

    }
}
