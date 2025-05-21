namespace API.DTOs.Order
{
    public class OrderDTO
    {
        public class CreateOrderDto
        {
            public string UserId { get; set; }
            public List<OrderItemDto> Items { get; set; }
        }

        public class OrderItemDto
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
