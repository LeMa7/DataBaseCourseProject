namespace DataBaseCourseProject.Models
{
    public class ShoppingCartDetails
    {
        public int Id { get; set; }

        public int? ShoppingCartId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }
    }
}