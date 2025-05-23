namespace API.DTOs.Order
{
    public class FavouriteDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal NewPrice { get; set; }
        public List<string> Photos { get; set; }
    }
}
