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
    public class FaseTests
    {

        private IFaseRepository _faseRepository;
        private IUnitOfWork _unitOfWork;

        public FaseTests()
        {
            ApplicationContext context =
            new ApplicationContext(ConnectionStringProvider.GetConnectionString());
            _faseRepository = new FaseRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [TestMethod]
        [DataRow(?)]
        [DataRow(?)]
        public void Can_Create_Fase(List<ControlAction> actionsList, string name, string description)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Fase fase = new Fase( actionsList, name, description, id);

            // Execute
            _faseRepository.AddFase(fase);
            _unitOfWork.SaveChanges();

            // Assert
            Fase? loadedFase = _faseRepository.GetFaseById<Fase>(id);
            Assert.IsNotNull(loadedFase);
        }





    }