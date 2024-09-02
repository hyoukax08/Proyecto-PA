using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;
using ProductionRecipes.Contracts;

namespace ProductionRecipes.Application.AccionElements.Commands.UpdateOperation
{
    public class UpdateOperationCommandHandler
: ICommandHandler<UpdateOperationCommand>
    {
        private readonly IAccionElementRepository _accionElementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOperationCommandHandler(
            IAccionElementRepository accionElementRepository,
            IUnitOfWork unitOfWork)
        {
            _accionElementRepository = accionElementRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
        {
            _accionElementRepository.UpdateAccionElement(request.Operation);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
