using System;
using NUnit.Framework;
using LegacyOrderService.Infrastructure.Repositories;
using LegacyOrderService.Domain.Exceptions;

namespace Infrastructure.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        [Test]
        public void GetPrice_ExistingProduct_ReturnsPrice()
        {
            var repo = new ProductRepository();
            var price = repo.GetPrice("Widget");
            Assert.AreEqual(12.99, price, 0.01);
        }

        [Test]
        public void GetPrice_NotFound_ThrowsProductNotFoundException()
        {
            var repo = new ProductRepository();
            Assert.Throws<ProductNotFoundException>(() => repo.GetPrice("NonExisting"));
        }

        [Test]
        public void GetAllProducts_ReturnsAllNames()
        {
            var repo = new ProductRepository();
            var products = repo.GetAllProducts();
            CollectionAssert.Contains(products, "Widget");
            CollectionAssert.Contains(products, "Gadget");
            CollectionAssert.Contains(products, "Doohickey");
        }
    }
}
