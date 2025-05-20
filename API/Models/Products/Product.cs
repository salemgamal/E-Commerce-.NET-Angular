using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Products
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
