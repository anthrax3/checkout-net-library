namespace Tests.ShoppingListService
{
    using System;
    using System.Net;

    using Checkout;
    using Checkout.ApiServices.ShoppingLists.RequestModels;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = "ShoppingListApi")]
    public class ShoppingListServiceTests : BaseServiceTests
    {
        [Test]
        public void AddItem_NonExisting_CreatedResponseWithCorrectCodeReturned()
        {
            // Arrange
            var customerId = "customer_1";
            var itemToCreate = new ItemCreate { Name = "Item 1" + DateTime.Now.Ticks, Quantity = 5 };
            
            // Act
            var response = CheckoutClient.ShoppingListService.AddItem(customerId, itemToCreate);

            // Assert
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.PathAndQuery.Should().BeEquivalentTo(new Uri(string.Format(ApiUrls.ShoppingListItem, customerId, itemToCreate.Name)).PathAndQuery);
            response.Model.Code.Should().Be(3000);
        }

        [Test]
        public void AddItem_Existing_OkResponseWithQuantityUpdatedCodeReturned()
        {
            // Arrange
            var customerId = "customer_1";
            var itemToCreate = new ItemCreate { Name = "Item 1" + DateTime.Now.Ticks, Quantity = 5 };

            CheckoutClient.ShoppingListService.AddItem(customerId, itemToCreate);

            // Act
            var response = CheckoutClient.ShoppingListService.AddItem(customerId, itemToCreate);

            // Assert
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Code.Should().Be(3002);
        }

        [Test]
        public void UpdateQuantity_Existing_OkResponseWithQuantityUpdatedCodeReturned()
        {
            // Arrange
            var customerId = "customer_1";
            var itemName = "Item 1" + DateTime.Now.Ticks;
            var itemToCreate = new ItemCreate { Name = itemName, Quantity = 5 };
            var quantityToUpdate = new ItemQuantityUpdate { Quantity = 10 };

            CheckoutClient.ShoppingListService.AddItem(customerId, itemToCreate);

            // Act
            var response = CheckoutClient.ShoppingListService.UpdateQuantity(customerId, itemName, quantityToUpdate);

            // Assert
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Code.Should().Be(3002);
        }

        [Test]
        public void GetItems_OkResponseReturned()
        {
            // Arrange
            var customerId = "customer_1";

            // Act
            var response = CheckoutClient.ShoppingListService.GetAllItems(customerId);

            // Assert
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void GetItem_Existing_OkResponseWithCorrectItemReturned()
        {
            // Arrange
            var customerId = "customer_1";
            var itemName = "Item 1" + DateTime.Now.Ticks;
            var quantity = 5;
            var itemToCreate = new ItemCreate { Name = itemName, Quantity = quantity };

            CheckoutClient.ShoppingListService.AddItem(customerId, itemToCreate);

            // Act
            var response = CheckoutClient.ShoppingListService.GetItem(customerId, itemName);

            // Assert
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().Be(itemName);
            response.Model.Quantity.Should().Be(quantity);
        }

        [Test]
        public void DeleteItem_NonExisting_NotFoundResponseWithCorrectCodeReturned()
        {
            // Arrange
            var customerId = "customer_1";
            var itemName = "Item 1" + DateTime.Now.Ticks;

            // Act
            var response = CheckoutClient.ShoppingListService.DeleteItem(customerId, itemName);

            // Assert
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Model.Code.Should().Be(3004);
        }

        [Test]
        public void DeleteItem_Existing_OkResponseWithCorrectCodeReturned()
        {
            // Arrange
            var customerId = "customer_1";
            var itemName = "Item 1" + DateTime.Now.Ticks;
            var itemToCreate = new ItemCreate { Name = itemName, Quantity = 5 };

            CheckoutClient.ShoppingListService.AddItem(customerId, itemToCreate);

            // Act
            var response = CheckoutClient.ShoppingListService.DeleteItem(customerId, itemName);

            // Assert
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Code.Should().Be(3001);
        }
    }
}
