using ProductionRecipes.Domain.Types;
using ProductionRecipes.Contracts;
using ProductionRecipes.DataAccess.Contexts;
using ProductionRecipes.DataAccess.Repositories.Products;
using ProductionRecipes.Domain.Entities.Products;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;
using ProductionRecipes.Domain.ValueObjects.ControlActions;
using ProductionRecipes.DataAccess.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProductionRecipes.DataAccess.Tests
    {
        [TestClass]
        public class ProductTests
        {

            private IProductRepository _productRepository;
            private IUnitOfWork _unitOfWork;


            public ProductTests()
            {
                ApplicationContext context =
                new ApplicationContext(ConnectionStringProvider.GetConnectionString());
                _productRepository = new ProductRepository(context);
                _unitOfWork = new UnitOfWork(context);
            }

            [DataRow("Producto de prueba 1")]
            [DataRow("Producto de prueba 2")]
            [TestMethod]
            public void Can_Create_Product(string name)
            {
                // Arrange
                Guid id = Guid.NewGuid();
                Product product1 = new(name, id);

                // Execute
                _productRepository.AddProduct(product1);
                _unitOfWork.SaveChanges();

                // Assert
                Product? loadedProduct = _productRepository.GetProductById(id);
                Assert.IsNotNull(loadedProduct);
            }

            [DataRow(0)]
            [TestMethod]
            public void Can_Get_Product_By_Id(int position)
            {
                // Arrange
                var products = _productRepository.GetAllProducts().ToList();
                Assert.IsNotNull(products);
                Assert.IsTrue(position < products.Count);
                Product productToGet = products[position];

                // Execute
                Product? loadedProduct = _productRepository.GetProductById(productToGet.Id);

                // Assert
                Assert.IsNotNull(loadedProduct);
            }

            [TestMethod]
            public void Cannot_Get_Product_By_Invalid_Id()
            {
                // Arrange

                // Execute
                Product? loadedProduct = _productRepository.GetProductById(Guid.Empty);

                // Assert
                Assert.IsNull(loadedProduct);
            }
          
            [DataRow(0, "BioCubaFarma", ContainerShape.Ampulas)]
            [TestMethod]
            public void Can_Update_Product(int position, string companyname, ContainerShape containerShape)
            {
                // Arrange
                var product = _productRepository.GetAllProducts().ToList();
                Assert.IsNotNull(product);
                Assert.IsTrue(position < product.Count);
                Product productToUpdate = product[position];

                // Execute
                productToUpdate.CompanyName = companyname;
                productToUpdate.Shape = containerShape;
                _productRepository.UpdateProduct(productToUpdate);
                _unitOfWork.SaveChanges();

                // Assert
                Product? loadedProduct = _productRepository.GetProductById(productToUpdate.Id);
                Assert.IsNotNull(loadedProduct);
                Assert.AreEqual(loadedProduct.CompanyName, companyname);
                Assert.AreEqual(loadedProduct.Shape, containerShape);
            }

            [DataRow(0)]
            [TestMethod]
            public void Can_Delete_Product(int position)
            {
                // Arrange
                var products = _productRepository.GetAllProducts().ToList();
                Assert.IsNotNull(products);
                Assert.IsTrue(position < products.Count);
                Product productToDelete = products[position];

                // Execute
                _productRepository.DeleteProduct(productToDelete);
                _unitOfWork.SaveChanges();

                // Assert
                Product? loadedProduct = _productRepository.GetProductById(productToDelete.Id);
                Assert.IsNull(loadedProduct);
            }

        }

    }