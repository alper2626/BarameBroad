namespace BaseEntities.Abstract
{
    public interface IDiscountableEntity 
    {
        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; }
    }
}