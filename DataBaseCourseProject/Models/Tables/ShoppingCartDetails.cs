namespace DataBaseCourseProject.Models.Tables
{
    public class ShoppingCartDetails : BaseTableEntity
    {
        public int? ShoppingCartId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }
    }
}