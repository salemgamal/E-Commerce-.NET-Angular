using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Orders
{
    public class Order : BaseEntity<int>
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
