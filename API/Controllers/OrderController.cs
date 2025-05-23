using System.Security.Claims;
using API.DTOs.Order;
using API.Models.Data;
using API.Models.Orders;
using API.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static API.DTOs.Order.OrderDTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly EcommerceDBContext _context;

        public OrdersController(UnitOfWork work, IMapper mapper, EcommerceDBContext context) : base(work)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            if (dto == null)
                return BadRequest("Order data is missing.");
            var userId = User.FindFirstValue("uid");
            if (userId == null)
                return Unauthorized();

            if (dto.Items == null || !dto.Items.Any())
                return BadRequest("Order must contain at least one item.");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var product = _context.Products.Find(item.ProductId );
                if (product == null)
                    return NotFound($"Product with ID {item.ProductId} not found.");

                var price = product.NewPrice;
                totalAmount += price * item.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = price
                });
            }

            order.TotalAmount = totalAmount;
            _unitOfWork.OrderRepository.AddAsync(order);
            _unitOfWork.Save();

            //_context.Orders.Add(order);
            //await _context.SaveChangesAsync();

            return Ok(new { order.Id, order.TotalAmount });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrder(int id)
        {
            var userId = User.FindFirstValue("uid");

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            if (order == null)
                return NotFound("Order not found or access denied.");

            var orderDto = new OrderResponseDto
            {
                Id = order.Id,
                CreatedAt = order.OrderDate,
                TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.Product.NewPrice),
                Items = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.Product.NewPrice
                }).ToList()
            };

            return Ok(orderDto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var userId = User.FindFirstValue("uid");
            if (userId == null)
                return Unauthorized();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            if (order == null)
                return NotFound("Order not found or access denied.");

            _context.OrderItems.RemoveRange(order.OrderItems);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok("Order deleted successfully.");
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetAllOrders()
        {
            var userId = User.FindFirstValue("uid");
            if (userId == null)
                return Unauthorized();
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();
            var orderDtos = orders.Select(order => new OrderResponseDto
            {
                Id = order.Id,
                CreatedAt = order.OrderDate,
                TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.Product.NewPrice),
                Items = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.Product.NewPrice
                }).ToList()
            }).ToList();
            return Ok(orderDtos);
        }


    }

}
