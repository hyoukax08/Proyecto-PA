using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;

namespace ProductionRecipes.Application.AccionElements.Commands.CreateOperation
{
    public class CreateOperationCommandHandler
        : ICommandHandler<CreateOperationCommand, Operation>
    {
        private readonly IAccionElementRepository _accionElementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOperationCommandHandler(
            IAccionElementRepository accionElementRepository,
            IUnitOfWork unitOfWork)
        {
            _accionElementRepository = accionElementRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Operation> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            Operation result = new Operation(
                request.Name,
                request.Description,
                Guid.NewGuid());

            _accionElementRepository.AddAccionElement(result);
            _unitOfWork.SaveChanges();

            return Task.FromResult(result);
        }
    }
}