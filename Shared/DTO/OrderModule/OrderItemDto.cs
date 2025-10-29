namespace Shared.DTO.OrderModule
{
    public class OrderItemDto
    {
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}