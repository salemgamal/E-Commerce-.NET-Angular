using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Products
{
    public class Photo : BaseEntity<int>
    {
        public string ImageName { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
