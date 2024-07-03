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
    public class OperationTests
    {

        private IOperationRepository _operationRepository;
        private IUnitOfWork _unitOfWork;

        public OperationTests()
        {
            ApplicationContext context =
            new ApplicationContext(ConnectionStringProvider.GetConnectionString());
            _operationRepository = new OperationRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [TestMethod]
        [DataRow(?)]
        [DataRow(?)]
        public void Can_Create_Operation(List<Fase> execFases, string name, string description)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Operation operation = new Operation(execFases, name, description, id);

            // Execute
            _operationRepository.AddOperation(operation);
            _unitOfWork.SaveChanges();

            // Assert
            Product? loadedOperation = _operationRepository.GetOperationById<Operation>(id);
            Assert.IsNotNull(loadedOperation);
        }





    }