namespace Checkout.ApiServices.ShoppingLists
{
    using System;
    using System.Web;

    using Checkout.ApiServices.SharedModels;
    using Checkout.ApiServices.ShoppingLists.RequestModels;
    using Checkout.ApiServices.ShoppingLists.ResponseModels;

    public sealed class ShoppingListService
    {
        public HttpResponse<BaseResponse> AddItem(string customerId, ItemCreate requestModel)
        {
            var createUri = string.Format(ApiUrls.ShoppingListItemCreate, customerId);
            return new ApiHttpClient().PostRequest<BaseResponse>(createUri, AppSettings.SecretKey, requestModel);
        }

        public HttpResponse<BaseResponse> DeleteItem(string customerId, string itemName)
        {
            var deleteUri = string.Format(ApiUrls.ShoppingListItemDelete, customerId, itemName);
            return new ApiHttpClient().DeleteRequest<BaseResponse>(deleteUri, AppSettings.SecretKey);
        }

        public HttpResponse<BaseResponse> UpdateQuantity(string customerId, string itemName, ItemQuantityUpdate requestModel)
        {
            var quantityUpdateUri = string.Format(ApiUrls.ShoppingListItemQuantityUpdate, customerId, itemName);
            return new ApiHttpClient().PutRequest<BaseResponse>(quantityUpdateUri, AppSettings.SecretKey, requestModel);
        }

        public HttpResponse<ShoppingList> GetAllItems(string customerId, int? pageNumber = 1, int? pageSize = 10)
        {
            var itemsUri = new Uri(string.Format(ApiUrls.ShoppingListItems, customerId));

            if (pageNumber.HasValue)
            {
                itemsUri = AddQueryParameter(itemsUri, "pageNumber", pageNumber.Value.ToString());
            }

            if (pageSize.HasValue)
            {
                itemsUri = AddQueryParameter(itemsUri, "pageSize", pageSize.Value.ToString());
            }

            return new ApiHttpClient().GetRequest<ShoppingList>(itemsUri.ToString(), AppSettings.SecretKey);
        }

        public HttpResponse<ShoppingListItem> GetItem(string customerId, string itemName)
        {
            var itemUri = string.Format(ApiUrls.ShoppingListItem, customerId, itemName);
            return new ApiHttpClient().GetRequest<ShoppingListItem>(itemUri, AppSettings.SecretKey);
        }

        private static Uri AddQueryParameter(Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }
    }
}
