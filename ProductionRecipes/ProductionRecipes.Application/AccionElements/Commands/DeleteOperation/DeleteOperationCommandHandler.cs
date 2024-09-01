using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;

namespace ProductionRecipes.Application.Commands.DeleteOperation
{
    public class DeleteOperationCommandHandler
        : ICommandHandler<DeleteOperationCommand>
    {
        private readonly IAccionElementRepository _accionElementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOperationCommandHandler(
            IAccionElementRepository accionElementRepository,
            IUnitOfWork unitOfWork)
        {
            _accionElementRepository = accionElementRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
        {
            var operationToDelete = _accionElementRepository.GetAccionElementById<Operation>(request.Id);
            if (operationToDelete is null)
                return Task.CompletedTask;
            _accionElementRepository.DeleteAccionElement(operationToDelete);
            _unitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
