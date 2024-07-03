using ProductionRecipes.Domain.Entities;
using ProductionRecipes.Domain.Entities.Types;
using ProductionRecipes.Domain.Entities.AcccionElements;
using ProductionRecipes.DataAccess.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System;

namespace ProductionRecipes.DataAccess.Tests
{
    [TestClass]
    public class AccionElementTests
    {

        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public AccionElementTests()
        {
            ApplicationContext context =
            new ApplicationContext(ConnectionStringProvider.GetConnectionString());
            _productRepository = new ProductRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [TestMethod]
        [DataRow(?)]
        [DataRow(?)]
        public void Can_Create_Product(string name)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Product product = new Product(name, id);

            // Execute
            _productRepository.AddProduct(product);
            _unitOfWork.SaveChanges();

            // Assert
            Product? loadedProduct = _productRepository.GetProductById<Product>(id);
            Assert.IsNotNull(loadedProduct);
        }

        



    }