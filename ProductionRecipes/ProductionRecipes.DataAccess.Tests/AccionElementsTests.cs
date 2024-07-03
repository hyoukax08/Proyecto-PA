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

        private IAccionElementRepository _accionElementRepository;
        private IUnitOfWork _unitOfWork;

        public AccionElementTests()
        {
            ApplicationContext context =
            new ApplicationContext(ConnectionStringProvider.GetConnectionString());
            _accionElementRepository = new AccionElementRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [TestMethod]
        [DataRow(?)]
        [DataRow(?)]
        public void Can_Create_AccionElement(string name, string description)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            AccionElement accionElement = new AccionElement(name, description, id);

            // Execute
            _accionElementRepository.AddAccionElement(accionElement);
            _unitOfWork.SaveChanges();

            // Assert
            AccionElement? loadedAccionElement = _accionElementRepository.GetAccionElementById<Product>(id);
            Assert.IsNotNull(loadedAccionElement);
        }





    }