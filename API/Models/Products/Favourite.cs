using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Products
{
    public class Favourite : BaseEntity<int>
    {

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
