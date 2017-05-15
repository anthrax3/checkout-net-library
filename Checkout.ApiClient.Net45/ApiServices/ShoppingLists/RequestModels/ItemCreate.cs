namespace Checkout.ApiServices.ShoppingLists.RequestModels
{
    public sealed class ItemCreate
    {
        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
