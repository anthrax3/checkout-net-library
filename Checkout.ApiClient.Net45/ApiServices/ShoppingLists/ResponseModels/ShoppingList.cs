namespace Checkout.ApiServices.ShoppingLists.ResponseModels
{
    using System.Collections.Generic;

    public class ShoppingList
    {
        public long Count { get; set; }

        public IEnumerable<ShoppingListItem> Items { get; set; }
    }
}
