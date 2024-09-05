using ProductionRecipes.Domain.Types;
using ProductionRecipes.Contracts;
using ProductionRecipes.DataAccess.Contexts;
using ProductionRecipes.DataAccess.Repositories.AccionElements;
using ProductionRecipes.Domain.Entities.AccionElements;
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

        [DataRow("Operacion de prueba","Pruebas totales")]
        [DataRow("Operacion de inicio", "Inicio del trabajo")]
        [TestMethod]
        public void Can_Create_Operation( string name, string description)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            List<Fase> listfases1 = new List<Fase>();
            Operation operation1 = new( name, description, id);

            // Execute
            _accionElementRepository.AddAccionElement(operation1);
            _unitOfWork.SaveChanges();

            // Assert
            Operation? loadedOperation = _accionElementRepository.GetAccionElementById<Operation>(id);
            Assert.IsNotNull(loadedOperation);
        }

        [DataRow("Fase 1", "Fase de prueba")]
        [DataRow("Fase 2", "Fase de correcion")]
        [TestMethod]
        public void Can_Create_Fase( string name, string description)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            List<ControlAction> listCA1 = new List<ControlAction>();
            Fase fase1 = new Fase(listCA1, name, description, id);

            // Execute
            _accionElementRepository.AddAccionElement(fase1);
            _unitOfWork.SaveChanges();

            // Assert
            Fase? loadedFase = _accionElementRepository.GetAccionElementById<Fase>(id);
            Assert.IsNotNull(loadedFase);
        }

        [DataRow(0)]
        [TestMethod]
        public void Can_Get_Operation_By_Id(int position)
        {
            // Arrange
            var operations = _accionElementRepository.GetAllAccionElements<Operation>().ToList();
            Assert.IsNotNull(operations);
            Assert.IsTrue(position < operations.Count);
            Operation operationToGet = operations[position];

            // Execute
            Operation? loadedOperation = _accionElementRepository.GetAccionElementById<Operation>(operationToGet.Id);

            // Assert
            Assert.IsNotNull(loadedOperation);
        }

        [DataRow(1)]
        [TestMethod]
        public void Can_Get_Fase_By_Id(int position)
        {
            // Arrange
            var fases = _accionElementRepository.GetAllAccionElements<Fase>().ToList();
            Assert.IsNotNull(fases);
            Assert.IsTrue(position < fases.Count);
            Fase faseToGet = fases[position];

            // Execute
            Fase? loadedFase = _accionElementRepository.GetAccionElementById<Fase>(faseToGet.Id);

            // Assert
            Assert.IsNotNull(loadedFase);
        }

        [TestMethod]
        public void Cannot_Get_Operation_By_Invalid_Id()
        {
            // Arrange

            // Execute
            Operation? loadedOperation = _accionElementRepository.GetAccionElementById<Operation>(Guid.Empty);

            // Assert
            Assert.IsNull(loadedOperation);
        }

        [TestMethod]
        public void Cannot_Get_Fase_By_Invalid_Id()
        {
            // Arrange

            // Execute
            Fase? loadedFase = _accionElementRepository.GetAccionElementById<Fase>(Guid.Empty);

            // Assert
            Assert.IsNull(loadedFase);
        }

        [DataRow(0, "Unidad inicial")]
        [TestMethod]
        public void Can_Update_Operation(int position, string unityname)
        {
            // Arrange
            var operations = _accionElementRepository.GetAllAccionElements<Operation>().ToList();
            Assert.IsNotNull(operations);
            Assert.IsTrue(position < operations.Count);
           Operation operationToUpdate = operations[position];

            // Execute
            operationToUpdate.UnityName = unityname;
            _accionElementRepository.UpdateAccionElement(operationToUpdate);
            _unitOfWork.SaveChanges();

            // Assert
            Operation? loadedOperation = _accionElementRepository.GetAccionElementById<Operation>(operationToUpdate.Id);
            Assert.IsNotNull(loadedOperation);
            Assert.AreEqual(loadedOperation.UnityName, unityname);
        }

        [DataRow(0, 30)]
        [TestMethod]
        public void Can_Update_Fase(int position, int duration)
        {
            // Arrange
            var fases = _accionElementRepository.GetAllAccionElements<Fase>().ToList();
            Assert.IsNotNull(fases);
            Assert.IsTrue(position < fases.Count);
            Fase faseToUpdate = fases [position];

            // Execute
            faseToUpdate.Duration = duration;
            _accionElementRepository.UpdateAccionElement(faseToUpdate);
            _unitOfWork.SaveChanges();

            // Assert
            Fase? loadedfase = _accionElementRepository.GetAccionElementById<Fase>(faseToUpdate.Id);
            Assert.IsNotNull(loadedfase);
            Assert.AreEqual(loadedfase.Duration, duration);
        }

        [DataRow(0)]
        [TestMethod]
        public void Can_Delete_Operation(int position)
        {
            // Arrange
            var operations = _accionElementRepository.GetAllAccionElements<Operation>().ToList();
            Assert.IsNotNull(operations);
            Assert.IsTrue(position < operations.Count);
            Operation operationToDelete = operations[position];

            // Execute
            _accionElementRepository.DeleteAccionElement(operationToDelete);
            _unitOfWork.SaveChanges();

            // Assert
            Operation? loadedOperation = _accionElementRepository.GetAccionElementById<Operation>(operationToDelete.Id);
            Assert.IsNull(loadedOperation);
        }

        [DataRow(0)]
        [TestMethod]
        public void Can_Delete_Fase(int position)
        {
            // Arrange
            var fases = _accionElementRepository.GetAllAccionElements<Fase>().ToList();
            Assert.IsNotNull(fases);
            Assert.IsTrue(position < fases.Count);
            Fase faseToDelete = fases[position];

            // Execute
            _accionElementRepository.DeleteAccionElement(faseToDelete);
            _unitOfWork.SaveChanges();

            // Assert
            Fase? loadedFase = _accionElementRepository.GetAccionElementById<Fase>(faseToDelete.Id);
            Assert.IsNull(loadedFase);
        }

    }



}